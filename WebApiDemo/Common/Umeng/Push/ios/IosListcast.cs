using System;

namespace CaseKey.Web.API.Common.Umeng.Push.ios
{
    /// <summary>
    /// listcast-列播(要求不超过500个device_token)
    /// </summary>
    public class IosListcast : IosNotification
    {
        public IosListcast()
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
