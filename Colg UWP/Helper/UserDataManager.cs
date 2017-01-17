using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.AppExtensions;

namespace Colg_UWP.Helper
{
    using Model;
    public class UserDataManager
    {
        private static UserData _userData = null;

        public static async Task<UserData> GetUserData()
        {
            return _userData ??
                   (_userData = await StorageHelper.Read<UserData>(StorageNames.UserData).ConfigureAwait(false))??(_userData=new UserData());
        }

        public static async Task SaveUserData()
        {
            await StorageHelper.Save(_userData, StorageNames.UserData).ConfigureAwait(false);
        }
    }
}
