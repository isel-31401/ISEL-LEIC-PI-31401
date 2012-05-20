using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CurricularUnitFormManager.Models;
using CurricularUnitFormManager.Models.CurricularUnit;
using CurricularUnitFormManager.Models.Repository;

namespace CurricularUnitFormManager.Controllers
{
    public class CUFController : Controller
    {
        public ActionResult Index() //link para a lista de propostas e lista de oficiais
        {
            return View();
        }
    }
}