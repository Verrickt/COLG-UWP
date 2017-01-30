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
        private UserData _userData;

        public UserData UserData
        {
            get { return _userData; }
            set { SetProperty(ref _userData, value); }
        }

        public ObservableCollection<string> Credits { get; set; }

        public MySpaceVM()
        {
            UserData = UserDataManager.GetUserData();
            Credits = new ObservableCollection<string>(UserData.Credits);

        }


        public async Task<bool> LogoutAsync()
        {
            var(result,message)=await LoginService.LogoutAsync();

            InAppNotifier.Show(message);

            return result;
        }
    }
}
