using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CurricularUnitFormManager.Models.Individual
{
    public class IndividualViewModel
    {
        public Individual User{ get; set; }
        public String[] AvailableRoles { get { return IndividualRoles.getRoles(); } }
        public HttpPostedFileBase file {get;set;}
    }
}