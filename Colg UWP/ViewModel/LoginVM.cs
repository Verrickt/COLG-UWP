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
using Windows.UI.Xaml.Controls;
using Colg_UWP.View.Pages;

namespace Colg_UWP.ViewModel
{
    public class LoginVM : VMBase
    {
        private ObservableCollection<User> _savedUsers;
        private User _quickLoginUser;
       

        public ObservableCollection<User> SavedUsers
        {
            get { return _savedUsers; }
            set { _savedUsers = value; }
        }


        public User QuickLoginUser
        {
            get { return _quickLoginUser; }
            set { SetProperty(ref _quickLoginUser,value); }
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

        public User NewUser { get; set; } = new User();

        public RelayCommand<Frame> LoginCommand { get; set; }

        public RelayCommand<Frame> QuickLoginCommand { get; set; }

        private bool _undergoingLogin = false;

        public bool UndergoingLogin
        {
            get { return _undergoingLogin; }
            set { SetProperty(ref _undergoingLogin, value); }
        }



        public void RemoveUser(User user)
        {
            UserDataManager.RemoveUser(user);
            UpdateSavedUser();
          
        }

        private async Task<bool> LoginAsync(User anotheruser=null)
        {
            var actualUser = anotheruser??NewUser;
            var credential = actualUser.Credential;
            UndergoingLogin = true;
            if (String.IsNullOrWhiteSpace(credential.LoginName) || String.IsNullOrEmpty(credential.Password))
            {
                await new MessageDialog("用户名或密码不能为空！").ShowAsync();
                UndergoingLogin = false;
                return false;
            }
            if (credential.QuestionId < 0)
            {
                credential.QuestionId = 0;
            }
            var (isSuccess, message) = await LoginService.LoginAsync(actualUser);
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

        public async Task<bool> QuickLoginAsync()
        {
            return await LoginAsync(QuickLoginUser);
        }




        public LoginVM()
        {
            SavedUsers = new ObservableCollection<User>();
            UpdateSavedUser();
            LoginCommand = new RelayCommand<Frame>(async (f) =>
            {
                if (await LoginAsync())
                {
                    GoToUserSpace(f);
                }
            });
            QuickLoginCommand = new RelayCommand<Frame>(async (f) =>
            {
                if (await LoginAsync(QuickLoginUser))
                {
                    GoToUserSpace(f);
                }
            }
            );
        }

        private static void GoToUserSpace(Frame f)
        {
            f.GoBack();
            f.Navigate(typeof(MySpace));
        }

        private void UpdateSavedUser()
        {
            SavedUsers.Clear();
            UserDataManager.GetUsers().ToList().ForEach(
                ld=>SavedUsers.Add(ld)
                );
        }

      
    }

 
}
