using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Colg_UWP.Util;
using Colg_UWP.Model;
using Colg_UWP.Service;
using Windows.UI.Xaml.Controls;

namespace Colg_UWP.ViewModel
{
    public class MySpaceVM : VMBase
    {
        private User _user;

        public User User
        {
            get { return _user; }
            set { SetProperty(ref _user, value); }
        }

        public ObservableCollection<Credit> Credits { get; set; }

        public RelayCommand<Frame> SignOutCommand { get; set; }

        /// <summary>
        /// Exp user now havs
        /// </summary>
        public double Exp { get; set; }

        /// <summary>
        /// Upperbound exp of user's current usergroup
        /// </summary>
        public double? ExpMax { get; set; }


        /// <summary>
        /// Exp required to be prompted
        /// </summary>
        public double? ExpRequired => ExpMax - Exp??null;

        public string TimeRegisted { get; set; }

        public MySpaceVM()
        {
            User = UserDataManager.GetActiveUser();
            SignOutCommand = new RelayCommand<Frame>(async (f) =>
            {
                var (result, message) = await LoginService.LogoutAsync();

                if (!result)
                {
                    InAppNotifier.Show("注销失败,是不是网络出问题了?");
                }
                else
                {
                    InAppNotifier.Show("注销成功");
                    f.GoBack();
                    f.Navigate(typeof(View.Pages.LoginPage));
                }
            });
            Credits = new ObservableCollection<Credit>(User.Credits);
            Exp = Credits.Max(k => k.Value);
            ExpMax = User.UserGroup.CreditRange.UpperBound;
            TimeRegisted = User.TimeRegisted?.ToString("yyyy.MM.dd");
        }

        


      
    }
}
