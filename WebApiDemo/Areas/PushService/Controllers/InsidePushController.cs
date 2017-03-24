using System;
using System.Collections.Generic;
using System.Web.Http;
using Cook.WebApi.Common;
using Cook.WebApi.Controllers;
using Cook.WebApi.Filter;
using Newtonsoft.Json.Linq;

namespace Cook.WebApi.Areas.PushService.Controllers
{
    /// <summary>
    /// 内部系统推送接口
    /// </summary>
    public class InsidePushController  : BaseApiController
    {
        //配置信息
        private readonly string _appkeyAndroid = PlatformConfig.AppkeyAndroid;
        private readonly string _appMasterSecretAndroid = PlatformConfig.MasterSecretAndroid;

        private readonly string _appkeyIos = PlatformConfig.AppkeyIos;
        private readonly string _appMasterSecretIos = PlatformConfig.MasterSecretIos;

        #region Android

        /// <summary>
        /// Android消息内部推送 alias
        /// </summary>
        /// <param name="jParams">参数</param>
        /// <returns></returns>
        [HttpPost]
        [IpAddressFilter]
        [Route("api/androidInsidePushAliasSrv")]
        public Dictionary<string, object> AndroidInsidePushAliasSrv(JObject jParams)
        {
            var state = false;
            JObject retstring = new JObject();
            //参数
            dynamic dParam = jParams;
            //string device_tokens = Convert.ToString(dParam.device_tokens);//device_tokens
            string alias = Convert.ToString(dParam.alias);//device_tokens
            string timestamp = !string.IsNullOrEmpty(Convert.ToString(dParam.timestamp)) ? Convert.ToString(dParam.timestamp) : DateTimeHelper.ConverDateTimeToJavaMillSecond(DateTime.Now).ToString();//必填 时间戳，10位或者13位均可，时间戳有效期为10分钟
            string @enum = Convert.ToString(dParam.type);//消息发送类型 unicast-单播 listcast-列播(要求不超过500个device_token) broadcast-广播
            string ticker = Convert.ToString(dParam.ticker);//必填 通知栏提示文字
            string title = Convert.ToString(dParam.title); //必填 通知标题
            string text = Convert.ToString(dParam.text);// 必填 通知文字描述 
            string afterOpen = !string.IsNullOrEmpty(Convert.ToString(dParam.after_open)) ? Convert.ToString(dParam.after_open) : "go_app"; //必填 点击"通知"的后续行为，默认为打开app。
            switch (@enum)
            {
                case "unicast":
                    //单播
                    AndroidUnicast unicast = new AndroidUnicast();
                    unicast.SetAppMasterSecret(_appMasterSecretAndroid);
                    unicast.SetPredefinedKeyValue("appkey", _appkeyAndroid);
                    unicast.SetPredefinedKeyValue("timestamp", timestamp);
                    //unicast.SetPredefinedKeyValue("device_tokens", device_tokens);
                    unicast.SetPredefinedKeyValue("alias", alias);
                    unicast.SetPredefinedKeyValue("ticker", ticker);
                    unicast.SetPredefinedKeyValue("title", title);
                    unicast.SetPredefinedKeyValue("text", text);
                    unicast.SetPredefinedKeyValue("after_open", afterOpen);
                    unicast.SetPredefinedKeyValue("display_type", "notification");
                    state = unicast.Send(out retstring);
                    break;
                case "listcast":
                    //列播
                    AndroidListcast listcast = new AndroidListcast();
                    listcast.SetAppMasterSecret(_appMasterSecretAndroid);
                    listcast.SetPredefinedKeyValue("appkey", _appkeyAndroid);
                    listcast.SetPredefinedKeyValue("timestamp", timestamp);
                    listcast.SetPredefinedKeyValue("alias", alias);
                    listcast.SetPredefinedKeyValue("ticker", ticker);
                    listcast.SetPredefinedKeyValue("title", title);
                    listcast.SetPredefinedKeyValue("text", text);
                    listcast.SetPredefinedKeyValue("after_open", afterOpen);
                    listcast.SetPredefinedKeyValue("display_type", "notification");
                    state = listcast.Send(out retstring);
                    break;
                case "broadcast":
                    //广播
                    AndroidBroadcast broadcast = new AndroidBroadcast();
                    broadcast.SetAppMasterSecret(_appMasterSecretAndroid);
                    broadcast.SetPredefinedKeyValue("appkey", _appkeyAndroid);
                    broadcast.SetPredefinedKeyValue("timestamp", timestamp);
                    broadcast.SetPredefinedKeyValue("alias", alias);
                    broadcast.SetPredefinedKeyValue("ticker", ticker);
                    broadcast.SetPredefinedKeyValue("title", title);
                    broadcast.SetPredefinedKeyValue("text", text);
                    broadcast.SetPredefinedKeyValue("after_open", afterOpen);
                    broadcast.SetPredefinedKeyValue("display_type", "notification");
                    state = broadcast.Send(out retstring);
                    break;
            }
            var result = new Dictionary<string, object>()
            {
                {"cmd", "androidInsidePushAliasSrv"},
                {"errCode", state?ConfigFile.StatusCode.操作成功:ConfigFile.StatusCode.操作失败},
                {"status", state},
                {"content", retstring}
            };
            return result;
        }

