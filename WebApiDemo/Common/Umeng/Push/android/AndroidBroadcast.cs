using System;
using CaseKey.Web.API.Common.Umeng.Push;

namespace Cook.WebApi.Common.Umeng.Push.android
{
    public class AndroidBroadcast : AndroidNotification
    {
        /// <summary>
        /// 广播消息
        /// </summary>
        public AndroidBroadcast()
        {
            try
            {
                SetPredefinedKeyValue("type", "broadcast");
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(ex.ToString());
            }
        }
    }
}
