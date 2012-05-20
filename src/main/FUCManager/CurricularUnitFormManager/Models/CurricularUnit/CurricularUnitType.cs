using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CurricularUnitFormManager.Models.CurricularUnit
{
    public enum Degree { EIC=0, EETC, ERCM };
    public enum CourseType { M=0, O }; //M - mandatory; O - opcional
    [Flags]
    public enum Semester { S1 = 0x01, S2 = 0x02, S3 = 0x04, S4 = 0x08, S5 = 0x10, S6 = 0x20 };

    public class CurricularUnitType
    {
        private CourseType _courseType;   
        private Semester _semester;
        private Degree _degree;

        public CurricularUnitType()
        {
        }

        public CourseType CourseType
        {
            get { return _courseType; }
            set { _courseType = value; }
        }

        public Semester Semester
        {
            get { return _semester; }
            set { _semester = value; }
        }

        public Degree Degree
        {
            get { return _degree; }
            set { _degree = value; }
        }
        public bool isDegree(Degree d)
        {
            return _degree == d;
        }
        public bool isAvailable(Semester s)
        {
            return ((_semester & s) != 0);
        }
    }
}
