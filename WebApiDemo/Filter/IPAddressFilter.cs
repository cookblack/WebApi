using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Web;
using Cook.WebApi.Common;

namespace Cook.WebApi.Filter
{
    /// <summary>
    /// 适用于post提交
    /// </summary>
    public class IpAddressFilter : System.Web.Http.Filters.ActionFilterAttribute
    {
        /// <summary>
        /// 检查用户的Token有效性
        /// </summary>
        /// <returns></returns>
        protected bool ValidateIpAddress(string strIp)
        {
            try
            {
                string[] tmpLink = PlatformConfig.PushWhiteList.Split(';');
                if (tmpLink.Any(item => strIp == item))
                {
                    return true;
                }
            }
            catch (Exception)
            {
                // ignored
            }
            return false;
        }

        public override void OnActionExecuting(System.Web.Http.Controllers.HttpActionContext actionContext)
        {
            base.OnActionExecuting(actionContext);
            string clientIp = GetIpaddress();
            //检验ip地址合法性
            if (!ValidateIpAddress(clientIp))
            {
                TokenHandleUnauthorizedRequest(actionContext, "非法地址，请求无效。该地址为"+ clientIp);
            }
        }

        //自定义的异常处理
        protected void TokenHandleUnauthorizedRequest(System.Web.Http.Controllers.HttpActionContext actionContext, string msg)
        {
            var requestUrl = actionContext.RequestContext.Url.Request.RequestUri.ToString();

            var response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized, new { errCode = "1001", success = false, errMsg = msg, request_url = requestUrl });
            throw new System.Web.Http.HttpResponseException(response);

        }

        /// <summary>
        ///   获取IP地址
        /// </summary>
        /// <returns></returns>
        protected string GetIpaddress()
        {
            var result = HttpContext.Current.Request.ServerVariables["HTTP_CDN_SRC_IP"];
            if (String.IsNullOrEmpty(result))
                result = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];

            if (String.IsNullOrEmpty(result))
                result = HttpContext.Current.Request.UserHostAddress;

            if (String.IsNullOrEmpty(result) || !IsIp(result))
                return "127.0.0.1";

            return result;
        }
        public static bool IsIp(string ip)
        {
            return Regex.IsMatch(ip, "^((2[0-4]\\d|25[0-5]|[01]?\\d\\d?)\\.){3}(2[0-4]\\d|25[0-5]|[01]?\\d\\d?)$");
        }

    }

}