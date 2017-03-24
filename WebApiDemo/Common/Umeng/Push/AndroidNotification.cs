using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;

namespace CaseKey.Web.API.Common.Umeng.Push
{
    public class AndroidNotification : UmengNotification
    {
        // Keys can be set in the payload level
        protected static HashSet<string> PayloadKeys = new HashSet<string>() { "display_type" };

        // Keys can be set in the body level
        protected static HashSet<string> BodyKeys = new HashSet<string>(){
            "ticker", "title", "text", "builder_id", "icon", "largeIcon", "img", "play_vibrate", "play_lights", "play_sound",
            "sound", "after_open", "url", "activity", "custom"};

        /// <summary>
        /// 构造
        /// </summary>
        public AndroidNotification()
        {
            SetPredefinedKeyValue("production_mode", ProductionMode ? "true" : "false");
        }

        /// <summary>
        /// Set key/value in the RootJson, for the keys can be set please see RootKeys, PayloadKeys, 
        /// BodyKeys and PolicyKeys.
        /// 设置预定义的key/value
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
            else if (PayloadKeys.Contains(key))
            {
                // This key should be in the payload level
                JObject payloadJson = null;

                if (RootJson.Properties().Any(p => p.Name == "payload"))
                {
                    payloadJson = RootJson.GetValue("payload").ToObject<JObject>();
                }
                else
                {
                    payloadJson = new JObject();
                    RootJson.Add("payload", payloadJson);
                }
                payloadJson.Add(key, JToken.FromObject(value));
                //需要重新赋值,否则值设置不上
                RootJson.Property("payload").Value = payloadJson;
            }
            else if (BodyKeys.Contains(key))
            {
                // This key should be in the body level
                JObject bodyJson = null;
                JObject payloadJson = null;
                // 'body' is under 'payload', so build a payload if it doesn't exist
                if (RootJson.Properties().Any(p => p.Name == "payload"))
                {
                    payloadJson = RootJson.Property("payload").Value.ToObject<JObject>();
                }
                else
                {
                    payloadJson = new JObject();
                    RootJson.Add("payload", payloadJson);
                }
                // Get body JSONObject, generate one if not existed
                if (payloadJson.Properties().Any(p => p.Name == "body"))
                {
                    bodyJson = payloadJson.GetValue("body").ToObject<JObject>();
                }
                else
                {
                    bodyJson = new JObject();
                    payloadJson.Add("body", bodyJson);
                }
                bodyJson.Add(key, JToken.FromObject(value));
                //需要重新赋值,否则值设置不上
                payloadJson.Property("body").Value = bodyJson;
                RootJson.Property("payload").Value = payloadJson;

            }
            else if (PolicyKeys.Contains(key))
            {
                // This key should be in the body level
                JObject policyJson = null;
                if (RootJson.Properties().Any(p=>p.Name == "policy"))
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
                if (key == "payload" || key == "body" || key == "policy" || key == "extra")
                {
                    throw new Exception("You don't need to set value for " + key + " , just set values for the sub keys in it.");
                }
                else
                {
                    throw new Exception("Unknown key: " + key);
                }
            }
            return true;
        }

        /// <summary>
        /// Set extra key/value for Android notification
        /// 设置附加的参数 key value列表 可选 用户自定义key-value。只对"通知"
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool SetExtraField(string key, string value)
        {
            JObject payloadJson = null;
            JObject extraJson = null;
            if (RootJson.Properties().Any(p => p.Name == "payload"))
            {
                payloadJson =  RootJson.Property("payload").Value.ToObject<JObject>();
            }
            else
            {
                payloadJson = new JObject();
                RootJson.Add("payload", payloadJson);
            }

            if (payloadJson.Properties().Any(p => p.Name == "extra"))
            {
                extraJson = payloadJson.Property("extra").Value.ToObject<JObject>();
            }
            else
            {
                extraJson = new JObject();
                payloadJson.Add("extra", extraJson);
            }
            extraJson.Add(key, value);
            //需要重新赋值,否则值设置不上
            payloadJson.Property("extra").Value = extraJson;
            RootJson.Property("payload").Value = payloadJson;
            return true;
        }
    }
}
