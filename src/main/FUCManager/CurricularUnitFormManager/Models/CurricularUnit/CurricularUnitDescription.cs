using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CurricularUnitFormManager.Models.CurricularUnit
{
    public class CurricularUnitDescription
    {
        private string _objectives;
        private string _learningResults;
        private string _resultEvaluation;
        private string _courseProgram;
        private string _language;

        public CurricularUnitDescription()
        {
            _objectives = "";
            _learningResults = "";
            _resultEvaluation = "";
            _courseProgram = "";
            _language = "";
        }

        public string Objectives
        {
            get { return _objectives; }
            set { _objectives = value; }
        }

        public string LearningResults
        {
            get { return _learningResults; }
            set { _learningResults = value; }
        }

        public string ResultEvaluation
        {
            get { return _resultEvaluation; }
            set { _resultEvaluation = value; }
        }

        public string CourseProgram
        {
            get { return _courseProgram; }
            set { _courseProgram = value; }
        }

        public string Language
        {
            get { return _language; }
            set { _language = value; }
        }
    }
}
