using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;

namespace CaseKey.Web.API.Common.Umeng.Push
{
    public class IosNotification : UmengNotification
    {
        // Keys can be set in the aps level
        protected static HashSet<String> ApsKeys = new HashSet<String>() { "alert", "badge", "sound", "content-available" };


        /// <summary>
        /// 构造
        /// </summary>
        public IosNotification()
        {
            SetPredefinedKeyValue("production_mode", ProductionMode ? "true" : "false");
        }


        /// <summary>
        /// 设置参数到提交json中去
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public sealed override bool SetPredefinedKeyValue(string key, object value)
        {
            if (RootKeys.Contains(key))
            {
                // This key should be in the root level
                RootJson.Add(key, JToken.FromObject(value));
            }
            else if (ApsKeys.Contains(key))
            {
                // This key should be in the aps level
                JObject apsJson = null;
                JObject payloadJson = null;
                if (RootJson.Properties().Any(p => p.Name == "payload"))
                {
                    payloadJson = RootJson.Property("payload").Value.ToObject<JObject>();
                }
                else
                {
                    payloadJson = new JObject();
                    RootJson.Add("payload", payloadJson);
                }
                if (payloadJson.Properties().Any(p => p.Name == "aps"))
                {
                    apsJson = payloadJson.Property("aps").Value.ToObject<JObject>();
                }
                else
                {
                    apsJson = new JObject();
                    payloadJson.Add("aps", apsJson);
                }
                apsJson.Add(key, JToken.FromObject(value));
                //需要重新赋值,否则值设置不上
                payloadJson.Property("aps").Value = apsJson;
                RootJson.Property("payload").Value = payloadJson;

            }
            else if (PolicyKeys.Contains(key))
            {
                // This key should be in the body level
                JObject policyJson = null;
                if (RootJson.Properties().Any(p => p.Name == "policy"))
                {
                    policyJson = RootJson.Property("policy").Value.ToObject<JObject>();
                }
                else
                {
                    policyJson = new JObject();
                    RootJson.Add("policy", policyJson);
                }
                policyJson.Add(key, JToken.FromObject(value));
                //需要重新赋值,否则值设置不上
                RootJson.Property("policy").Value = policyJson;
            }
            else
            {
                if (key == "payload" || key == "aps" || key == "policy")
                {
                    throw new Exception("You don't need to set value for " + key + " , just set values for the sub keys in it.");
                }
                else
                {
                    throw new Exception("Unknownd key: " + key);
                }
            }

            return true;
        }


        /// <summary>
        /// Set customized key/value for IOS notification
        /// 设置自定义的key/value参数到提交的json字符串中
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool SetCustomizedField(string key, string value)
        {
            //RootJson.put(key, value);
            JObject payloadJson = null;
            if (RootJson.Properties().Any(p => p.Name == "payload"))
            {
                payloadJson = RootJson.Property("payload").Value.ToObject<JObject>();
            }
            else
            {
                payloadJson = new JObject();
                RootJson.Add("payload", payloadJson);
            }
            payloadJson.Add(key, value);
            //需要重新赋值,否则值设置不上
            RootJson.Property("payload").Value = payloadJson;
            return true;
        }
    }
}
