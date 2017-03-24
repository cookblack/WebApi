using System;
using log4net;

namespace CaseKey.Web.API.Common.Umeng.Push.ios
{
    public class IosUnicast : IosNotification
    {
        public IosUnicast()
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
