using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using Cook.WebApi.OAuth.Auth;

namespace Cook.WebApi.Filter
{
    /// <summary>
    /// 令牌验证过滤器
    /// </summary>
    public class CheckTokenFilterAttribute : System.Web.Http.AuthorizeAttribute
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

        //重写授权
        public override void OnAuthorization(System.Web.Http.Controllers.HttpActionContext actionContext)
        {
            //以下针对于需要授权访问的资源来说，不需要授权的就不打标签就可以

            //获取当前访问资源用户的信息， 当前用户类型是否能访问该资源

            var queryParameters = actionContext.Request.GetQueryNameValuePairs().ToDictionary(x => x.Key, x => x.Value);
            var accessToken = queryParameters.ContainsKey("token")
                        ? queryParameters["token"] : string.Empty;

            //打上CheckTokenFilterAttribute特性的资源，以下都不能为空，必须以授权形式访问资源
            if (string.IsNullOrEmpty(accessToken))
            {
                TokenHandleUnauthorizedRequest(actionContext, "Token为空，您还没有登录");
            }

            //验证
            CheckResult checkResult = ValidateToken(accessToken);//检验令牌的准确性
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