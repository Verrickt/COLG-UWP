using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Colg_UWP.Util;
using Colg_UWP.Model;
using Colg_UWP.Service;

namespace Colg_UWP.ViewModel
{
    public class MySpaceVM:VMBase
    {
        private User _user;

        public User User
        {
            get { return _user; }
            set { SetProperty(ref _user, value); }
        }

        public ObservableCollection<string> Credits { get; set; }

        public MySpaceVM()
        {
            User = UserDataManager.GetActiveUser();
            Credits = new ObservableCollection<string>(User.Credits);

        }


        public async Task<bool> LogoutAsync()
        {
            var(result,message)=await LoginService.LogoutAsync();

            InAppNotifier.Show(result ? "注销成功" : "注销失败");

            return result;
        }
    }
}
