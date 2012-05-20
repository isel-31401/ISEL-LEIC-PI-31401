using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CurricularUnitFormManager.Models.CurricularUnit
{
    public class ProposalCurricularUnitForm : AbstractCurricularUnitForm
    {
        private String _myType;
        public ProposalCurricularUnitForm(): this(new CurricularUnitForm())
        {}

        public ProposalCurricularUnitForm(CurricularUnitForm t)
            : base()
        {
            CourseFile = t;
            _myType = "PROPOSAL";
        }
        public override String CurricularUnitFormType() { return _myType; }
    }
}