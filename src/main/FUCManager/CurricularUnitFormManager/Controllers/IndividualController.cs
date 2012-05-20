using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using CurricularUnitFormManager.Models.Individual;
using CurricularUnitFormManager.Models.Repository;
using System.IO;

namespace CurricularUnitFormManager.Controllers
{
    public class IndividualController : Controller
    {
        readonly static IndividualRepository _userRepository = IndividualRepository.Instance;
        
       
        public ActionResult Index()
        {
            List<IndividualRepoModel> users = new List<IndividualRepoModel>();
            foreach (Individual user in _userRepository.getUserList())
                users.Add(new IndividualRepoModel(user));
            return View(users);
        }

        [Authorize]
        public ActionResult ViewUser(String username)
        {
            Individual user = (Individual)_userRepository.GetUser(username, true);
            IndividualViewModel ind = new IndividualViewModel();
            ind.User = user;

            return View(ind);
        }


        [Authorize]
        public ActionResult EditUser(String username)
        {
            Individual user = (Individual)_userRepository.GetUser(username, true);
            IndividualViewModel ind = new IndividualViewModel();
            ind.User =user ;

            return View(ind);
        }

        [Authorize]
        [HttpPost]
        public ActionResult EditUser(IndividualViewModel indiv)
        {
            if (indiv.file.ContentLength > 0)
            {
                var fileName = Path.GetFileName(indiv.file.FileName);
                var path = Path.Combine(Server.MapPath("~/Content/user"), fileName);
                indiv.file.SaveAs(path);
                Individual user = (Individual)_userRepository.GetUser(indiv.User.UserName, true);
                user.PhotoName = fileName;
            }

            return this.RedirectToAction("ViewUser", new { username = indiv.User.UserName});
        }

    }
}
