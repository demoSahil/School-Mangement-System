using System.Web.Mvc;

namespace SMS_WEBUI.Areas.A_Students
{
    public class A_StudentsAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "A_Students";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "A_Students_default",
                "{area}/{controller}/{action}/{id}",
               new {area="A_Students",controller="Student" ,action = "Index", id = UrlParameter.Optional },
                namespaces: new string[] { "SMS_WEBUI.Areas.A_Students.Controllers" }
            ); 
        }
    }
}