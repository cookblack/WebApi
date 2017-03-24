using System;
using CaseKey.Web.API.Common.Umeng.Push.android;
using log4net;

namespace CaseKey.Web.API.Common.Umeng.Push.ios
{
    public class IosBroadcast : IosNotification
    {


        public IosBroadcast()
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
