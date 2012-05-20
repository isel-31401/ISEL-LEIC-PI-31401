using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Web;
using System.Xml;
using CurricularUnitFormManager.Models.CurricularUnit;

namespace CurricularUnitFormManager.Models.Repository
{
    class CurricularUnitFormLoader
    {

        public static bool Load(ICurricularUnitFormRepository<CurricularUnitForm> _repo, String _filePath)
        {
            try
            {
                CurricularUnitForm cuf = new CurricularUnitForm();

                if (_repo == null || _filePath == null) return false;

                XmlTextReader xmlFile = new XmlTextReader(_filePath);
                //xmlFile.Read();

                while (xmlFile.Read())
                {
                    xmlFile.MoveToElement();
                    if (xmlFile.NodeType == XmlNodeType.Element && xmlFile.Name.Equals("fuc"))
                    {
                        ulong id = ulong.Parse(xmlFile.GetAttribute("id"));

                        XmlReader xmlTree = xmlFile.ReadSubtree();
                        while (xmlTree.Read())
                        {
                            if (xmlFile.NodeType == XmlNodeType.Element)
                            {
                                switch (xmlTree.Name)
                                {
                                    case "uc":
                                        String acr = xmlFile.GetAttribute("acronym");
                                        cuf = _repo.GetByAcronym(acr);
                                        if (cuf == null)
                                            cuf = new CurricularUnitForm(xmlFile.GetAttribute("name"), acr);
                                        cuf.CUnit.Name = xmlFile.GetAttribute("name");
                                        cuf.ID = id;
                                        try
                                        {
                                            cuf.CUnit.ECTS = Convert.ToDouble(xmlFile.GetAttribute("ects"));
                                        }
                                        catch (FormatException e)
                                        {
                                            cuf.CUnit.ECTS = 0;
                                        }
                                        break;
                                    case "ruc":
                                        acr = xmlFile.GetAttribute("acronym");
                                        CurricularUnitForm cuf_aux = _repo.GetByAcronym(acr);
                                        if (cuf_aux == null)
                                            cuf_aux = new CurricularUnitForm(xmlFile.GetAttribute("name"), acr);
                                        _repo.Add(cuf_aux);
                                        cuf.AddRequiredCourses(cuf_aux);
                                        break;
                                    case "type-uc":
                                        cuf.Type.CourseType = (CourseType)Enum.Parse(typeof(CourseType), xmlFile.GetAttribute("courseType"));
                                        cuf.Type.Semester = (Semester)Enum.Parse(typeof(Semester), xmlFile.GetAttribute("semester"));
                                        cuf.Type.Degree = (Degree)Enum.Parse(typeof(Degree), xmlFile.GetAttribute("degree"));
                                        break;
                                    case "objectives":
                                        cuf.Description.Objectives = xmlFile.ReadString();
                                        break;
                                    case "learningResults":
                                        cuf.Description.LearningResults = xmlFile.ReadString();
                                        break;
                                    case "resultEvaluation":
                                        cuf.Description.ResultEvaluation = xmlFile.ReadString();
                                        break;
                                    case "courseProgram":
                                        cuf.Description.CourseProgram = xmlFile.ReadString();
                                        break;
                                    case "language":
                                        cuf.Description.Language = xmlFile.ReadString();
                                        break;
                                }
                            }
                        }
                        if (cuf.Type.Degree.Equals(Degree.EIC))
                            _repo.Add(cuf);
                    }
                }
            }

            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
            return true;    
        }

        private static int StringToInt(String str)
        {
            int res;
            try
            {
                res = Convert.ToInt32(str);
            }
            catch (FormatException e)
            {
                res = 0;
            }
            return res;
        }

        private static double StringToDouble(String str)
        {
            double res;
            try
            {
                res = Convert.ToInt32(str);
            }
            catch (FormatException e)
            {
                res = 0;
            }
            return res;
        }



        

    }
}
