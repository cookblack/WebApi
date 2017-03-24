namespace Cook.WebApi.OAuth.Auth
{
    /// <summary>
    /// 令牌对象返回
    /// </summary>
    public class TokenResult
    {
        /// <summary>
        /// 结果返回
        /// </summary>
         public  bool Success { get; set; }

        /// <summary>
        /// 令牌数据
        /// </summary>
        public  string AccessToken { get; set; }

        /// <summary>
        /// 过期时间
        /// </summary>
        public int ExpiresIn { get; set; }

        /// <summary>
        /// 用户id
        /// </summary>
        public string UserId { get; set; }        

        /// <summary>
        /// 信息提示
        /// </summary>
        public string Errmsg { get; set; }


    }
}