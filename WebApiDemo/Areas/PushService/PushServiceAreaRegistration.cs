using System.Web.Mvc;
using Cook.WebApi.MvcBase;

namespace Cook.WebApi.Areas.PushService
{
    public class PushServiceAreaRegistration : CustomAreaRegistration
    {
        public override string AreaName => "PushService";
    }
}