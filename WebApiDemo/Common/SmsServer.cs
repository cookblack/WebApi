namespace Cook.WebApi.Common
{
    /// <summary>
    /// 短信服务类
    /// </summary>
    public class SmsServer
    {
        /// <summary>
        /// 发送短信验证码
        /// </summary>
        /// <param name="phoneNo"></param>
        /// <param name="smsStr"></param>
        /// <returns></returns>
        public static bool GetSmsNote(string phoneNo,string smsStr)
        {
            var stats = false;
            if (phoneNo != null)
            {
                //todo 第三方短信接口方法 后期具体实现
                var ff = smsStr;
                stats = true;
            }

            return stats;
        }


    }
}