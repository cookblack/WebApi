using System.Web.Http;
using System.Web.Mvc;

namespace Cook.WebApi.MvcBase
{
    public class CustomAreaRegistration : AreaRegistration
    {
        public override string AreaName => "Custom";

        /// <summary>
        /// 使用自定义的映射路径地址
        /// </summary>
        /// <param name="context"></param>
        public override void RegisterArea(AreaRegistrationContext context)
        {
            var areaName = AreaName;

            context.MapRoute(
                areaName + "_area",
                "api/" + areaName + "/{controller}/{action}/{id}",
                new { area = areaName, action = "index", id = RouteParameter.Optional },
                 namespaces: new string[] { "CaseKey.Web.API.Areas." + areaName + ".Controllers" }
            );
            context.MapRoute(
                 areaName + "_default",
                 areaName + "api/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );

        }

    }
}