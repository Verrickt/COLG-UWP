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
        private ObservableCollection<Credential> _savedLogins;
        private Credential _quickCredential;
       

        public ObservableCollection<Credential> SavedLogins
        {
            get { return _savedLogins; }
            set { _savedLogins = value; }
        }

        public CredentialVM CredentialVM { get; set; } = new CredentialVM();

        public Credential QuickCredential
        {
            get { return _quickCredential; }
            set { SetProperty(ref _quickCredential,value); }
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

        public void RemoveSavedLogin(Credential credential)
        {
            LoginDataManager.RemoveLoginData(credential);
            UpdateSavedLogin();
        }

        public LoginVM()
        {
            SavedLogins = new ObservableCollection<Credential>();
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
            return await CredentialVM.QuickLoginAsync(QuickCredential);
        }
    }

    public class CredentialVM : VMBase
    {
        private Credential _credential;

        private bool _undergoingLogin=false;

        public bool UndergoingLogin
        {
            get { return _undergoingLogin; }
            set { SetProperty(ref _undergoingLogin, value); }
        }

        public Credential Credential { get { return _credential; }
            set { SetProperty(ref _credential,value);} }

        public CredentialVM()
        {
            _credential = new Credential();
            _credential.QuestionId = -1;
            _credential.QuestionAnswer = String.Empty;

        }

       

        public async Task<bool> LoginAsync(Credential credential=null)
        {
            Credential actualCredential = credential ?? _credential;
            UndergoingLogin = true;
            if (String.IsNullOrWhiteSpace(actualCredential.LoginName)||String.IsNullOrEmpty(actualCredential.Password))
            {
                await new MessageDialog("用户名或密码不能为空！").ShowAsync();
                UndergoingLogin = false;
                return false;
            }
            if (actualCredential.QuestionId<0)
            {
                actualCredential.QuestionId = 0;
            }
            var (isSuccess,message)=await LoginService.LoginAsync(actualCredential);
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

        public async Task<bool> QuickLoginAsync(Credential credential)
        {
            return await LoginAsync(credential);
        }
    }
}
