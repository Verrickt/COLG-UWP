using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Colg_UWP.Helper;
using Colg_UWP.Model;

namespace Colg_UWP.ViewModel
{
    public class MySpaceVM:VMBase
    {
        public UserData _userData;

        public UserData UserData
        {
            get { return _userData; }
            set { _userData = value;OnPropertyChanged(); }
        }

        /// <summary>
        /// Retrive current user data from local storage
        /// </summary>
        /// <returns></returns>
        public async Task InitAsync()
        {
             UserData = await UserDataManager.GetUserData();
        }
    }
}
