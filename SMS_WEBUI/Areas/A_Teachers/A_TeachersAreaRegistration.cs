using System.Web.Mvc;

namespace SMS_WEBUI.Areas.A_Teachers
{
    public class A_TeachersAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "A_Teachers";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "A_Teachers_default",
                "A_Teachers/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional },
                namespaces: new string[] {"SMS_WEBUI.Areas.A_Teachers.Controllers"}
            );
        }
    }
}