using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using Cook.WebApi.OAuth.Auth;

namespace Cook.WebApi.Filter
{
    /// <summary>
    /// 适用于post提交
    /// </summary>
    public class GetActionFilter : System.Web.Http.Filters.ActionFilterAttribute
    {
        /// <summary>
        /// 检查用户的Token有效性
        /// </summary>
        /// <returns></returns>
        protected CheckResult ValidateToken(string token)
        {
            try
            {
                AuthApi authApi = new AuthApi();
                CheckResult checkResult = authApi.ValidateToken(token);
                return checkResult;
            }
            catch (Exception)
            {
                // ignored
            }
            return null;
        }

        public override void OnActionExecuting(System.Web.Http.Controllers.HttpActionContext actionContext)
        {
            base.OnActionExecuting(actionContext);

            var queryParameters = actionContext.Request.GetQueryNameValuePairs().ToDictionary(x => x.Key, x => x.Value);
            var token = queryParameters.ContainsKey("token")
                        ? queryParameters["token"] : string.Empty;

            //打上CheckTokenFilterAttribute特性的资源，以下都不能为空，必须以授权形式访问资源
            if (string.IsNullOrEmpty(token))
            {
                TokenHandleUnauthorizedRequest(actionContext, "Token为空，您还没有登录");
            }

            //验证
            CheckResult checkResult = ValidateToken(token);//检验令牌的准确性
            if (!checkResult.Success)
            {
                TokenHandleUnauthorizedRequest(actionContext, "Token为无效，请重新登录");
            }
        }


        //自定义的异常处理
        protected void TokenHandleUnauthorizedRequest(System.Web.Http.Controllers.HttpActionContext actionContext, string msg)
        {
            var requestUrl = actionContext.RequestContext.Url.Request.RequestUri.ToString();

            var response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized, new { success = false, msg = msg, request_url = requestUrl });
            throw new System.Web.Http.HttpResponseException(response);

        }
    }
}