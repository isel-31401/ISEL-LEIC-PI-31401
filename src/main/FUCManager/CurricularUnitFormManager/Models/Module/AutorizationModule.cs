using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Principal;
using System.Web.Security;
using CurricularUnitFormManager.Models.Repository;

namespace CurricularUnitFormManager.Models.Module
{
    public class AutorizationModule : IHttpModule
    {
        public void Dispose(){}

        public void Init(HttpApplication context)
        {
            context.AuthenticateRequest += new EventHandler(OnAuthenticateRequest);
        }
        public void OnAuthenticateRequest(Object source, EventArgs args)
        {

            //custom Authentication logic can go here
            var app = source as HttpApplication;

            HttpRequest req = app.Context.Request;
            HttpResponse rep = app.Context.Response;

            //empty cookie goes to Login
            if (req.Cookies.Count == 0)
            {
                //set temp cookie
                string publicName = "public";
                HttpCookie c = new HttpCookie("public", publicName);
                c.Expires = DateTime.Now.AddMinutes(30);
                HttpContext.Current.Response.Cookies.Add(c);
                HttpContext.Current.User = new GenericPrincipal(new GenericIdentity("public"), new[] { "public" });

                //redirect to base
                rep.Redirect("/");
            }
            //get cookie
            HttpCookie cookie = req.Cookies["user"];
            if (cookie != null)
            {
                //desencriptacao
                string userName = cookie.Value;
                MembershipUser user = IndividualRepository.Instance.GetUser(userName, true);
                //User u = UserLocator.findUser(userName);
                if (user != null)
                    HttpContext.Current.User = new GenericPrincipal(new GenericIdentity(userName), ((Individual.Individual)user).getRoles());
            }

        }
    }
}