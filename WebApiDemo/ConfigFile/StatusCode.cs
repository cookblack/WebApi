namespace Cook.WebApi.ConfigFile
{
    /// <summary>
    /// 返回的状态码
    /// </summary>
    public  enum StatusCode
    {
        操作成功 = 1008,
        操作失败 = 1004,
        令牌过期 = 1001,
        数据完整性检查不通过 = 1002,
        验证码错误 = 1003,
        手机号码已注册 = 1005,
        第三方注册失败 = 1006,
        没有相关信息 = 1007,
        签名时间戳失效 = 1009,
        手机号码错误=1010
    }
}