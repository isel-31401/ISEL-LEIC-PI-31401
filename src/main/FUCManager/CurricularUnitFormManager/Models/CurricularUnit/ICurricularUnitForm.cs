using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CurricularUnitFormManager.Models.CurricularUnit
{
    interface ICurricularUnitForm
    {
         bool IsValid{set;get;}
         DateTime UserTimeStamp { set; get; }
         string Username { set; get; }
         ulong Version { set; get; }
         CurricularUnitForm CourseFile { set; get; }
         String CurricularUnitFormType();
    }
}
