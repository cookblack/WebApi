using System;

namespace CaseKey.Web.API.Common.Umeng.Push.android
{
    /// <summary>
    /// listcast-列播(要求不超过500个device_token)
    /// </summary>
    public class AndroidListcast : AndroidNotification
    {
        public AndroidListcast()
        {
            try
            {
                SetPredefinedKeyValue("type", "listcast");
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(ex.ToString());
            }
        }

    }
}
