using System;
using log4net;

namespace CaseKey.Web.API.Common.Umeng.Push.android
{
    public class AndroidGroupcast : AndroidNotification
    {
        public AndroidGroupcast()
        {
            try
            {
                SetPredefinedKeyValue("type", "groupcast");
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(ex.ToString());
            }
        }

    }
}
