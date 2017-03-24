using System;
using System.Collections.Generic;
using Cook.WebApi.Common;
using Newtonsoft.Json.Linq;

namespace Cook.WebApi.OAuth.Auth
{
    /// <summary>
    /// 系统认证等基础接口实现
    /// </summary>
    public class AuthApi : BaseApi, IAuthApi
    {
        /// <summary>
        /// 加密私密钥匙
        /// </summary>
        private readonly string _sharedKey;

        /// <summary>
        /// 客户端与服务端时间相差合理时间 时间戳常量  
        /// </summary>
        private readonly double _timspanExpiredMinutes;

        /// <summary>
        /// 令牌默认有效时间，天数
        /// </summary>
        private readonly int _expiredDays;

        /// <summary>
        /// JWT的签发者
        /// </summary>
        private readonly string _iss;
        public AuthApi()
        {
            _iss = "QSQY";
            _sharedKey = "f08bc0ffe3043e2d";//加密钥匙
            _timspanExpiredMinutes = 100;//分钟数
            _expiredDays = 7;//天数
        }


        /// <summary>
        /// 检查应用接入的数据完整性
        /// 1）检查timestamp 与系统时间是否相差在合理时间内，如10分钟。
        /// 2）将appSecret、timestamp、nonce三个参数进行字典序排序
        /// 3）将三个参数字符串拼接成一个字符串进行SHA1加密
        /// 4）加密后的字符串可与signature对比，若匹配则标识该次请求来源于某应用端，请求是合法的。
        /// </summary>
        /// <param name="signature">加密签名内容</param>
        /// <param name="timestamp">时间戳</param>
        /// <param name="nonce">随机字符串</param>
        /// <param name="appid">应用接入Id</param>
        /// <returns></returns>
        public CheckResult ValidateSignature(string signature, string timestamp, string nonce, string appid)
        {
            CheckResult result = new CheckResult { Errmsg = "数据完整性检查不通过" };

            #region 校验签名参数的来源是否正确

            #region 加密后的字符串可与signature对比
            string[] arrTmp = { appid, timestamp, nonce };
            Array.Sort(arrTmp);
            string tmpStr = string.Join("", arrTmp);

//            tmpStr = EncryptHelper.HashString(tmpStr + ConstHelper.UnlockingKey, "MD5");
            tmpStr = CommonContext.MD5Encrupt(tmpStr + ConstHelper.UnlockingKey).ToLower();
            #endregion 

            if (tmpStr == signature && ValidateUtil.IsNumber(timestamp))
            {
                DateTime dtTime =  DateTimeHelper.StampToDateTime(timestamp);
                double minutes = DateTime.Now.Subtract(dtTime).TotalMinutes;
                if (minutes > _timspanExpiredMinutes)
                {
                    result.Errmsg = "签名时间戳失效";
                    result.Success = false;
                }
                else
                {
                    result.Errmsg = "检验成功";
                    result.Success = true;
                }
                #endregion
            }
            return result;
        }

        /// <summary>
        /// 注册用户获取访问令牌接口
        /// </summary>
        /// <param name="username">用户登录名称</param>
        /// <param name="password">用户密码</param>
        /// <param name="signature">加密签名字符串</param>
        /// <param name="timestamp">时间戳</param>
        /// <param name="nonce">随机数</param>
        /// <param name="appid">应用接入ID</param>
        /// <returns></returns>
        public TokenResult GetAccessToken(string username, string password,
            string signature, string timestamp, string nonce, string appid)
        {
            TokenResult token = new TokenResult();
            try
            {
                //先校验介入应用的数据完整性
                CheckResult checkResult = ValidateSignature(signature, timestamp, nonce, appid);//检查用户签名
                if (checkResult.Success)
                {
                    #region 检查用户身份
                    //检验数据完整性后，从数据库检查用户身份
                    CommonResult result = VerifyUser.VerifyUserPwd(username, password);//验证用户身份
                    if (result.Success)
                    {
                        //用户ID
                        string userId = result.UserId;
                        string userName = result.UserName;
                        int times = DateTimeHelper.DateTimeToStamp(DateTime.UtcNow);
                        int  expTimes= DateTimeHelper.DateTimeToStamp(DateTime.UtcNow.AddDays(_expiredDays));

                        var payload = new Dictionary<string, object>()
                        {
                            { "iss",_iss},//该JWT的签发者
                            { "sub",userId},//该JWT所面向的用户
                            { "aud",userName},//接收该JWT的一方
                            {"iat",times },//什么时候签发
                            { "exp",expTimes}//什么时候过期
                        };
                        //生成具体的Token和过期时间  生成JWT令牌
                        token.AccessToken = JsonWebToken.Encode(payload, _sharedKey, JwtHashAlgorithm.Hs256);
                        token.ExpiresIn = _expiredDays * 24 *3600;//过期时间
                        token.Success = true;
                        token.UserId = userId;
                        token.Errmsg = "生成令牌，过期时间为"+ expTimes;
                        //LogHelper.WriteLog("在"+ DateTime.UtcNow.ToString("yyyy-MMM-dd ss")+"生成" + userName+"令牌");
                    }
                    #endregion
                }
                else
                {
                    token.Errmsg = checkResult.Errmsg;
                    token.Success = checkResult.Success;
                }
            }

            catch (Exception ex)
            {
                Exception objExp = ex;
                //LogHelper.WriteLog("操作注册用户获取访问令牌接口", objExp);
            }
            return token;
        }

