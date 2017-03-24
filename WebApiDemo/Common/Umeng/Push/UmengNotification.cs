using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using CaseKey.Common;
using log4net;
using Newtonsoft.Json.Linq;

namespace CaseKey.Web.API.Common.Umeng.Push
{
    /// <summary>
    /// 友盟推送配置基础类
    /// </summary>
    public abstract class UmengNotification
    {

        /// <summary>
        /// 是否调试模式
        /// </summary>
        protected static bool ProductionMode = bool.Parse(PlatformConfig.ProductionMode);


        /// <summary>
        ///这个JSONObject用于构建整个请求字符串
        /// </summary>
        protected JObject RootJson = new JObject();

        /// <summary>
        ///该对象用于发送post请求“友盟”
        /// </summary>
        protected WebClient Client = new WebClient();

        /// <summary>
        /// 地址
        /// </summary>
        protected const string Host = "http://msg.umeng.com";

        /// <summary>
        /// 上传路径
        /// </summary>
        protected const string UploadPath = "/upload";

        /// <summary>
        ///提交路径
        /// </summary>
        protected const string PostPath = "/api/send";

        /// <summary>
        /// 应用程序主秘密
        /// </summary>
        protected string AppMasterSecret;

        /// <summary>
        /// // The user agent
        /// </summary>
        protected string UserAgent = "Mozilla/5.0";

        /// <summary>
        ///Keys can be set in the root level 键可以设置在根水平
        /// </summary>
        protected static HashSet<string> RootKeys = new HashSet<string>(){
            "appkey", "timestamp", "type", "device_tokens", "alias", "alias_type", "file_id",
            "filter", "production_mode", "feedback", "description", "thirdparty_id"};

        /// <summary>
        ///Keys can be set in the policy level 键可以设置在政策层面
        /// </summary>
        protected static HashSet<string> PolicyKeys = new HashSet<string>() { "start_time", "expire_time", "max_send_num", "out_biz_no" };

        /// <summary>
        /// Set predefined keys in the RootJson, for extra keys(Android) or customized keys(IOS) please   RootJson预定义键集,额外键(Android)或自定义键(IOS)请
        // refer to corresponding methods in the subclass.  参考相应的子类中的方法
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public abstract bool SetPredefinedKeyValue(string key, object value);

        /// <summary>
        /// 设置AppMasterSecret
        /// </summary>
        /// <param name="secret"></param>
        public void SetAppMasterSecret(string secret)
        {
            AppMasterSecret = secret;
        }

        /// <summary>
        /// 发送请求
        /// </summary>
        /// <returns></returns>
        public bool Send(out JObject resJson)
        {
            string url = Host + PostPath;
            string postBody = RootJson.ToString();
            string sign = CommonContext.Md5Utf8Encrupt("POST" + url + postBody + AppMasterSecret).ToLower();

            url = url + "?sign=" + sign;

            var request = (HttpWebRequest)WebRequest.Create(url);

            request.Method = "POST";
            request.UserAgent = UserAgent;
            request.Timeout = 2 * 60 * 1000; //超时时间设置为两分钟
            request.ContentType = "application/json";
            //request.Headers.Set("Pragma", "no-cache");

            byte[] postData = Encoding.UTF8.GetBytes(postBody);//参数列表
            string retstring;

            try
            {
                using (var requestStream = request.GetRequestStream())
                {
                    requestStream.Write(postData, 0, postData.Length);
                    using (var response = request.GetResponse())
                    {
                        using (var responseStream = response.GetResponseStream())
                        {
                            using (StreamReader myStreamReader = new StreamReader(responseStream, Encoding.GetEncoding("utf-8")))
                            {
                                retstring = myStreamReader.ReadToEnd();
                            }
                        }
                    }
                }
            }
            catch (WebException wEx)
            {
                using (var res = (HttpWebResponse)wEx.Response)
                {
                    using (StreamReader sr = new StreamReader(res.GetResponseStream(), Encoding.GetEncoding("utf-8")))
                    {
                        retstring = sr.ReadToEnd();
                        LogHelper.WriteLog("发送失败,Status:" + res.StatusCode);
                    }
                }
            }
            try
            {

                JObject jObject = JObject.Parse(retstring);
                string result = jObject.Property("ret").Value.ToString();
                resJson = jObject;
                if (result.Equals("SUCCESS", StringComparison.OrdinalIgnoreCase))
                {
                    LogHelper.WriteLog("调用友盟发送成功");
                }
                else
                {
                    LogHelper.WriteLog("调用友盟发送失败:" + retstring);
                    return false;
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(ex.ToString());
                resJson = new JObject();
                return false;
            }
            return true;
        }

    }
}
