using System;
using log4net;

namespace CaseKey.Web.API.Common.Umeng.Push.ios
{
    public class IosGroupcast : IosNotification
    {

        public IosGroupcast()
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
