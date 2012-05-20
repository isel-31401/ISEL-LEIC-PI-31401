using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CurricularUnitFormManager.Models.CurricularUnit;
using CurricularUnitFormManager.Models.Repository;
using CurricularUnitFormManager.Models.PageManagement;

namespace CurricularUnitFormManager.Controllers
{
    public class OfficialController : Controller
    {
        static CurricularUnitFormRepository _repository = CurricularUnitFormRepository.Instance;

        public ActionResult Index()
        {
            //return View();
            return this.RedirectToAction("Page", new { pagenr = 0, itemsnr = 2, order = "Asc" });
        }


        public ActionResult Page(int pagenr, int itemsnr, String order)
        {
            if (!(Paging.itemsPerPage).Contains(itemsnr)) return RedirectToAction("NotFound", "Error"); // o numero de items por página está errado

            if (!(Paging.orderOptions).Contains(order)) return RedirectToAction("MethodNotAllowed", "Error"); // o método de ordenação está errado
            
            int amountPages = ((_repository.GetAllOfficialCurricularUnitForms().Count() + itemsnr - 1) / itemsnr); // vamos testar as páginas

            if (pagenr < 0 || pagenr > (amountPages - 1)) return RedirectToAction("NotFound", "Error"); // o numero de páginas está errada. 

            String[] a = new String[Request.Headers.Count];
            for(int i = 0; i < a.Length; ++i)
            {
                a[i] = Request.Headers[i];
                Console.WriteLine(a[i]);
            }
            if (Paging.IsAjaxRequest(Request))
            //if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
            {
                ViewBag.ajax = "true";
                return Json("MORANGOS", JsonRequestBehavior.AllowGet);
                //return Json(Paging.BuildPage(pagenr, itemsnr, order, _repository.GetAllOfficialCurricularUnitForms(), "Official", "Detail", false), JsonRequestBehavior.AllowGet);
                //Represents a class that extends the System.Web.HttpRequestBase class by adding the ability
                //to determine whether an HTTP request is an AJAX request.
            }
            ViewBag.ajax = "false";

            return View(Paging.BuildPage(pagenr, itemsnr, order, _repository.GetAllOfficialCurricularUnitForms(), "Official", "Detail", false));
        }

        public ActionResult Detail(String acr)
        {
            AbstractCurricularUnitForm cuf = _repository.Detail(acr);
            return View(cuf);
        }

        public ActionResult XML(String acr) 
        {
            var xml = acr ?? "Lista XML de acrónimos";
            return View((object)xml);
        }

        //TODO Test
        public ActionResult Search(String SearchValue)
        {
            CUFsListModel clm = new CUFsListModel("Official");
            foreach (var cuf in _repository.Search(SearchValue))
            {
                clm.AddURIParams(cuf.Name, "Detail", cuf.Acr);
            }

            return View("Search",clm.GetCUFsListModel());
        }
        //TODO need Testing
        public ActionResult SearchTags(String SearchValue) 
        {
            List<String> clm = new List<String>();
            foreach (var cuf in _repository.Search(SearchValue))
            {
                clm.Add(cuf.Name);
                clm.Add(cuf.Acr);
            }
            return Json(clm, JsonRequestBehavior.AllowGet);
        }
    }
}
