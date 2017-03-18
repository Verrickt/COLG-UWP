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
        private static List<Credential> _loginDatas= new List<Credential>();

        public static  IReadOnlyList<Credential> GetLoginDataList()
        {
           return _loginDatas.AsReadOnly();
        }

        private static string _fileName = "LoginData";
        public static async Task InitializtionAsync()
        {
            _loginDatas =
                 await StorageHelper.ReadAsync<List<Credential>>
                (_fileName)
             ?? _loginDatas;
        }

        public static async Task SaveLoginDatasAsync()
        {
            await StorageHelper.SaveAsync(_loginDatas, _fileName);
        }

        public static void AddLoginData(Credential credential)
        {
            _loginDatas.Add(credential);
        }

        public static void RemoveLoginData(Credential credential)
        {
            _loginDatas.Remove(credential);
        }
        public static void RemoveLoginData(string username)
        {
            RemoveLoginData(_loginDatas.SingleOrDefault(d => d.LoginName == username));
        }
    }
}
