﻿using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using CaseKey.Common;
using log4net;
using Newtonsoft.Json.Linq;

namespace CaseKey.Web.API.Common.Umeng.Push.android
{
    public class AndroidFilecast : AndroidNotification
    {
        public AndroidFilecast()
        {
            try
            {
                SetPredefinedKeyValue("type", "filecast");
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(ex.ToString());
            }
        }


        // Upload file with device_tokens to Umeng
        public string UploadContents(string contents)
        {
            if (RootJson.Properties().All(p => p.Name != "appkey") || RootJson.Properties().All(p => p.Name != "timestamp"))
            {
                throw new Exception("appkey, timestamp needs to be set.");
            }

            // Construct the json string
            JObject uploadJson = new JObject
            {
                {"appkey", RootJson.GetValue("appkey")},
                {"timestamp", RootJson.GetValue("timestamp")},
                {"content", contents}
            };

            // Construct the request
            string url = Host + UploadPath;
            string postBody = uploadJson.ToString();
            string sign = CommonContext.MD5Encrupt("POST" + url + postBody + AppMasterSecret).ToLower();
            url = url + "?sign=" + sign;


            var request = (HttpWebRequest)WebRequest.Create(url);

            request.Method = "POST";
            request.UserAgent = UserAgent;
            request.Timeout = 2 * 60 * 1000; //超时时间设置为两分钟
            //request.ContentType = "application/json";
            //request.Headers.Set("Pragma", "no-cache");

            byte[] postData = Encoding.UTF8.GetBytes(postBody);
            string retString;
            using (var requestStream = request.GetRequestStream())
            {
                requestStream.Write(postData, 0, postData.Length);
                using (var response = request.GetResponse())
                {
                    using (var responseStream = response.GetResponseStream())
                    {
                        using (StreamReader myStreamReader = new StreamReader(responseStream, Encoding.GetEncoding("utf-8")))
                        {
                            retString = myStreamReader.ReadToEnd();
                            //ret = responseStream.ReadBytes();
                        }
                    }
                }
            }

            try
            {

                JObject jObject = JObject.Parse(retString);
                string result = jObject.Property("ret").Value.ToString();
                if (result.Equals("SUCCESS", StringComparison.OrdinalIgnoreCase))
                {
                    string fileId = jObject.GetValue("data").ToObject<JObject>().GetValue("file_id").ToString();
                    SetPredefinedKeyValue("file_id", fileId);
                    return fileId;
                }
                else
                {
                    LogHelper.WriteLog("调用友盟发送失败");
                    LogHelper.WriteLog(retString);
                    throw new Exception("Failed to upload file");
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(ex.ToString());
                throw ex;
            }

        }


    }
}
