using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Security.Cryptography;
using System.Web;

namespace Cook.WebApi.Common.Tool
{
    /// <summary>
    /// 工具公用类 MAC IP MD5
    /// </summary>
    public static class CommonContext
    {
        private static string _GetIdentityKey;
        public static string GetIdentityKey
        {
            get
            {
                var IdentityKeySession = HttpContext.Current.Session["IdentityKey"];
                if (IdentityKeySession == null)
                {
                    //_GetIdentityKey = MD5Encrupt(string.Concat(GetWebClientIp(), GetMac()));
                    _GetIdentityKey = MD5Encrupt(string.Concat(HttpContext.Current.Request.UserHostAddress, GetClientKey(), HttpContext.Current.Request.UserAgent));
                    HttpContext.Current.Session["IdentityKey"] = _GetIdentityKey;
                }
                else
                    _GetIdentityKey = IdentityKeySession.ToString();
                return _GetIdentityKey;
            }

        }
        /// <summary>
        /// 获取访问MAC
        /// </summary>
        /// <returns></returns>
        public static string GetMac()
        {
            using (ManagementClass mc = new ManagementClass("Win32_NetworkAdapterConfiguration"))
            {
                ManagementObjectCollection moc2 = mc.GetInstances();
                foreach (ManagementObject mo in moc2)
                {
                    if ((bool)mo["IPEnabled"] == true)
                    {
                        return mo["MacAddress"].ToString();
                    }
                }
            }
            return string.Empty;
        }

        public static string GetClientKey()
        {
            try
            {
                if (HttpContext.Current.Request.Cookies["ClientKey"] == null)
                {
                    string key = Guid.NewGuid().ToString();
                    HttpCookie cookie = new HttpCookie("ClientKey");
                    cookie.Domain = PlatformConfig.UrlDomain;
                    cookie.Path = "/";
                    cookie.Expires = DateTime.Now.AddDays(1);
                    cookie.Value = key;
                    HttpContext.Current.Response.AppendCookie(cookie);
                }
                //不直接返回key，避免Cookie无法写入造成key一直变的情况
                if (HttpContext.Current.Request.Cookies["ClientKey"] != null)
                {
                    return HttpContext.Current.Request.Cookies["ClientKey"].Value;
                }
            }
            catch { }
            return "";
        }
        /// <summary>
        /// 获取IP
        /// </summary>
        /// <returns></returns>
        public static string GetWebClientIp()
        {
            string userIP = string.Empty;
            try
            {
                if (System.Web.HttpContext.Current == null
            || System.Web.HttpContext.Current.Request == null
            || System.Web.HttpContext.Current.Request.ServerVariables == null)
                    return "";

                string CustomerIP = "";

                //CDN加速后取到的IP 
                CustomerIP = System.Web.HttpContext.Current.Request.Headers["Cdn-Src-Ip"];
                if (!string.IsNullOrEmpty(CustomerIP))
                {
                    return CustomerIP;
                }

                CustomerIP = System.Web.HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];


                if (!String.IsNullOrEmpty(CustomerIP))
                    return CustomerIP;

                if (System.Web.HttpContext.Current.Request.ServerVariables["HTTP_VIA"] != null)
                {
                    CustomerIP = System.Web.HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
                    if (CustomerIP == null)
                        CustomerIP = System.Web.HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
                }
                else
                {
                    CustomerIP = System.Web.HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];

                }

                if (string.Compare(CustomerIP, "unknown", true) == 0)
                    return System.Web.HttpContext.Current.Request.UserHostAddress;
                return CustomerIP;
            }
            catch { }