        #endregion

        #region IOS

        /// <summary>
        /// IOS消息内部推送 alias
        /// </summary>
        /// <param name="jParams">参数</param>
        /// <returns></returns>
        [HttpPost]
        [IpAddressFilter]
        [Route("api/iosInsidePushAliasSrv")]
        public Dictionary<string, object> IosInsidePushAliasSrv(JObject jParams)
        {
            var state = false;
            JObject retstring = new JObject();
            //参数
            dynamic dParam = jParams;
            string alias = Convert.ToString(dParam.alias);//alias
            string timestamp = !string.IsNullOrEmpty(Convert.ToString(dParam.timestamp)) ? Convert.ToString(dParam.timestamp) : DateTimeHelper.ConverDateTimeToJavaMillSecond(DateTime.Now);//必填 时间戳，10位或者13位均可，时间戳有效期为10分钟
            string @enum = Convert.ToString(dParam.type);//消息发送类型 unicast-单播 listcast-列播(要求不超过500个device_token) broadcast-广播
            string ticker = Convert.ToString(dParam.ticker);//必填 通知栏提示文字
            string title = Convert.ToString(dParam.title); //必填 通知标题
            string text = Convert.ToString(dParam.text);// 必填 通知文字描述 
            string afterOpen = !string.IsNullOrEmpty(Convert.ToString(dParam.after_open)) ? Convert.ToString(dParam.after_open) : "go_app"; //必填 点击"通知"的后续行为，默认为打开app。
            switch (@enum)
            {
                case "unicast":
                    //单播
                    IosUnicast unicast = new IosUnicast();
                    unicast.SetAppMasterSecret(_appMasterSecretIos);
                    unicast.SetPredefinedKeyValue("appkey", _appkeyIos);
                    unicast.SetPredefinedKeyValue("timestamp", timestamp);
                    unicast.SetPredefinedKeyValue("alias", alias);
                    unicast.SetPredefinedKeyValue("ticker", ticker);
                    unicast.SetPredefinedKeyValue("title", title);
                    unicast.SetPredefinedKeyValue("text", text);
                    unicast.SetPredefinedKeyValue("after_open", afterOpen);
                    unicast.SetPredefinedKeyValue("display_type", "notification");
                    state = unicast.Send(out retstring);
                    break;
                case "listcast":
                    //列播
                    IosListcast listcast = new IosListcast();
                    listcast.SetAppMasterSecret(_appMasterSecretIos);
                    listcast.SetPredefinedKeyValue("appkey", _appkeyIos);
                    listcast.SetPredefinedKeyValue("timestamp", timestamp);
                    listcast.SetPredefinedKeyValue("alias", alias);
                    listcast.SetPredefinedKeyValue("ticker", ticker);
                    listcast.SetPredefinedKeyValue("title", title);
                    listcast.SetPredefinedKeyValue("text", text);
                    listcast.SetPredefinedKeyValue("after_open", afterOpen);
                    listcast.SetPredefinedKeyValue("display_type", "notification");
                    state = listcast.Send(out retstring);
                    break;
                case "broadcast":
                    //广播
                    IosBroadcast broadcast = new IosBroadcast();
                    broadcast.SetAppMasterSecret(_appMasterSecretIos);
                    broadcast.SetPredefinedKeyValue("appkey", _appkeyIos);
                    broadcast.SetPredefinedKeyValue("timestamp", timestamp);
                    broadcast.SetPredefinedKeyValue("alias", alias);
                    broadcast.SetPredefinedKeyValue("ticker", ticker);
                    broadcast.SetPredefinedKeyValue("title", title);
                    broadcast.SetPredefinedKeyValue("text", text);
                    broadcast.SetPredefinedKeyValue("after_open", afterOpen);
                    broadcast.SetPredefinedKeyValue("display_type", "notification");
                    state = broadcast.Send(out retstring);
                    break;
            }
            var result = new Dictionary<string, object>()
            {
                {"cmd", "iosPushSrv"},
                {"errCode", state?ConfigFile.StatusCode.操作成功:ConfigFile.StatusCode.操作失败},
                {"status", state},
                {"content", retstring}
            };
            return result;
        }

        #endregion
    }
}
