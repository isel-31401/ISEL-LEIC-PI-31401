using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml;
using CurricularUnitFormManager.Models.Repository;
using CurricularUnitFormManager.Models.CurricularUnit;

namespace CurricularUnitFormManager.Models
{
    class CurricularUnitFormWriter
    {
        public static bool WriteRepositoryToFile(ICurricularUnitFormRepository<CurricularUnitForm> cufr, string filePath)
        {
            try
            {
                XmlDocument doc = WriteRepository(cufr);
                XmlTextWriter writer = new XmlTextWriter(filePath, null);
                writer.Formatting = Formatting.Indented;
                doc.Save(writer);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
            return true;
        }

        public static XmlDocument WriteRepository(ICurricularUnitFormRepository<CurricularUnitForm> cufr)
        {
            XmlDocument xmlFile = new XmlDocument();
            XmlDeclaration decl = xmlFile.CreateXmlDeclaration("1.0", "utf-8", null);
            xmlFile.InsertBefore(decl, xmlFile.DocumentElement);
            
            XmlDocumentType type = xmlFile.CreateDocumentType("fuc-repository", "template.dtd", null, null);
            xmlFile.AppendChild(type);
            XmlElement fuc_repo = xmlFile.CreateElement("fuc-repository");
            fuc_repo.SetAttribute("school", "Instituto Superior de Engenharia de Lisboa");
            fuc_repo.SetAttribute("language", "pt");
            xmlFile.AppendChild(fuc_repo);
            
            foreach (CurricularUnitForm cuf in cufr.GetAll())
            {
                /*http://stackoverflow.com/questions/982597/what-is-the-fastest-way-to-combine-two-xml-files-into-one*/
                var xmlTmp = xmlFile.ImportNode(Write(cuf, false).DocumentElement, true);
                fuc_repo.AppendChild(xmlTmp);
            }
            return xmlFile;
        }

        public static XmlDocument Write(CurricularUnitForm cuf, bool declaration)
        {
            XmlDocument xmlFile = new XmlDocument();
            if (declaration)
                xmlFile.AppendChild(xmlFile.CreateXmlDeclaration("1.0","utf-8", null));
            
            XmlElement fuc = xmlFile.CreateElement("fuc");
            fuc.SetAttribute("id",cuf.ID.ToString());
            xmlFile.AppendChild(fuc);

            XmlElement uc = xmlFile.CreateElement("uc");
            uc.SetAttribute("name", cuf.CUnit.Name);
            uc.SetAttribute("acronym", cuf.CUnit.Acronym);
            uc.SetAttribute("ects", cuf.CUnit.ECTS.ToString());
            fuc.AppendChild(uc);
            
            XmlElement req = xmlFile.CreateElement("required-uc");
            foreach (CurricularUnitForm requiredUC in cuf.RequiredCourses)
            {
                XmlElement reqUC = xmlFile.CreateElement("ruc");
                reqUC.SetAttribute("name", requiredUC.CUnit.Name);
                reqUC.SetAttribute("acronym", requiredUC.CUnit.Acronym);
                req.AppendChild(reqUC);
            }
            uc.AppendChild(req);

            XmlElement type = xmlFile.CreateElement("type-uc");
            type.SetAttribute("courseType", cuf.Type.CourseType.ToString());
            type.SetAttribute("semester", cuf.Type.Semester.ToString());
            type.SetAttribute("degree", cuf.Type.Degree.ToString());
            uc.AppendChild(type);

            XmlElement desc = xmlFile.CreateElement("description-uc");

            XmlElement objectives = xmlFile.CreateElement("objectives");
            objectives.InnerText = cuf.Description.Objectives;
            desc.AppendChild(objectives);

            XmlElement learningResults = xmlFile.CreateElement("learningResults");
            learningResults.InnerText = cuf.Description.LearningResults;
            desc.AppendChild(learningResults);

            XmlElement resultEvaluation = xmlFile.CreateElement("resultEvaluation");
            resultEvaluation.InnerText = cuf.Description.ResultEvaluation;
            desc.AppendChild(resultEvaluation);

            XmlElement courseProgram = xmlFile.CreateElement("courseProgram");
            courseProgram.InnerText = cuf.Description.CourseProgram;
            desc.AppendChild(courseProgram);

            XmlElement language = xmlFile.CreateElement("language");
            language.InnerText = cuf.Description.Language;
            desc.AppendChild(language);

            fuc.AppendChild(desc);
            return xmlFile;
        }

    }
}
