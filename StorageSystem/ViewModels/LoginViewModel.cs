using Newtonsoft.Json;
using StorageSystem.Common;
using StorageSystem.Extensions;
using StorageSystem.Service;
using StorageSystem.Shared.Dtos;

namespace StorageSystem.ViewModels
{
    public class LoginViewModel : BindableBase, IDialogAware
    {
        public LoginViewModel(ILoginService loginService, IEventAggregator aggregator)
        {
            UserDto = new ResgiterUserDto();
            ExecuteCommand = new DelegateCommand<string>(Execute);
            this.loginService = loginService;
            this.aggregator = aggregator;
        }
        public string Title { get; set; } = "登陆";
        public bool CanCloseDialog()
        {
            return true;
        }

        public void OnDialogClosed()
        {
            LoginOut(result);
        }

        public void OnDialogOpened(IDialogParameters parameters)
        {
        }

        #region Login

        private ButtonResult result;

        private int selectIndex;

        public int SelectIndex
        {
            get { return selectIndex; }
            set { selectIndex = value; RaisePropertyChanged(); }
        }


        public DelegateCommand<string> ExecuteCommand { get; private set; }


        private string userName;

        public string UserName
        {
            get { return userName; }
            set { userName = value; RaisePropertyChanged(); }
        }

        private string passWord;
        private readonly ILoginService loginService;
        private readonly IEventAggregator aggregator;

        public string PassWord
        {
            get { return passWord; }
            set { passWord = value; RaisePropertyChanged(); }
        }

        private void Execute(string obj)
        {
            switch (obj)
            {
                case "Login": Login(); break;
                case "LoginOut": LoginOut(ButtonResult.No); break;
                case "Resgiter": Resgiter(); break;
                case "ResgiterPage": SelectIndex = 1; break;
                case "Return": SelectIndex = 0; break;
            }
        }

        private ResgiterUserDto userDto;

        public ResgiterUserDto UserDto
        {
            get { return userDto; }
            set { userDto = value; RaisePropertyChanged(); }
        }

        DialogCloseListener requestClose = new DialogCloseListener();
        DialogCloseListener IDialogAware.RequestClose => requestClose;

        async void Login()
        {
            if (string.IsNullOrWhiteSpace(UserName) ||
                string.IsNullOrWhiteSpace(PassWord))
            {
                return;
            }

            var loginResult = await loginService.Login(new Shared.Dtos.UserDto()
            {
                Account = UserName,
                Password = PassWord
            });

            if (loginResult != null && loginResult.Status)
            {
                UserDto user = JsonConvert.DeserializeObject<UserDto>(loginResult.Result.ToString());
                AppSession.UserName = user.Name;
                result = ButtonResult.OK;
                requestClose.Invoke(result);
            }
            else
            {
                //登录失败提示...
                aggregator.SendMessage(loginResult.Message, "Login");
            }
        }

        private async void Resgiter()
        {
            if (string.IsNullOrWhiteSpace(UserDto.Account) ||
                string.IsNullOrWhiteSpace(UserDto.UserName) ||
                string.IsNullOrWhiteSpace(UserDto.PassWord) ||
                string.IsNullOrWhiteSpace(UserDto.NewPassWord))
            {
                aggregator.SendMessage("请输入完整的注册信息！", "Login");
                return;
            }

            if (UserDto.PassWord != UserDto.NewPassWord)
            {
                aggregator.SendMessage("密码不一致,请重新输入！", "Login");
                return;
            }

            var resgiterResult = await loginService.Resgiter(new Shared.Dtos.UserDto()
            {
                Account = UserDto.Account,
                Name = UserDto.UserName,
                Password = UserDto.PassWord
            });

            if (resgiterResult != null && resgiterResult.Status)
            {
                aggregator.SendMessage("注册成功", "Login");
                //注册成功,返回登录页页面
                SelectIndex = 0;
            }
            else
                aggregator.SendMessage(resgiterResult.Message, "Login");
        }

        void LoginOut(ButtonResult result)
        {
            requestClose.Invoke(new DialogResult(result));
        }

        #endregion
    }
}
