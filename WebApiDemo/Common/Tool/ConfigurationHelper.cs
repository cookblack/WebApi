using System;
using System.Configuration;

namespace Cook.WebApi.Common.Tool
{
    /// <summary>
    /// 获取配置文件值
    /// </summary>
    public class ConfigurationHelper
    {
        #region 获取Web.config或App.config的值
        /// <summary>
        /// 获取Web.config或App.config的值。
        /// </summary>
        /// <param name="key">属性名</param>
        public static string GetApp(string key)
        {
            return GetApp(key, string.Empty);
        }
        /// <summary>
        /// 获取Web.config或App.config的值（允许值不存在或为空时输出默认值）。
        /// </summary>
        /// <param name="key">属性名</param>
        /// <param name="defaultValue">默认值</param>
        public static string GetApp(string key, string defaultValue)
        {
            string value = ConfigurationManager.AppSettings[key];
            value = string.IsNullOrEmpty(value) ? defaultValue : value;
            return value;
        }
        /// <summary>
        /// 获取Web.config或App.config的值（允许值不存在或为空时输出默认值）。
        /// </summary>
        /// <param name="key">属性名</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns></returns>
        public static int GetAppInt(string key, int defaultValue)
        {
            int result = 0;
            string value = GetApp(key);
            if (!int.TryParse(value, out result))
            {
                return defaultValue;
            }
            return result;
        }
        #endregion

        #region 获取自定义配置文件方法
        /// <summary>
        /// 获取配置文件中AppSetting节的所有键值
        /// 取值方法：ConfigurationHelper.AppSettions["Key"].Value
        /// </summary>
        public static KeyValueConfigurationCollection AppSettions
        {
            get { return GetConfiguration().AppSettings.Settings; }
        }
        public static object GetSection(string sectionName)
        {
            return GetConfiguration().GetSection(sectionName);
        }
        /// <summary>
        /// 获取连接字符串
        /// </summary>
        public static ConnectionStringSettingsCollection ConnectionStrings
        {
            get { return GetConfiguration().ConnectionStrings.ConnectionStrings; }
        }
        /// <summary>
        /// 打开默认配置文件
        /// </summary>
        /// <returns></returns>
        public static Configuration GetConfiguration()
        {
            string configFile = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "DefaultValue.config");
            return GetConfiguration(configFile);
        }
        /// <summary>
        /// 打开指定配置文件
        /// </summary>
        /// <param name="configFile">配置文件路径</param>
        /// <returns></returns>
        public static Configuration GetConfiguration(string configFile)
        {
            ExeConfigurationFileMap fileMap = new ExeConfigurationFileMap();
            fileMap.ExeConfigFilename = configFile;
            return ConfigurationManager.OpenMappedExeConfiguration(fileMap, ConfigurationUserLevel.None);
        }
        #endregion
    }
}
