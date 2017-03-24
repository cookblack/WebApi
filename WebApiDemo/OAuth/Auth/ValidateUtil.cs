namespace Cook.WebApi.OAuth.Auth
{
    /// <summary>
    /// 验证帮助类
    /// </summary>
    public class ValidateUtil
    {
        /// <summary>
        /// 验证是不是int类型
        /// </summary>
        /// <param name="isStr"></param>
        /// <returns></returns>
        public static bool IsNumber(string isStr)
        {
            int output;
            return int.TryParse(isStr, out output);
        }
    }
}