using Microsoft.Practices.Prism.Events;
using SmokeMusic.Logic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmokeMusic.Common.Events.Account
{
    /// <summary>
    /// 
    /// </summary>
    public class ChangeCurrentUserEvent : CompositePresentationEvent<UserInfo>
    {
    }
}
