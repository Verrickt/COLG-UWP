using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.AppExtensions;

namespace Colg_UWP.Util
{
    using Model;
    public class UserDataManager
    {
        public const string _fileName = "UserData";

        private static User _user = new User();

        public static  User GetUserData()
        {
            return _user;
        }

        public static async Task InitializationAsync()
        {
            _user = await StorageHelper.ReadAsync<User>(_fileName)
            ??
            _user;
        }

        public static async Task SaveUserData()
        {
            await StorageHelper.SaveAsync(_user, _fileName).ConfigureAwait(false);
        }
    }
}
