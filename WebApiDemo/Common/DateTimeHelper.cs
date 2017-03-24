using System;
using System.Globalization;

namespace Cook.WebApi.Common
{
    /// <summary>
    /// 时间戳帮助类
    /// </summary>
    public class DateTimeHelper
    {

        /// <summary>
        /// 时间戳转为C#格式时间
        /// </summary>
        /// <param name="timeStamp"></param>
        /// <returns></returns>
        public static DateTime StampToDateTime(string timeStamp)
        {
            DateTime dateTimeStart = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            long lTime = long.Parse(timeStamp + "0000000");
            TimeSpan toNow = new TimeSpan(lTime);

            return dateTimeStart.Add(toNow);
        }

        /// <summary>
        /// DateTime时间格式转换为Unix时间戳格式
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        public static int DateTimeToStamp(DateTime time)
        {
            System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1));
            return (int)(time - startTime).TotalSeconds;
        }
        /// <summary>
        /// DateTime时间格式转换为Unix时间戳格式
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        public static int DateTimeToStamp(DateTime? time)
        {
            DateTime tmpTime = Convert.ToDateTime(time);
            System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1));
            return (int)(tmpTime - startTime).TotalSeconds;
        }

        /// <summary>
        /// DateTime时间格式转换为Unix时间戳格式
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        public static string DateTimeToStampString(DateTime time)
        {
            System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1));
            return Convert.ToString((time - startTime).TotalSeconds, CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// 该时间一天开始时间
        /// </summary>
        /// <param name="now"></param>
        /// <returns></returns>
        public static DateTime ToDayBegin(DateTime now)
        {
            return Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd 00:00:00"));
        }

        /// <summary>
        /// 该时间一天结束时间
        /// </summary>
        /// <param name="now"></param>
        /// <returns></returns>
        public static DateTime ToDayEnd(DateTime now)
        {
            return Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd 23:59:59"));
        }

        /// <summary>
        /// 将java的毫秒数转换成C#的datetime
        /// </summary>
        /// <param name="javaMs"></param>
        /// <returns></returns>
        public static DateTime ConverJavaMillSecondToDateTime(long javaMs)
        {
            DateTime utcBaseTime=new DateTime(1970,1,1,0,0,0,0);
            DateTime dt = utcBaseTime.Add(new TimeSpan(javaMs*TimeSpan.TicksPerMillisecond)).ToLocalTime();
            return dt;
        }

        /// <summary>
        /// C#的datetime转换成java的毫秒数
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static long ConverDateTimeToJavaMillSecond(DateTime dateTime)
        {
            DateTime windowsEpoch=new DateTime(1601,1,1,0,0,0,0);
            DateTime javaEpoch = new DateTime(1970,1,1,0,0,0,0);
            long epochDiff = (javaEpoch.ToFileTimeUtc() - windowsEpoch.ToFileTimeUtc())/TimeSpan.TicksPerMillisecond;
            return (dateTime.ToFileTimeUtc()/TimeSpan.TicksPerMillisecond) - epochDiff;
        }

        /// <summary>
        /// C#的datetime转换成java的毫秒数
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static long ConverDateTimeToJavaMillSecond(DateTime? dateTime)
        {
            DateTime tmpTime = Convert.ToDateTime(dateTime);
            DateTime windowsEpoch = new DateTime(1601, 1, 1, 0, 0, 0, 0);
            DateTime javaEpoch = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            long epochDiff = (javaEpoch.ToFileTimeUtc() - windowsEpoch.ToFileTimeUtc()) / TimeSpan.TicksPerMillisecond;
            return (tmpTime.ToFileTimeUtc() / TimeSpan.TicksPerMillisecond) - epochDiff;
        }
    }
}