using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DouBanAPI.API
{
    /// <summary>
    /// 验证码操作类
    /// </summary>
    public class Captcha
    {
        /// <summary>
        /// 得到验证码图片地址
        /// </summary>
        /// <param name="captchaID"></param>
        /// <returns></returns>
        public string GetCaptchaUri(out string captchaID)
        {
            captchaID = new ConnectionBase().Get("http://douban.fm/j/new_captcha");
            if (string.IsNullOrEmpty(captchaID)) return null;
            captchaID = captchaID.Replace("\"", "");
            var uri = string.Format("http://douban.fm/misc/captcha?size=m&id={0}", captchaID);
            return uri;
        }
    }
}
