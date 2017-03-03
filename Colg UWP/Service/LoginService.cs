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
        public static async Task<(bool IsSuccess,string ErrorMessage)> LoginAsync(User user)
        {
            var credential = user.Credential;
            Dictionary<string, string> dictionary = new Dictionary<string, string>
            {
                {"username", credential.LoginName},
                {"password", credential.Password},
                {"questionid", credential.QuestionId.ToString()},
                {"answer", credential.QuestionAnswer}
            };
            Util.Logging.WriteLine($"Login with {credential}");


            var json = await GetJson(ApiUrl.Login, dictionary).ConfigureAwait(false);

            var result = GetOperationResult(json);

            if (result.IsSuccess)
            {
                user.IsActive = true;
                UserService.UpdateUserInfo(json["data"],user);
                await UserService.UpdateUserInfoAsync(user).ConfigureAwait(false);
                UserDataManager.AddOrUpdateUser(user);
                await UserDataManager.SaveUserData();
            }

            Logging.WriteLineIf(result.IsSuccess,"Login success","Login failed");

            return result;
        }

        public static async Task<(bool IsSuccess, string ErrorMessage)> LogoutAsync()
        {
            var activeUser = UserDataManager.GetActiveUser();
            var json = await GetJson(ApiUrl.Logout).ConfigureAwait(false);
            activeUser.IsActive = false;
            await UserDataManager.SaveUserData();
            return GetOperationResult(json);
        }

        public static async Task<(bool IsSuccess, string ErrorMessage)> AutoLoginAsync()
        {
            var activeUser = UserDataManager.GetActiveUser();
            if (activeUser!=null)
            {
                return await LoginAsync(activeUser).ConfigureAwait(false);
            }
            Logging.WriteLine("Auto login failed. No active user");
            return (IsSuccess:false,ErrorMessage:"No active user!!");
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
