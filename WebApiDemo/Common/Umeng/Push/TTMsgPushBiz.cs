using System;
using CaseKey.Common;
using CaseKey.Web.API.Common.Umeng.Push.android;
using CaseKey.Web.API.Common.Umeng.Push.ios;
using log4net;
using Newtonsoft.Json.Linq;

namespace CaseKey.Web.API.Common.Umeng.Push
{
    /// <summary>
    /// 这里作为推送的对外接口使用
    /// </summary>
    public class TtMsgPushBiz
    {

        //配置信息
        private readonly string _appkeyAndroid = PlatformConfig.AppkeyAndroid;
        private readonly string _appMasterSecretAndroid = PlatformConfig.MasterSecretAndroid;

        private readonly string _appkeyIos = PlatformConfig.AppkeyIos;
        private readonly string _appMasterSecretIos = PlatformConfig.MasterSecretIos;


        private const string ParaKeyArticleId = "Id";
        private const string ParaKeyContentType = "ContentType";
        private const string ParaKeyTitle = "Title";


//        #region android测试
//        /// <summary>
//        /// 广播
//        /// </summary>
//        public bool SendAndroidBroadcast()
//        {
//            string timestamp =DateTimeHelper.DateTimeToStampString(DateTime.Now);
//            AndroidBroadcast broadcast = new AndroidBroadcast();
//            broadcast.SetAppMasterSecret(_appMasterSecretAndroid);
//            broadcast.SetPredefinedKeyValue("appkey", _appkeyAndroid);
//            broadcast.SetPredefinedKeyValue("timestamp", timestamp);
//            broadcast.SetPredefinedKeyValue("ticker", "Android broadcast ticker");
//            broadcast.SetPredefinedKeyValue("title", "中文的title");
//            broadcast.SetPredefinedKeyValue("text", "Android broadcast text");
//            broadcast.SetPredefinedKeyValue("after_open", "go_app");
//            broadcast.SetPredefinedKeyValue("display_type", "notification");
//            // TODO Set 'production_mode' to 'false' if it's a test device. 
//            // For how to register a test device, please see the developer doc.
//            //broadcast.SetPredefinedKeyValue("production_mode", "true");
//            // Set customized fields
//            broadcast.SetExtraField("test", "helloworld");
//            broadcast.Send();
//            return true;
//        }
//
//        /// <summary>
//        /// 单播
//        /// </summary>
//        public void SendAndroidUnicast()
//        {
//            AndroidUnicast unicast = new AndroidUnicast();
//            unicast.SetAppMasterSecret(_appMasterSecretAndroid);
//            unicast.SetPredefinedKeyValue("appkey", _appkeyAndroid);
//            unicast.SetPredefinedKeyValue("timestamp", DateTimeHelper.DateTimeToStampString(DateTime.Now));
//            // TODO Set your device token
//            unicast.SetPredefinedKeyValue("device_tokens", "xxxx");
//            unicast.SetPredefinedKeyValue("ticker", "Android unicast ticker");
//            unicast.SetPredefinedKeyValue("title", "中文的title");
//            unicast.SetPredefinedKeyValue("text", "Android unicast text");
//            unicast.SetPredefinedKeyValue("after_open", "go_app");
//            unicast.SetPredefinedKeyValue("display_type", "notification");
//            // TODO Set 'production_mode' to 'false' if it's a test device. 
//            // For how to register a test device, please see the developer doc.
//            //unicast.SetPredefinedKeyValue("production_mode", "true");
//            // Set customized fields
//            unicast.SetExtraField("test", "helloworld");
//            unicast.Send();
//        }
//
//        /// <summary>
//        /// 组播
//        /// </summary>
//        public void SendAndroidGroupcast()
//        {
//            AndroidGroupcast groupcast = new AndroidGroupcast();
//            groupcast.SetAppMasterSecret(_appMasterSecretAndroid);
//            groupcast.SetPredefinedKeyValue("appkey", _appkeyAndroid);
//            groupcast.SetPredefinedKeyValue("timestamp", DateTimeHelper.DateTimeToStampString(DateTime.Now));
//            /*  TODO
//             *  Construct the filter condition:
//             *  "where": 
//             *	{
//             *		"and": 
//             *		[
//             *			{"tag":"test"},
//             *			{"tag":"Test"}
//             *		]
//             *	}
//             */
//            JObject filterJson = new JObject();
//            JObject whereJson = new JObject();
//            JArray tagArray = new JArray();
//            JObject testTag = new JObject();
//            JObject TestTag = new JObject();
//            testTag.Add("tag", "test");
//            TestTag.Add("tag", "Test");
//            tagArray.Add(testTag);
//            tagArray.Add(TestTag);
//            whereJson.Add("and", tagArray);
//            filterJson.Add("where", whereJson);
//
//            groupcast.SetPredefinedKeyValue("filter", filterJson);
//            groupcast.SetPredefinedKeyValue("ticker", "Android groupcast ticker");
//            groupcast.SetPredefinedKeyValue("title", "中文的title");
//            groupcast.SetPredefinedKeyValue("text", "Android groupcast text");
//            groupcast.SetPredefinedKeyValue("after_open", "go_app");
//            groupcast.SetPredefinedKeyValue("display_type", "notification");
//            // TODO Set 'production_mode' to 'false' if it's a test device. 
//            // For how to register a test device, please see the developer doc.
//            //groupcast.SetPredefinedKeyValue("production_mode", "true");
//            groupcast.Send();
//        }
//
//        /// <summary>
//        /// 自定义
//        /// </summary>
//        public void SendAndroidCustomizedcast()
//        {
//            AndroidCustomizedcast customizedcast = new AndroidCustomizedcast();
//            customizedcast.SetAppMasterSecret(_appMasterSecretAndroid);
//            customizedcast.SetPredefinedKeyValue("appkey", _appkeyAndroid);
//            customizedcast.SetPredefinedKeyValue("timestamp", DateTimeHelper.DateTimeToStampString(DateTime.Now));
//            // TODO Set your alias here, and use comma to split them if there are multiple alias.
//            // And if you have many alias, you can also upload a file containing these alias, then 
//            // use file_id to Send customized notification.
//            customizedcast.SetPredefinedKeyValue("alias", "xx");
//            // TODO Set your alias_type here
//            customizedcast.SetPredefinedKeyValue("alias_type", "xx");
//            customizedcast.SetPredefinedKeyValue("ticker", "Android customizedcast ticker");
//            customizedcast.SetPredefinedKeyValue("title", "中文的title");
//            customizedcast.SetPredefinedKeyValue("text", "Android customizedcast text");
//            customizedcast.SetPredefinedKeyValue("after_open", "go_app");
//            customizedcast.SetPredefinedKeyValue("display_type", "notification");
//            // TODO Set 'production_mode' to 'false' if it's a test device. 
//            // For how to register a test device, please see the developer doc.
//            //customizedcast.SetPredefinedKeyValue("production_mode", "true");
//            customizedcast.Send();
//        }
//
//        /// <summary>
//        /// 文件
//        /// </summary>
//        public void SendAndroidFilecast()
//        {
//            AndroidFilecast filecast = new AndroidFilecast();
//            filecast.SetAppMasterSecret(_appMasterSecretAndroid);
//            filecast.SetPredefinedKeyValue("appkey", _appkeyAndroid);
//            filecast.SetPredefinedKeyValue("timestamp", DateTimeHelper.DateTimeToStampString(DateTime.Now));
//            // TODO upload your device tokens, and use '\n' to split them if there are multiple tokens 
//            filecast.UploadContents("aa" + "\n" + "bb");
//            filecast.SetPredefinedKeyValue("ticker", "Android filecast ticker");
//            filecast.SetPredefinedKeyValue("title", "中文的title");
//            filecast.SetPredefinedKeyValue("text", "Android filecast text");
//            filecast.SetPredefinedKeyValue("after_open", "go_app");
//            filecast.SetPredefinedKeyValue("display_type", "notification");
//            filecast.Send();
//        }
//
//        #endregion
//
//
//        #region ios测试
//        /// <summary>
//        /// ios 组播
//        /// </summary>
//        public void SendIosBroadcast()
//        {
//            IosBroadcast broadcast = new IosBroadcast();
//            broadcast.SetAppMasterSecret(_appMasterSecretIos);
//            broadcast.SetPredefinedKeyValue("appkey", _appkeyIos);
//            broadcast.SetPredefinedKeyValue("timestamp", DateTimeHelper.DateTimeToStampString(DateTime.Now));
//
//            broadcast.SetPredefinedKeyValue("alert", "IOS 广播测试");
//            broadcast.SetPredefinedKeyValue("badge", 0);
//            broadcast.SetPredefinedKeyValue("sound", "chime");
//            // TODO set 'production_mode' to 'true' if your app is under production mode
//            //broadcast.SetPredefinedKeyValue("production_mode", "false");
//            // Set customized fields
//            broadcast.SetCustomizedField("test", "helloworld");
//            broadcast.Send();
//        }
//
//        public void SendIosUnicast()
//        {
//            IosUnicast unicast = new IosUnicast();
//            unicast.SetAppMasterSecret(_appMasterSecretIos);
//            unicast.SetPredefinedKeyValue("appkey", _appkeyIos);
//            unicast.SetPredefinedKeyValue("timestamp", DateTimeHelper.DateTimeToStampString(DateTime.Now));
//            // TODO Set your device token
//            unicast.SetPredefinedKeyValue("device_tokens", "xx");
//            unicast.SetPredefinedKeyValue("alert", "IOS 单播测试");
//            unicast.SetPredefinedKeyValue("badge", 0);
//            unicast.SetPredefinedKeyValue("sound", "chime");
//            // TODO set 'production_mode' to 'true' if your app is under production mode
//            //unicast.SetPredefinedKeyValue("production_mode", "false");
//            // Set customized fields
//            unicast.SetCustomizedField("test", "helloworld");
//            unicast.Send();
//        }
//
//        public void SendIosGroupcast()
//        {
//            IosGroupcast groupcast = new IosGroupcast();
//            groupcast.SetAppMasterSecret(_appMasterSecretIos);
//            groupcast.SetPredefinedKeyValue("appkey", _appkeyIos);
//            groupcast.SetPredefinedKeyValue("timestamp", DateTimeHelper.DateTimeToStampString(DateTime.Now));
//            /*  TODO
//             *  Construct the filter condition:
//             *  "where": 
//             *	{
//             *		"and": 
//             *		[
//             *			{"tag":"iostest"}
//             *		]
//             *	}
//             */
//            JObject filterJson = new JObject();
//            JObject whereJson = new JObject();
//            JArray tagArray = new JArray();
//            JObject testTag = new JObject();
//            testTag.Add("tag", "iostest");
//            tagArray.Add(testTag);
//            whereJson.Add("and", tagArray);
//            filterJson.Add("where", whereJson);
//
//
//            // Set filter condition into RootJson
//            groupcast.SetPredefinedKeyValue("filter", filterJson);
//            groupcast.SetPredefinedKeyValue("alert", "IOS 组播测试");
//            groupcast.SetPredefinedKeyValue("badge", 0);
//            groupcast.SetPredefinedKeyValue("sound", "chime");
//            // TODO set 'production_mode' to 'true' if your app is under production mode
//            //groupcast.SetPredefinedKeyValue("production_mode", "false");
//            groupcast.Send();
//        }
//
//        public void SendIosCustomizedcast()
//        {
//            IosCustomizedcast customizedcast = new IosCustomizedcast();
//            customizedcast.SetAppMasterSecret(_appMasterSecretIos);
//            customizedcast.SetPredefinedKeyValue("appkey", _appkeyIos);
//            customizedcast.SetPredefinedKeyValue("timestamp", DateTimeHelper.DateTimeToStampString(DateTime.Now));
//            // TODO Set your alias here, and use comma to split them if there are multiple alias.
//            // And if you have many alias, you can also upload a file containing these alias, then 
//            // use file_id to Send customized notification.
//            customizedcast.SetPredefinedKeyValue("alias", "xx");
//            // TODO Set your alias_type here
//            customizedcast.SetPredefinedKeyValue("alias_type", "xx");
//            customizedcast.SetPredefinedKeyValue("alert", "IOS 个性化测试");
//            customizedcast.SetPredefinedKeyValue("badge", 0);
//            customizedcast.SetPredefinedKeyValue("sound", "chime");
//            // TODO set 'production_mode' to 'true' if your app is under production mode
//            //customizedcast.SetPredefinedKeyValue("production_mode", "false");
//            customizedcast.Send();
//        }
//
//        public void SendIosFilecast()
//        {
//            IosFilecast filecast = new IosFilecast();
//            filecast.SetAppMasterSecret(_appMasterSecretIos);
//            filecast.SetPredefinedKeyValue("appkey", _appkeyIos);
//            filecast.SetPredefinedKeyValue("timestamp", DateTimeHelper.DateTimeToStampString(DateTime.Now));
//            // TODO upload your device tokens, and use '\n' to split them if there are multiple tokens 
//            filecast.UploadContents("aa" + "\n" + "bb");
//            filecast.SetPredefinedKeyValue("alert", "IOS 文件播测试");
//            filecast.SetPredefinedKeyValue("badge", 0);
//            filecast.SetPredefinedKeyValue("sound", "chime");
//            // TODO set 'production_mode' to 'true' if your app is under production mode
//            //filecast.SetPredefinedKeyValue("production_mode", "false");
//            filecast.Send();
//        }
//
//        #endregion
    }
}
