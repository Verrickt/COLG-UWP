using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Colg_UWP.Model;

namespace Colg_UWP.Util
{
    public  class LoginDataManager
    {
        private static List<LoginData> _loginDatas= new List<LoginData>();

        public static  IReadOnlyList<LoginData> GetLoginDataList()
        {
           return _loginDatas.AsReadOnly();
        }

        private static string _fileName = "LoginData";
        public static async Task InitializtionAsync()
        {
            _loginDatas =
                 await StorageHelper.ReadAsync<List<LoginData>>
                (_fileName)
             ?? _loginDatas;
        }

        public static async Task SaveLoginDatasAsync()
        {
            await StorageHelper.SaveAsync(_loginDatas, _fileName);
        }

        public static void AddLoginData(LoginData data)
        {
            _loginDatas.Add(data);
        }

        public static void RemoveLoginData(LoginData data)
        {
            _loginDatas.Remove(data);
        }
        public static void RemoveLoginData(string username)
        {
            RemoveLoginData(_loginDatas.SingleOrDefault(d => d.UserName == username));
        }
    }
}
