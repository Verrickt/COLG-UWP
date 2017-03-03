using Colg_UWP.Model;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Colg_UWP.Util;

namespace Colg_UWP.Service
{
    public class UserService : ApiBaseService
    {



        public static async Task UpdateUserCreditsAsync(User user)
        {
            if (user!=null)
            {
                var json = await GetJson(ApiUrl.ValidateLogin).ConfigureAwait(false);
                var variable = json["Variables"].Value<JObject>();
                var credits =
                    GetUserCredits(variable.Properties().Where(x => x.Name.Contains("credit")).Select(x => x.First));
                user.Credits.Clear();
                string formhash = variable["formhash"].ToString();
                ReplyService.Formhash = formhash;
                user.Credits.AddRange(credits); 
            }
        }

        public static void UpdateUserInfo(JToken json,User user)
        {

            if (user!=null)
            {
                string userid = json.ValueForEitherName("member_uid", "uid");
                string username = json.ValueForEitherName("member_username", "username");
                string avatar = json["member_avatar"]?.ToString().Replace("small", "big");
                int readAccessLevel = Convert.ToInt32(json["readaccess"]?.ToString());
                string grouptitle = json["group"]?["grouptitle"]?.ToString();
                string formhash = json["formhash"]?.ToString();
                DateTimeOffset? timeRegisted = null;
                if (long.TryParse(json["regdate"]?.ToString(), out long regDate))
                {
                    timeRegisted = DateTimeOffset.FromUnixTimeSeconds(regDate).ToLocalTime();
                }
                ReplyService.Formhash = formhash;
                user.ID = userid;
                user.Avatar = avatar;
                user.GroupTitle = grouptitle;
                user.HomeUrl = null;
                user.UserName = username;
                user.ReadPermission = readAccessLevel;
                user.TimeRegisted = user.TimeRegisted ?? timeRegisted;
            }
           
        }

        public static async Task UpdateUserInfoAsync(User user)
        {
            if (user!=null)
            {
                var json = await GetJson(ApiUrl.ForumList()).ConfigureAwait(false); //request forumlist to get avatar 
                var variable = json["Variables"];
                await UpdateUserCreditsAsync(user);
                UpdateUserInfo(variable, user); 
            }
        }

       

        private static IEnumerable<string> GetUserCredits(IEnumerable<JToken> tokens)

        {
            Dictionary<string, string> credits = new Dictionary<string, string>();

            foreach (var token in tokens)
            {
                credits.Add(token["title"].Value<string>(), token["value"].ToString());
            }
            return credits.Select(x => $"{x.Key}:{x.Value}");
        }
    }
}
