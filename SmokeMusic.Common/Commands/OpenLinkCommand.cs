using Framework.Common.Helpers;
using Microsoft.Practices.Prism.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmokeMusic.Common.Commands
{
    /// <summary>
    /// 打开链接命令
    /// </summary>
    public class OpenLinkCommand : DelegateCommand<string>
    {
        public OpenLinkCommand()
            : base(OpenLink)
        {

        }
        static void OpenLink(string url)
        {
            UrlHelper.OpenLink(url);
        }
    }
}