        /// <summary>
        /// 第三方注册用户获取访问令牌接口
        /// </summary>
        /// <param name="key">第三方加密钥匙</param>
        /// <param name="thirdtype">第三方登录类型</param>
        /// <param name="signature">加密签名字符串</param>
        /// <param name="timestamp">时间戳</param>
        /// <param name="nonce">随机数</param>
        /// <param name="appid">应用接入ID</param>
        /// <returns></returns>
        public TokenResult ThirdPartyGetAccessToken(string key, string thirdtype,
            string signature, string timestamp, string nonce, string appid)
        {
            TokenResult token = new TokenResult();
            try
            {
                //先校验介入应用的数据完整性
                CheckResult checkResult = ValidateSignature(signature, timestamp, nonce, appid);//检查用户签名
                if (checkResult.Success)
                {
                    #region 检查用户身份
                    //检验数据完整性后，从数据库检查用户身份
                    CommonResult result = VerifyUser.VerifyThirdKey(key, thirdtype);//验证用户身份
                    if (result.Success)
                    {
                        //用户ID
                        string userId = result.UserId;
                        string userName = result.UserName;
                        int times = DateTimeHelper.DateTimeToStamp(DateTime.UtcNow);
                        int expTimes = DateTimeHelper.DateTimeToStamp(DateTime.UtcNow.AddDays(_expiredDays));

                        var payload = new Dictionary<string, object>()
                        {
                            { "iss",_iss},//该JWT的签发者
                            { "sub",userId},//该JWT所面向的用户
                            { "aud",userName},//接收该JWT的一方
                            {"iat",times },//什么时候签发
                            { "exp",expTimes}//什么时候过期
                        };
                        //生成具体的Token和过期时间  生成JWT令牌
                        token.AccessToken = JsonWebToken.Encode(payload, _sharedKey, JwtHashAlgorithm.Hs256);
                        token.ExpiresIn = _expiredDays * 24 * 3600;//过期时间
                        token.Success = true;
                        token.UserId = userId;
                        token.Errmsg = "生成令牌，过期时间为" + expTimes;
                        //LogHelper.WriteLog("在"+ DateTime.UtcNow.ToString("yyyy-MMM-dd ss")+"生成" + userName+"令牌");
                    }
                    #endregion
                }
                else
                {
                    token.Errmsg = checkResult.Errmsg;
                    token.Success = checkResult.Success;
                }
            }

            catch (Exception ex)
            {
                Exception objExp = ex;
                //LogHelper.WriteLog("操作注册用户获取访问令牌接口", objExp);
            }
            return token;
        }
        /// <summary>
        /// 检查用户的Token有效性
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public CheckResult ValidateToken(string token)
        {
            //返回的结果对象
            CheckResult result = new CheckResult { Errmsg = "令牌检查不通过",Success = false};

            if (!string.IsNullOrEmpty(token))
            {
                try
                {
                    string decodedJwt = JsonWebToken.Decode(token, _sharedKey);
                    if (!string.IsNullOrEmpty(decodedJwt))
                    {
                        #region 检查令牌对象内容
                        dynamic root = JObject.Parse(decodedJwt);
                        string username = root.aud;
                        string userid = root.sub;
                        int jwtcreated = (int)root.iat;

                        //检查令牌的有效期，7天内有效
                        int timestamp =DateTimeHelper.DateTimeToStamp(DateTime.UtcNow);
                        TimeSpan ts = DateTimeHelper.StampToDateTime(Convert.ToString(jwtcreated)) - DateTimeHelper.StampToDateTime(Convert.ToString(timestamp));
                        int differDays = ts.Days;
                        if (differDays > _expiredDays)
                        {
                            result.Success = false;
                            result.Errmsg = "用户令牌失效.";
                            return result;
                            //throw new ArgumentException("用户令牌失效.");
                        }

                        //成功校验
                        result.Success = true;
                        result.Errmsg = "令牌检查通过";
                        result.Userid = userid;
                        result.UserName = username;

                        #endregion
                    }
                }
                catch (Exception ex)
                {
                    Exception objExp = ex;
                    //LogHelper.WriteLog("操作检查用户的Token有效性", objExp);
                }
            }
            return result;
        }
    }
}
