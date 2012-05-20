using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CurricularUnitFormManager.Models.CurricularUnit
{
    public class OfficialCurricularUnitForm : AbstractCurricularUnitForm
    {
        private String _myType;
        public OfficialCurricularUnitForm() :this(new CurricularUnitForm())
        {
        }

        public OfficialCurricularUnitForm(CurricularUnitForm t) : base()
        {
            CourseFile = t;
            _myType = "OFFICIAL";
        }
        public override String CurricularUnitFormType() { return _myType; }
    }
}