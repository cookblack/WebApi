using System;
using System.Net;
using System.Net.Http;
using System.Web;
using Cook.WebApi.OAuth.Auth;

namespace Cook.WebApi.Filter
{
    /// <summary>
    /// 适用于post提交
    /// </summary>
    public class PostActionFilter : System.Web.Http.Filters.ActionFilterAttribute
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

            HttpContextBase context = (HttpContextBase)actionContext.Request.Properties["MS_HttpContext"];//获取传统context
            HttpRequestBase request = context.Request;//定义传统request对象
            string token = request.Params["token"];

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

            var response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized, new { errCode="1001", success = false, errMsg = msg, request_url = requestUrl });
            throw new System.Web.Http.HttpResponseException(response);

        }
    }
}