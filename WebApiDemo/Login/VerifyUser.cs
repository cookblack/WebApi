using Cook.WebApi.OAuth.Auth;

namespace Cook.WebApi.Login
{
    /// <summary>
    /// 检验用户
    /// </summary>
    public class VerifyUser
    {
        /// <summary>
        /// 检验用户名和密码准备性
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <param name="pwd">密码</param>
        /// <returns></returns>
        public static CommonResult VerifyUserPwd(string userName, string pwd)
        {
            CommonResult commonResult = new CommonResult();
            string userId ;
//            if (UserBal.ChecKoutUser(userName, pwd, out userId))
//            {
//                commonResult.Success = true;
//                commonResult.UserId = userId;
//                commonResult.UserName = userName;
//                return commonResult;
//            }
//            else
//            {
//                commonResult.Success = false;
//                commonResult.UserId = "";
//                return commonResult;
//            }
            return commonResult;
        }

        /// <summary>
        /// 检验第三方key准确性
        /// </summary>
        /// <param name="key">第三方key</param>
        /// <param name="thirdtype">key类型</param>
        /// <returns></returns>
        public static CommonResult VerifyThirdKey(string key, string thirdtype)
        {
            CommonResult commonResult = new CommonResult();
            string userId;
            //            if (UserBal.ChecKoutUserKey(key, thirdtype, out userId))
            //            {
            //                commonResult.Success = true;
            //                commonResult.UserId = userId;
            //                return commonResult;
            //            }
            //            else
            //            {
            //                commonResult.Success = false;
            //                commonResult.UserId = "";
            //                return commonResult;
            //            }
            return commonResult;
        }
    }
}