using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Cook.WebApi.OAuth.Auth;

namespace Cook.WebApi.Controllers
{
    public class BaseApiController : ApiController
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
    }
}
