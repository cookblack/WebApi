using System;
using System.IO;
using System.Net;
using System.Text;

namespace Cook.WebApi.Common
{
    /// <summary>
    /// 模拟网络提交
    /// </summary>
    public class HttpRequestHelper
    {
        /// <summary>
        /// Post 提交
        /// </summary>
        /// <param name="url">提交的地址</param>
        /// <param name="paramsValue">参数</param>
        /// <param name="typeApplication">参数协议</param>
        /// <returns></returns>
        public static string HttpPostMath(string url, string paramsValue,string typeApplication="json")
        {
            string result;
            byte[] byteArray = Encoding.UTF8.GetBytes(paramsValue);
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "POST";
            request.ContentType = typeApplication!= "json" ? "application/x-www-form-urlencoded" : "application/json";
           //request.Headers.Add("sign", "123");

            request.ContentLength = byteArray.Length;
            using (Stream newStream = request.GetRequestStream())
            {
                newStream.Write(byteArray, 0, byteArray.Length); //写入参数 
            }
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();//获取响应

            using (StreamReader sr = new StreamReader(response.GetResponseStream(), encoding: Encoding.UTF8))
            {
                result = sr.ReadToEnd();
            }
            return result;

        }

        public static string HttpGetMath(string url, string paramsValue)
        {
            string result = string.Empty;
            Uri uri = new Uri(url);
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri + "?" + paramsValue);
            request.Method = "Get";
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream myResponseStream = response.GetResponseStream();
            StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.GetEncoding("utf-8"));
            string retString = myStreamReader.ReadToEnd();
            myStreamReader.Close();
            myResponseStream?.Close();
            return result;
        }
    }
}