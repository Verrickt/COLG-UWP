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

        private static UserData _userData = new UserData();

        public static  UserData GetUserData()
        {
            return _userData;
        }

        public static async Task InitializationAsync()
        {
            _userData = await StorageHelper.ReadAsync<UserData>(_fileName)
            ??
            _userData;
        }

        public static async Task SaveUserData()
        {
            await StorageHelper.SaveAsync(_userData, _fileName).ConfigureAwait(false);
        }
    }
}
