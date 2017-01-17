using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Colg_UWP.Model;

namespace Colg_UWP.Helper
{
    public  class LoginDataManager
    {
        private static List<LoginData> _loginDatas;

        public static async Task<List<LoginData>> GetLoginDatas()
        {
            return _loginDatas ??
                   (_loginDatas = await StorageHelper.Read<List<LoginData>>(StorageNames.LoginData)) ??
                   (_loginDatas = new List<LoginData>());
        }

        public static async Task SaveLoginDatas()
        {
            await StorageHelper.Save(_loginDatas, StorageNames.LoginData);
        }
    }
}
