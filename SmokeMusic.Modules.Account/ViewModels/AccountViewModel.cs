using Microsoft.Practices.Prism.Commands;
using SmokeMusic.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmokeMusic.Modules.Account.ViewModels
{
    public class AccountViewModel : ViewModelBase
    {
        #region 构造器
        public AccountViewModel()
        {
            if (!IsInDesignMode)
            {
                this.CaptchaBLL = new Logic.Core.Captcha();
                this.AccountBLL = new Logic.Core.Account();
                this.LoadCaptcha();
            }
        }
        #endregion

        #region 属性
        private string _userName;
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName
        {
            get { return _userName; }
            set
            {
                if (_userName != value)
                {
                    _userName = value;
                    this.RaisePropertyChanged("UserName");
                }
            }
        }
        private string _password;
        /// <summary>
        /// 密码
        /// </summary>
        public string Password
        {
            get { return _password; }
            set
            {
                if (_password != value)
                {
                    _password = value;
                    this.RaisePropertyChanged("Password");
                }
            }
        }
        private string _captcha;
        /// <summary>
        /// 输入的验证码
        /// </summary>
        public string Captcha
        {
            get { return _captcha; }
            set
            {
                if (_captcha != value)
                {
                    _captcha = value;
                    this.RaisePropertyChanged("Captcha");
                }
            }
        }
        private string _captchaID;
        /// <summary>
        /// 验证码ID
        /// </summary>
        public string CaptchaID
        {
            get { return _captchaID; }
            set
            {
                if (_captchaID != value)
                {
                    _captchaID = value;
                    this.RaisePropertyChanged("CaptchaID");
                }
            }
        }
        private string _captchaUri;
        /// <summary>
        /// 验证码图片地址
        /// </summary>
        public string CaptchaUri
        {
            get { return _captchaUri; }
            set
            {
                if (_captchaUri != value)
                {
                    _captchaUri = value;
                    this.RaisePropertyChanged("CaptchaUri");
                }
            }
        }
        private string _busyText;
        /// <summary>
        /// 忙碌文本
        /// </summary>
        public string BusyText
        {
            get { return _busyText; }
            set
            {
                if (_busyText != value)
                {
                    _busyText = value;
                    this.RaisePropertyChanged("BusyText");
                }
            }
        }
        private bool _isLoadingCaptcha;
        /// <summary>
        /// 是否在加载验证码
        /// </summary>
        public bool IsLoadingCaptcha
        {
            get { return _isLoadingCaptcha; }
            set
            {
                if (_isLoadingCaptcha != value)
                {
                    _isLoadingCaptcha = value;
                    this.RaisePropertyChanged("IsLoadingCaptcha");
                }
            }
        }
        private string _errorMessage;
        /// <summary>
        /// 错误信息
        /// </summary>
        public string ErrorMessage
        {
            get { return _errorMessage; }
            set
            {
                if (_errorMessage != value)
                {
                    _errorMessage = value;
                    this.RaisePropertyChanged("ErrorMessage");
                }
            }
        }
        /// <summary>
        /// 重写Title,用于注册Region
        /// </summary>
        public override string Title
        {
            get
            {
                return "Account";
            }
        }
        /// <summary>
        /// 重写KeepAlive,用于注册Region
        /// </summary>
        public override bool KeepAlive
        {
            get
            {
                return true;
            }
        }
        private Logic.Models.UserInfo _currentLoginUser;
        /// <summary>
        /// 当前登录用户
        /// </summary>
        public Logic.Models.UserInfo CurrentLoginUser
        {
            get { return _currentLoginUser; }
            set
            {
                if (_currentLoginUser != value)
                {
                    _currentLoginUser = value;
                    this.RaisePropertyChanged("CurrentLoginUser", "IsLogOn", "IsLogOff");
                }
            }
        }
        /// <summary>
        /// 是否是登录状态
        /// </summary>
        public bool IsLogOn
        {
            get
            {
                return CurrentLoginUser != null;
            }
        }
        /// <summary>
        /// 是否是登出状态
        /// </summary>
        public bool IsLogOff
        {
            get
            {
                return CurrentLoginUser == null;
            }
        }        
        /// <summary>
        /// 账户操作类
        /// </summary>
        public Logic.Core.Account AccountBLL { get; private set; }
        /// <summary>
        /// 验证码操作类
        /// </summary>
        public Logic.Core.Captcha CaptchaBLL { get; private set; }
        #endregion

        #region 命令
        private DelegateCommand<string> _changePasswordCommand;
        /// <summary>
        /// 改变密码时触发的命令
        /// </summary>
        public DelegateCommand<string> ChangePasswordCommand
        {
            get
            {
                if (_changePasswordCommand == null)
                {
                    _changePasswordCommand = new DelegateCommand<string>(this.ChangePassword);
                }
                return _changePasswordCommand;
            }
        }
        private DelegateCommand _loginCommand;
        /// <summary>
        /// 登录命令
        /// </summary>
        public DelegateCommand LoginCommand
        {
            get
            {
                if (_loginCommand == null)
                {
                    _loginCommand = new DelegateCommand(this.Login);
                }
                return _loginCommand;
            }
        }
        private DelegateCommand _loadCaptchaCommand;
        /// <summary>
        /// 加载验证码命令
        /// </summary>
        public DelegateCommand LoadCaptchaCommand
        {
            get
            {
                if (_loadCaptchaCommand == null)
                {
                    _loadCaptchaCommand = new DelegateCommand(this.LoadCaptcha);
                }
                return _loadCaptchaCommand;
            }
        }        
        #endregion

        #region 方法
        /// <summary>
        /// 加载验证码
        /// </summary>
        public void LoadCaptcha()
        {
            if (this.IsLoadingCaptcha) return;
            this.IsLoadingCaptcha = true;
            var action = new Action(() =>
            {
                string captchaID;
                var captchaUri = this.CaptchaBLL.GetCaptchaUri(out captchaID);
                this.CaptchaID = captchaID;
                this.CaptchaUri = captchaUri;
                this.IsLoadingCaptcha = false;
            });
            action.BeginInvoke(null, null);
        }
        public void Login()
        {
            if (string.IsNullOrWhiteSpace(this.UserName))
            {
                this.ErrorMessage = "用户名不能为空!";
                return;
            }
            if (string.IsNullOrWhiteSpace(this.Password))
            {
                this.ErrorMessage = "密码不能为空!";
                return;
            }
            if (string.IsNullOrWhiteSpace(this.Captcha))
            {
                this.ErrorMessage = "验证码不能为空!";
                return;
            }
            this.ErrorMessage = null;

            IsLoading = true;
            var action = new Func<string, string, string, string, bool, Logic.Models.LoginResult>(this.AccountBLL.Login);
            action.BeginInvoke(this.UserName, this.Password, this.Captcha, this.CaptchaID, false, ar => 
            {
                IsLoading = false;
                var result = action.EndInvoke(ar);
                if (result.R > 0)
                {
                    this.ErrorMessage = result.Message;
                    this.LoadCaptcha();
                }
                else
                {
                    this.CurrentLoginUser = result.UserInfo;
                    
                }                
            }, null);
        }
        public void ChangePassword(string password)
        {
            this.Password = password;
        }
        #endregion
    }
}
