namespace Cook.WebApi.OAuth.Auth
{
    /// <summary>
    /// 验证返回对象
    /// </summary>
    public class CommonResult
    {
        /// <summary>
        /// 验证结果返回
        /// </summary>
        public bool Success { get; set; }
        
        /// <summary>
        /// 用户Id
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// 用户名称
        /// </summary>
        public string UserName { get; set; }

        public string Errmsg { get; set; }

        public CommonResult()
        {

        }
    }
}