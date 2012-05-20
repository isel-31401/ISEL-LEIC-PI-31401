using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CurricularUnitFormManager.Models.CurricularUnit;

namespace CurricularUnitFormManager.Models
{
    class CurricularUnitFormProcessor
    {
        public static CurricularUnitForm Process(IEnumerable<KeyValuePair<string, string>> content)
        {

            if (content == null) return null;


            CurricularUnitForm cuf = new CurricularUnitForm();

            cuf.CUnit.Acronym = content.Where(p => p.Key == "acronym").FirstOrDefault().Value;
            cuf.CUnit.Name = content.Where(p => p.Key == "name").FirstOrDefault().Value;
            cuf.CUnit.ECTS = Double.Parse(content.Where(p => p.Key == "credits").FirstOrDefault().Value);

            cuf.Type.CourseType = (CourseType)Enum.Parse(typeof(CourseType), content.Where(p => p.Key == "coursetype").FirstOrDefault().Value);
            cuf.Type.Degree = (Degree)Enum.Parse(typeof(Degree), content.Where(p => p.Key == "degree").FirstOrDefault().Value);
            cuf.Type.Semester = (Semester)Enum.Parse(typeof(Semester), content.Where(p => p.Key == "semester").FirstOrDefault().Value);

            cuf.Description.CourseProgram = content.Where(p => p.Key == "courseprogram").FirstOrDefault().Value;
            cuf.Description.Language = "PT";
            cuf.Description.LearningResults = content.Where(p => p.Key == "learningresults").FirstOrDefault().Value;
            cuf.Description.Objectives = content.Where(p => p.Key == "objectives").FirstOrDefault().Value;
            cuf.Description.ResultEvaluation = content.Where(p => p.Key == "resultevaluation").FirstOrDefault().Value;

            //TODO Required Courses
            //TODO Verify Contents


            return cuf;
        }


    }


}
