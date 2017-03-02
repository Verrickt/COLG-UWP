using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Popups;
using Windows.UI.Xaml.Controls.Primitives;
using Colg_UWP.Util;
using Colg_UWP.Model;
using Colg_UWP.Service;

namespace Colg_UWP.ViewModel
{
    public class LoginVM : VMBase
    {
        private ObservableCollection<LoginData> _savedLogins;
        private LoginData _quickLoginData;
       

        public ObservableCollection<LoginData> SavedLogins
        {
            get { return _savedLogins; }
            set { _savedLogins = value; }
        }

        public LoginDataVM LoginDataVM { get; set; } = new LoginDataVM();

        public LoginData QuickLoginData
        {
            get { return _quickLoginData; }
            set { SetProperty(ref _quickLoginData,value); }
        }

        public List<string> SecurityQuestions => new List<string>()
        {
            "安全提问(未设置请忽略)",
            "母亲的名字",
            "父亲的名字",
            "爷爷的名字",
            "父亲出生的城市",
            "您其中一位老师的名字",
            "您个人计算机的型号",
            "您最喜欢的餐馆名称",
            "驾驶执照最后四位数字"
        };

        public void RemoveSavedLogin(LoginData data)
        {
            LoginDataManager.RemoveLoginData(data);
            UpdateSavedLogin();
        }

        public LoginVM()
        {
            SavedLogins = new ObservableCollection<LoginData>();
            UpdateSavedLogin();
        }

        private void UpdateSavedLogin()
        {
            SavedLogins.Clear();
            LoginDataManager.GetLoginDataList().ToList().ForEach(
                ld=>SavedLogins.Add(ld)
                );
        }

        public async Task<bool> QuickLoginAsync()
        {
            return await LoginDataVM.QuickLoginAsync(QuickLoginData);
        }
    }

    public class LoginDataVM : VMBase
    {
        private LoginData _data;

        private bool _undergoingLogin=false;

        public bool UndergoingLogin
        {
            get { return _undergoingLogin; }
            set { SetProperty(ref _undergoingLogin, value); }
        }

        public LoginData Data { get { return _data; }
            set { SetProperty(ref _data,value);} }

        public LoginDataVM()
        {
            _data = new LoginData();
            _data.QuestionId = -1;
            _data.QuestionAnswer = String.Empty;

        }

       

        public async Task<bool> LoginAsync(LoginData data=null)
        {
            LoginData actualData = data ?? _data;
            UndergoingLogin = true;
            if (String.IsNullOrWhiteSpace(actualData.UserName)||String.IsNullOrEmpty(actualData.Password))
            {
                await new MessageDialog("用户名或密码不能为空！").ShowAsync();
                UndergoingLogin = false;
                return false;
            }
            if (actualData.QuestionId<0)
            {
                actualData.QuestionId = 0;
            }
            var (isSuccess,message)=await LoginService.LoginAsync(actualData);
            if (!isSuccess)
            {
                await new MessageDialog("请检查登录信息。确认无误后请再次尝试", "登录失败").ShowAsync();
                UndergoingLogin = false;
            }
            else
            {
                InAppNotifier.Show("登录成功");
            }
            UndergoingLogin = false;
            return isSuccess;
        }

        public async Task<bool> QuickLoginAsync(LoginData data)
        {
            return await LoginAsync(data);
        }
    }
}