            return userIP;
        }

        /// <summary>
        /// MD5加密
        /// </summary>
        /// <param name="strText"></param>
        /// <returns></returns>
        public static string MD5Encrupt(string strText)
        {
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            return BitConverter.ToString(md5.ComputeHash(System.Text.Encoding.Default.GetBytes(strText))).Replace("-", "").ToLower();
        }
        /// <summary>
        /// MD5加密
        /// </summary>
        /// <param name="strText"></param>
        /// <returns></returns>
        public static string Md5Utf8Encrupt(string strText)
        {
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            return BitConverter.ToString(md5.ComputeHash(System.Text.Encoding.UTF8.GetBytes(strText))).Replace("-", "").ToLower();
        }
        //public static void Code()
        //{
        //    string chkCode = string.Empty;
        //    //颜色列表，用于验证码、噪线、噪点 
        //    Color[] color = { Color.Black, Color.Red, Color.Blue, Color.Green, Color.Orange, Color.Brown, Color.Brown, Color.DarkBlue };
        //    //字体列表，用于验证码 
        //    string[] font = { "Times New Roman", "MS Mincho", "Book Antiqua", "Gungsuh", "PMingLiU", "Impact" };
        //    //验证码的字符集，去掉了一些容易混淆的字符 
        //    char[] character = { '2', '3', '4', '5', '6', '8', '9', 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'J', 'K', 'L', 'M', 'N', 'P', 'R', 'S', 'T', 'W', 'X', 'Y' };
        //    Random rnd = new Random();
        //    //生成验证码字符串 
        //    for (int i = 0; i < 4; i++)
        //    {
        //        chkCode += character[rnd.Next(character.Length)];
        //    } 
        //    UpdateCache("ValidateCode", chkCode); 
        //    Bitmap bmp = new Bitmap(100, 40);
        //    Graphics g = Graphics.FromImage(bmp);
        //    g.Clear(Color.White);
        //    //画噪线 
        //    for (int i = 0; i < 10; i++)
        //    {
        //        int x1 = rnd.Next(100);
        //        int y1 = rnd.Next(40);
        //        int x2 = rnd.Next(100);
        //        int y2 = rnd.Next(40);
        //        Color clr = color[rnd.Next(color.Length)];
        //        g.DrawLine(new Pen(clr), x1, y1, x2, y2);
        //    }
        //    //画验证码字符串 
        //    for (int i = 0; i < chkCode.Length; i++)
        //    {
        //        string fnt = font[rnd.Next(font.Length)];
        //        Font ft = new Font(fnt, 18);
        //        Color clr = color[rnd.Next(color.Length)];
        //        g.DrawString(chkCode[i].ToString(), ft, new SolidBrush(clr), (float)i * 20 + 8, (float)8);
        //    }
        //    //画噪点 
        //    for (int i = 0; i < 100; i++)
        //    {
        //        int x = rnd.Next(bmp.Width);
        //        int y = rnd.Next(bmp.Height);
        //        Color clr = color[rnd.Next(color.Length)];
        //        bmp.SetPixel(x, y, clr);
        //    }
        //    //清除该页输出缓存，设置该页无缓存 
        //    HttpContext.Current.Response.Buffer = true;
        //    HttpContext.Current.Response.ExpiresAbsolute = System.DateTime.Now.AddMilliseconds(0);
        //    HttpContext.Current.Response.Expires = 0;
        //    HttpContext.Current.Response.CacheControl = "no-cache";
        //    HttpContext.Current.Response.AppendHeader("Pragma", "No-Cache");
        //    //将验证码图片写入内存流，并将其以 "image/Png" 格式输出 
        //    MemoryStream ms = new MemoryStream();
        //    try
        //    {
        //        bmp.Save(ms, ImageFormat.Png);
        //        HttpContext.Current.Response.ClearContent();
        //        HttpContext.Current.Response.ContentType = "image/Png";
        //        HttpContext.Current.Response.BinaryWrite(ms.ToArray());            

        //    }
        //    finally
        //    {
        //        //显式释放资源 
        //        bmp.Dispose();
        //        g.Dispose();
        //    }
        //}

        public static void Code(int? width = 100, int? height = 40)
        {
            string chkCode = string.Empty;
            //颜色列表，用于验证码、噪线、噪点 
            Color[] color = { Color.Black, Color.Red, Color.Blue, Color.Green, Color.Orange, Color.Brown, Color.Brown, Color.DarkBlue };
            //字体列表，用于验证码 
            string[] font = { "Times New Roman", "MS Mincho", "Book Antiqua", "Gungsuh", "PMingLiU", "Impact" };
            //验证码的字符集，去掉了一些容易混淆的字符 
            char[] character = { '2', '3', '4', '5', '6', '8', '9', 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'J', 'K', 'L', 'M', 'N', 'P', 'R', 'S', 'T', 'W', 'X', 'Y' };
            Random rnd = new Random();
            //生成验证码字符串 
            for (int i = 0; i < 4; i++)
            {
                chkCode += character[rnd.Next(character.Length)];
            }
            UpdateCache("ValidateCode", chkCode);
            Bitmap bmp = new Bitmap(width.Value, height.Value);
            Graphics g = Graphics.FromImage(bmp);
            g.Clear(Color.White);
            //画噪线 
            for (int i = 0; i < 1; i++)
            {
                int x1 = rnd.Next(100);
                int y1 = rnd.Next(40);
                int x2 = rnd.Next(100);
                int y2 = rnd.Next(40);
                Color clr = color[rnd.Next(color.Length)];
                g.DrawLine(new Pen(clr), x1, y1, x2, y2);
            }
            //画验证码字符串 
            for (int i = 0; i < chkCode.Length; i++)
            {
                string fnt = font[rnd.Next(font.Length)];
                Font ft = new Font(fnt, 18);
                Color clr = color[rnd.Next(color.Length)];
                g.DrawString(chkCode[i].ToString(), ft, new SolidBrush(clr), (float)i * 20 + 8, (float)8);
            }
            //画噪点 
            for (int i = 0; i < 1; i++)
            {
                int x = rnd.Next(bmp.Width);
                int y = rnd.Next(bmp.Height);
                Color clr = color[rnd.Next(color.Length)];
                bmp.SetPixel(x, y, clr);
            }
            //清除该页输出缓存，设置该页无缓存 
            HttpContext.Current.Response.Buffer = true;
            HttpContext.Current.Response.ExpiresAbsolute = System.DateTime.Now.AddMilliseconds(0);
            HttpContext.Current.Response.Expires = 0;
            HttpContext.Current.Response.CacheControl = "no-cache";
            HttpContext.Current.Response.AppendHeader("Pragma", "No-Cache");
            //将验证码图片写入内存流，并将其以 "image/Png" 格式输出 
            MemoryStream ms = new MemoryStream();
            try
            {
                bmp.Save(ms, ImageFormat.Png);
                HttpContext.Current.Response.ClearContent();
                HttpContext.Current.Response.ContentType = "image/Png";
                HttpContext.Current.Response.BinaryWrite(ms.ToArray());

            }
            finally
            {
                //显式释放资源 
                bmp.Dispose();
                g.Dispose();
            }
        }


        /// <summary>
        /// 获取验证码
        /// </summary>
        /// <returns></returns>
        public static object GetCode()
        {
            object CodeStr = GetCache("ValidateCode");
            return CodeStr;
        }


        /// <summary>
        ///  校验验证码
        /// </summary>
        /// <param name="Code">校对的验证码</param>
        /// <returns></returns>
        public static bool CheckCode(string Code)
        {
            object CCode = GetCache("ValidateCode");
            if (string.IsNullOrEmpty(Code) || CCode == null || string.Compare(CCode.ToString().ToUpper(), Code.ToUpper()) != 0)
            {
                return true;
            }
            else
            {
                RemoveCache("ValidateCode");
                return false;
            }
        }


        /// <summary>
        /// 新增用户缓存数据 有个更新 没有就新增
        /// </summary>
        /// <param name="KeyNum">键</param>
        /// <param name="Info">值</param>
        /// <returns></returns>
        public static bool UpdateCache(string KeyNum, object Info)
        {
            if (Info == null)
                return false;
            try
            {
                var _KeyNum = string.Concat(KeyNum, GetIdentityKey);

                CacheManage cache = CacheManage.LocalInstance;
                cache.Add(_KeyNum, Info, 15);
                return true;
            }
            catch (Exception ex)
            {
                Log.WriteLogToTxt(string.Format("更新用户缓存信息[UpdateUserInfoCache]：{0}", ex.Message));
                return false;
            }

        }


        /// <summary>
        ///  移除用户缓存数据
        /// </summary>
        /// <param name="KeyNum">键</param>
        /// <returns></returns>
        public static bool RemoveCache(string KeyNum)
        {
            try
            {
                var IdentityKey = GetIdentityKey;
                var _KeyNum = string.Concat(KeyNum, IdentityKey);
                CacheManage cache = CacheManage.LocalInstance;
                cache.Remove(_KeyNum);

                if (KeyNum.Equals("USERINFO:"))
                {
                    cache.Remove(string.Concat("USERINFO-TuthorityManager", IdentityKey));
                    cache = CacheManage.Instance;
                    cache.Remove(_KeyNum);
                }
                return true;
            }
            catch (Exception ex)
            {
                Log.WriteLogToTxt(string.Format("移除缓存数据[RemoveUserInfoCache]：{0}", ex.Message));
                return false;
            }
        }
        public static bool RemoveCache(string KeyNum, string IdentityKey)
        {
            try
            {
                var _KeyNum = string.Concat(KeyNum, IdentityKey);
                CacheManage cache = CacheManage.LocalInstance;
                cache.Remove(_KeyNum);

                if (KeyNum.Equals("USERINFO:"))
                {
                    cache.Remove(string.Concat("USERINFO-TuthorityManager", IdentityKey));
                    cache.Remove(string.Concat("GetTimes:", IdentityKey));
                }
                return true;
            }
            catch (Exception ex)
            {
                Log.WriteLogToTxt(string.Format("移除缓存数据[RemoveUserInfoCache]：{0}", ex.Message));
                return false;
            }
        }

        /// <summary>
        /// 获取用户缓存数据
        /// </summary>
        /// <param name="KeyNum">键</param> 
        /// <returns></returns>
        public static object GetCache(string KeyNum)
        {
            try
            {
                var _KeyNum = string.Concat(KeyNum, GetIdentityKey);
                CacheManage cache = CacheManage.LocalInstance;
                var value = cache.Get(_KeyNum);
                return value;
            }
            catch (Exception ex)
            {
                Log.WriteLogToTxt(string.Format("更新用户缓存信息[UpdateUserInfoCache]：{0}", ex.Message));
                return false;
            }
        }

        /// <summary>
        /// 设置全局一级缓存
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <param name="value">缓存值</param>
        /// <param name="minute">缓存时间（单位分钟，可使用小数，默认30分钟）</param>
        /// <returns></returns>
        public static bool CacheSet(string key, object value, double minute = 30)
        {
            if (value == null)
                return false;
            try
            {
                CacheManage cache = CacheManage.LocalInstance;
                cache.Set(key, value, minute);
                return true;
            }
            catch (Exception ex)
            {
                Log.WriteLogToTxt(string.Format("更新缓存信息[CacheSet]：{0}", ex.Message));
                return false;
            }
        }

        /// <summary>
        /// 获取全局缓存数据
        /// </summary>
        /// <param name="key">键</param> 
        /// <returns></returns>
        public static object CacheGet(string key)
        {
            try
            {
                CacheManage cache = CacheManage.LocalInstance;
                var value = cache.Get(key);
                return value;
            }
            catch (Exception ex)
            {
                Log.WriteLogToTxt(string.Format("更新用户缓存信息[CacheGet]：{0}", ex.Message));
                return false;
            }
        }

        /// <summary>
        /// 新增手机用户缓存数据 有个更新 没有就新增
        /// </summary>
        /// <param name="KeyNum">键</param>
        /// <param name="Info">值</param>
        /// <param name="CacheMinute">缓存时间</param>
        /// <returns></returns>
        public static bool SetAppCache(string KeyNum, object Info, double CacheMinute = 15)
        {
            if (Info == null || string.IsNullOrEmpty(KeyNum))
                return false;
            try
            {
                var _KeyNum = $"AppCache:{KeyNum}";
                CacheManage cache = CacheManage.LocalInstance;
                cache.Set(_KeyNum, Info, CacheMinute);
                return true;
            }
            catch (Exception ex)
            {
                Log.WriteLogToTxt(string.Format("新增手机用户缓存数据[SetAppCache]：{0}", ex.Message));
                return false;
            }

        }

        /// <summary>
        /// 获取手机用户缓存数据
        /// </summary>
        /// <param name="KeyNum">键</param> 
        /// <returns></returns>
        public static object GetAppCache(string KeyNum)
        {
            try
            {
                var _KeyNum = $"AppCache:{KeyNum}";
                CacheManage cache = CacheManage.LocalInstance;
                var value = cache.Get(_KeyNum);
                return value;
            }
            catch (Exception ex)
            {
                Log.WriteLogToTxt(string.Format("获取手机用户缓存数据[GetAppCache]：{0}", ex.Message));
                return false;
            }
        }

        /// <summary>
        ///  移除手机用户缓存数据
        /// </summary>
        /// <param name="KeyNum">键</param>
        /// <returns></returns>
        public static bool RemoveAppCache(string KeyNum)
        {
            try
            {
                var _KeyNum = $"AppCache:{KeyNum}";
                CacheManage cache = CacheManage.LocalInstance;
                cache.Remove(_KeyNum);
                return true;
            }
            catch (Exception ex)
            {
                Log.WriteLogToTxt(string.Format("移除手机用户缓存数据[RemoveAppCache]：{0}", ex.Message));
                return false;
            }
        }

        #region 访问次数控制

        /// <summary>
        /// 根据操作标识和用户标识检测是否没超过限制次数
        /// 默认1440分钟，1次，使用URL和GetIdentityKey作为Key
        /// 如：CommonContext.VisitCheck(1, 1440, "HitIndustry" + id, userId);
        /// </summary>
        /// <param name="minute">限制分钟数，默认1440分钟(24小时)</param>
        /// <param name="limit">限制次数，默认1</param>
        /// <param name="mark">操作标识，默认URL，可指定为：需求名+Id、产品名+Id、设计师名+Id等</param>
        /// <param name="userKey">用户标识，默认GetIdentityKey，可指定为：用户名或用户ID等</param>
        /// <returns></returns>
        public static bool VisitCheck(double minute = 1440, int limit = 1, object mark = null, object userKey = null)
        {
            if (limit > 0 && minute > 0)
            {
                mark = mark ?? HttpContext.Current.Request.Url;
                userKey = userKey ?? GetIdentityKey;
                string key = "VisitCheck:" + MD5Encrupt($"{userKey}{mark}");
                CacheManage cache = CacheManage.LocalInstance;
                var value = cache.Get(key);
                if (value != null)//已经访问过
                {
                    try
                    {
                        int times = (int)value;
                        if (times < limit) //未超过限制次数
                        {
                            cache.Set(key, times + 1, minute);
                            return true;
                        }
                        else //超过次数
                        {
                            return false;
                        }
                    }
                    catch (Exception)
                    {
                        return true;
                    }
                }
                else //未访问过
                {
                    cache.Set(key, 1, minute);
                }
            }
            return true;
        }

        #endregion 访问次数控制
    }
}
