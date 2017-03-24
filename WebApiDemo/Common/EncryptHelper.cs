using System;
using System.Security.Cryptography;
using System.Text;

namespace Cook.WebApi.Common
{
    /// <summary>
    /// 加密帮助类
    /// </summary>
    public class EncryptHelper
    {
        /// <summary>
        /// 哈希加密
        /// </summary>
        /// <param name="inputString">加密的字符串</param>
        /// <param name="hashName">加密算法</param>
        /// <returns></returns>
        public static string HashString(string inputString, string hashName)
        {
            HashAlgorithm algorithm = HashAlgorithm.Create(hashName);
            if (algorithm == null)
            {
                throw new ArgumentException("Unrecognized hash name", nameof(hashName));
            }
            byte[] hash = algorithm.ComputeHash(Encoding.UTF8.GetBytes(inputString));
            return Convert.ToBase64String(hash);
        }

    }
}