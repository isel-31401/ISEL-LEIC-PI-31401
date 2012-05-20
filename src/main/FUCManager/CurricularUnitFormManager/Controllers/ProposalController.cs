using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CurricularUnitFormManager.Models.CurricularUnit;
using CurricularUnitFormManager.Models.PageManagement;
using CurricularUnitFormManager.Models.Repository;

namespace CurricularUnitFormManager.Controllers
{
    public class ProposalController : Controller
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
            
            int amountPages = ((_repository.GetAllProposalCurricularUnitForms().Count() + itemsnr - 1) / itemsnr); // vamos testar as páginas

            if (pagenr < 0 || pagenr > (amountPages - 1)) return RedirectToAction("NotFound", "Error"); // o numero de páginas está errada. 

            return View(Paging.BuildPage(pagenr, itemsnr, order, _repository.GetAllProposalCurricularUnitForms(), "Proposal", "Version", false));
        }

        public ActionResult Version(String acr)
        {
            //return View();
            return this.RedirectToAction("PageVersion", new { pagenr = 0, itemsnr = 2, order = "Asc", acr });
        }

        public ActionResult PageVersion(int pagenr, int itemsnr, String order, String acr)
        {
            if (!(Paging.itemsPerPage).Contains(itemsnr)) return RedirectToAction("NotFound", "Error"); // o numero de items por página está errado

            if (!(Paging.orderOptions).Contains(order)) return RedirectToAction("MethodNotAllowed", "Error"); // o método de ordenação está errado

            int amountPages = ((_repository.GetAllProposalCurricularUnitFormsVersions(acr).Count() + itemsnr - 1) / itemsnr); // vamos testar as páginas

            if (pagenr < 0 || pagenr > (amountPages - 1)) return RedirectToAction("NotFound", "Error"); // o numero de páginas está errada. 

            return View(Paging.BuildPage(pagenr, itemsnr, order, _repository.GetAllProposalCurricularUnitFormsVersions(acr), "Proposal", "Detail", true));
        }

        public ActionResult Detail(String acr, ulong id)
        {
            AbstractCurricularUnitForm cuf = _repository.GetProposalCurricularUnitForm(acr, id);
            return View(cuf);
        }

        #region NEW
        public ActionResult New()
        {
            return View(new ProposalCurricularUnitForm());
        }

        #endregion
        #region EDIT
        public ActionResult EditOfficial(String acr)
        {
            AbstractCurricularUnitForm cuf = _repository.GetOfficialCurricularUnitForm(acr);

            return View(cuf);
        }

        [ActionName("New")]
        [Authorize]
        [HttpPost]
        public ActionResult NewAction(ProposalCurricularUnitForm cuf)
        {
            _repository.New(cuf);
            return RedirectToAction("Index");
        }

        public ActionResult Edit(String acr, ulong version = (ulong) 0)
        {
            AbstractCurricularUnitForm cuf = _repository.GetProposalCurricularUnitForm(acr,version);
            return View(cuf);
        }


        [HttpPost]
        public ActionResult Edit(ProposalCurricularUnitForm cuf)
        {
            if (_repository.Edit(cuf))
            {
                return View("Detail",cuf); //TODO : Redirect to Sucess then to index
            }
            ModelState.AddModelError("", "Data Invalid");
            return View();
        }
        #endregion
        #region ACCEPT
        public ActionResult Accept(String acr, ulong version)
        {
            AbstractCurricularUnitForm cuf = _repository.GetProposalCurricularUnitForm(acr, version);
            return View(cuf);
        }

        [ActionName("Accept")]
        [HttpPost]
        public ActionResult AcceptConfirmation(String acr, ulong version)
        {
            _repository.Accept(acr, version);
            return RedirectToAction("Index");
        }
        #endregion

        #region CANCEL
        public ActionResult Cancel(String acr, ulong version)
        {
            AbstractCurricularUnitForm cuf = _repository.GetProposalCurricularUnitForm(acr, version);
            return View(cuf);
        }

        [HttpPost]
        public ActionResult CancelConfirmation(String acr, ulong version)
        {
            _repository.Reject(acr, version);
            return View("CancelConfirmation");
        }
        #endregion
    }
}
