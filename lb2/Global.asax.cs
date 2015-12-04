using lb2.Models;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Web.Mvc;
using System.Web.Routing;

namespace lb2
{
    public class MvcApplication : System.Web.HttpApplication
    {
        public static bool logged
        {
            get; set;
        }        
        
        protected void Application_Start()
        {
            logged = false;
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            //Session["login"] = "no";
            //Session["role"] = "no";
            //Session["name"] = "no";
            Users.Open();
        }

        protected void Application_End()
        {
            Users.Save();
        }
    }
}
