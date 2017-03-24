namespace Cook.WebApi.OAuth.Auth
{
    /// <summary>
    /// 检查类型返回的类型
    /// </summary>
    public class CheckResult
    {
         public string Errmsg { get; set; }

        public  bool Success { get; set; }

        public  string Channel { get; set; }

        public string Userid { get; set; }

        public string UserName { get; set; }
    }
}