using Colg_UWP.Util;
using Colg_UWP.Model;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colg_UWP.Service
{
    public class LoginService:ApiBaseService
    {
        public static async Task<(bool IsSuccess,string ErrorMessage)> LoginAsync(LoginData loginData)
        {
            Dictionary<string, string> dictionary = new Dictionary<string, string>
            {
                {"username", loginData.UserName},
                {"password", loginData.Password},
                {"questionid", loginData.QuestionId.ToString()},
                {"answer", loginData.QuestionAnswer}
            };
            Util.Logging.WriteLine($"Login with {loginData}");


            var json = await GetJson(ApiUrl.Login, dictionary).ConfigureAwait(false);

            var result = GetOperationResult(json);

            if (result.IsSuccess)
            {
                loginData.IsActive = true;
                UserService.UpdateUserInfo(json["data"]);
                await UserService.UpdateUserCreditsAsync().ConfigureAwait(false);
                LoginDataManager.RemoveLoginData(loginData.UserName);
                LoginDataManager.AddLoginData(loginData);
                await LoginDataManager.SaveLoginDatasAsync().ConfigureAwait(false);
            }

            Logging.WriteLineIf(result.IsSuccess,"Login success","Login failed");

            return result;
        }

        public static async Task<(bool IsSuccess, string ErrorMessage)> LogoutAsync()
        {
            var json = await GetJson(ApiUrl.Logout).ConfigureAwait(false);
            var userData = LoginDataManager.GetLoginDataList().Single(i => i.IsActive);
            userData.IsActive = false;
            await LoginDataManager.SaveLoginDatasAsync().ConfigureAwait(false);
            return GetOperationResult(json);
        }

        public static async Task<(bool IsSuccess, string ErrorMessage)> AutoLoginAsync()
        {
            var list =  LoginDataManager.GetLoginDataList();
            var loginData = list.SingleOrDefault(ld => ld.IsActive);
            if (loginData!=null)
            {
                return await LoginAsync(loginData).ConfigureAwait(false);
            }
            return (IsSuccess:false,ErrorMessage:"username doesn't exist");
        }
        private static (bool IsSuccess, string ErrorMessage) GetOperationResult(JToken json)
        {
            bool successed = true;
            string message="";
            var errorCode = Convert.ToInt32(json["error_code"].ToString());
            var errorMsg = json["error_msg"].Value<string>();
            if (errorCode != 0)
            {
                successed = false;
            }
            message = errorMsg;
            return (successed, message);
        }

    }
}
