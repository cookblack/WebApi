using System;
using log4net;

namespace CaseKey.Web.API.Common.Umeng.Push.android
{
    /// <summary>
    /// unicast-单播
    /// </summary>
    public class AndroidUnicast : AndroidNotification
    {
        public AndroidUnicast()
        {
            try
            {
                SetPredefinedKeyValue("type", "unicast");
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(ex.ToString());
            }
        }

    }
}
