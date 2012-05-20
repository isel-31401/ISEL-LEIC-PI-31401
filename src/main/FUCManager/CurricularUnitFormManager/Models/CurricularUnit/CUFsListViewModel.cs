using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CurricularUnitFormManager.Models.CurricularUnit
{
    public class CUFsListViewModel
    {
        public IEnumerable<CUFNameURIModel> CUFList { get; set; }
        public int PageNumber { get; set; }
        public int ItemsNumber { get; set; }
        public String Order { get; set; }
        public int AmountPage { get; set; }
        public bool LastPage { get; set; }
        public int[] ItemsPerPage { get; set; }
        public String[] OrderPage { get; set; }
        public String Method { get; set; }
    }
}