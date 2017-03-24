namespace Cook.WebApi.OAuth.Auth
{
    /// <summary>
    /// 系统认证等基础接口
    /// </summary>
    public interface IAuthApi
    {
        /// <summary>
        /// 注册用户获取访问令牌接口
        /// 1）Web API 为各种应用接入，如APP、Web、Winform等接入端分配应用AppID以及通信密钥AppSecret，双方各自存储。
       ////2）接入端在请求Web API接口时需携带以下参数：signature、 timestamp、nonce、appid，签名是根据几个参数和加密秘钥生成。
       ////3） Web API 收到接口调用请求时需先检查传递的签名是否合法，验证后才调用相关接口。
        /// </summary>
        /// <param name="username">用户登录名称</param>
        /// <param name="password">用户密码</param>
        /// <param name="signature">加密签名字符串</param>
        /// <param name="timestamp">时间戳</param>
        /// <param name="nonce">随机数</param>
        /// <param name="appid">应用接入ID</param>
        /// <returns></returns>
        TokenResult GetAccessToken(string username, string password,
            string signature, string timestamp, string nonce, string appid);
    }
}