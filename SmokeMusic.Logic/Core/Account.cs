using Framework.Common.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace SmokeMusic.Logic.Core
{
    /// <summary>
    /// 账户操作类
    /// </summary>
    public class Account
    {
        /// <summary>
        /// 登录操作
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <param name="captcha"></param>
        /// <param name="captchaID"></param>
        /// <param name="remember"></param>
        public Models.UserInfo Login(string userName, string password)
        {
            Parameters parameters = new Parameters();
            parameters["apikey"] = "02646d3fb69a52ff072d47bf23cef8fd";
            parameters["client_id"] = "02646d3fb69a52ff072d47bf23cef8fd";
            parameters["client_secret"] = "cde5d61429abcd7c";
            parameters["udid"] = "b88146214e19b8a8244c9bc0e2789da68955234d";
            parameters["douban_udid"] = "b635779c65b816b13b330b68921c0f8edc049590";
            parameters["device_id"] = "b88146214e19b8a8244c9bc0e2789da68955234d";
            parameters["grant_type"] = "password";
            parameters["username"] = userName;
            parameters["password"] = password;
            string json = new ConnectionBase().Post("https://www.douban.com/service/auth2/token", Encoding.UTF8.GetBytes(parameters.ToString()));
            var result = JsonHelper.Deserialize<Models.UserInfo>(json);
            return result;
        }
        /// <summary>
        /// 保存用户信息
        /// </summary>
        /// <param name="user"></param>
        public void SaveUserInfo(Models.UserInfo user)
        {
            if (user == null) return;
            BinarySerializeHelper.Serialize(Paths.UserInfoFile, user);
        }
        /// <summary>
        /// 从本地文件得到用户信息
        /// </summary>
        /// <returns></returns>
        public Logic.Models.UserInfo GetUserInfo()
        {
            try
            {
                return BinarySerializeHelper.Deserialize<Logic.Models.UserInfo>(Paths.UserInfoFile);
            }
            catch
            {
                return null;
            }
        }
        /// <summary>
        /// 删除保存的用户信息
        /// </summary>
        public void ClearUserInfo()
        {
            try
            {
                File.Delete(Paths.UserInfoFile);
            }
            catch
            {

            }
        }
        /// <summary>
        /// 保存登录的Cookies
        /// </summary>
        public void SaveCookies()
        {
            ConnectionBase.SaveCookies();
        }
        /// <summary>
        /// 删除Cookies
        /// </summary>
        public void ClearCookies()
        {
            try
            {
                File.Delete(Paths.CookiesFile);
            }
            catch
            {
 
            }
        }
    }
}
