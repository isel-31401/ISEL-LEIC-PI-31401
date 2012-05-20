using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace CurricularUnitFormManager
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            
            routes.MapRoute(
                "Proposal2", // Route name
                "CUF/Proposal/{action}/{acr}", // URL with parameters
                new { controller = "Proposal", action = "Index", acr = UrlParameter.Optional }//, // Parameter defaults
                //new { id = @"\d+" } //parameters restritions
            );

            routes.MapRoute(
                "Proposal", // Route name
                "CUF/Proposal/{action}/{acr}/{id}", // URL with parameters
                new { controller = "Proposal", action = "Index", acr = UrlParameter.Optional, id = UrlParameter.Optional }//, // Parameter defaults
                //new { id = @"\d+" } //parameters restritions
            );

//            routes.MapRoute(
//    "Proposal3", // For Cancel Accept
//    "CUF/Proposal/{action}", // URL with parameters
//    new { controller = "Proposal", action = "Cancel" }//, // Parameter defaults
//                //new { id = @"\d+" } //parameters restritions
//);

            routes.MapRoute(
                "Official", // Route name
                "CUF/Official/{action}/{acr}", // URL with parameters
                //new { controller = "Official", action = "Index", acr = UrlParameter.Optional, id = UrlParameter.Optional }, // Parameter defaults
                new { controller = "Official", action = "Index", acr = UrlParameter.Optional }//, // Parameter defaults
                //new { acr = @"^[A-Za-z]\d+" } //parameters restritions
            );

            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{acr}", // URL with parameters
                new { controller = "Home", action = "Index", acr = UrlParameter.Optional } // Parameter defaults
            );

        }

        protected void Application_Start()
        {
            // estabelece o path para o xml como sendo caminho do nosso servidor
            Environment.CurrentDirectory = HttpContext.Current.Server.MapPath("/");

            AreaRegistration.RegisterAllAreas();

            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);
            //Load UserDatabase
            Models.Individual.UserLoad.Start();
            Models.Repository.IndividualRepository.Start();
            Models.Repository.CurricularUnitFormRepository.Start();
        }
    }
}