using Colg_UWP.Model;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Colg_UWP.Util;
using Newtonsoft.Json;

namespace Colg_UWP.Service
{
    public class UserService : ApiBaseService
    {

        private static List<UserGroup> _userGroups;

        public static IEnumerable<UserGroup> UserGroups { get { return _userGroups.AsReadOnly(); } }

        public static async Task UpdateUserCreditsAsync(User user)
        {
            if (user != null)
            {
                var json = await GetJson(ApiUrl.ValidateLogin).ConfigureAwait(false);
                var variable = json["Variables"].Value<JObject>();
                var pairs =
                    GetUserCredits(variable.Properties().Where(x => x.Name.Contains("credit")).Select(x => x.First));
                user.Credits?.Clear();
                var credits = pairs.Select(pair => new Credit { Name = pair.Key, Value = pair.Value });
                user.Credits?.AddRange(credits);
                string formhash = variable["formhash"].ToString();
                user.FormHash = formhash;
            }
        }

        public static void UpdateUserInfo(JToken json, User user)
        {

            if (user != null)
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
                user.FormHash = formhash;
                user.ID = userid;
                user.Avatar = avatar;
                user.UserGroup = _userGroups.SingleOrDefault(g => g.Title == grouptitle)??
                    user.UserGroup;
                user.HomeUrl = null;
                user.UserName = username;
                user.TimeRegisted = user.TimeRegisted ?? timeRegisted;
            }

        }

        public static async Task UpdateUserInfoAsync(User user)
        {
            if (user != null)
            {
                var json = await GetJson(ApiUrl.ForumList()).ConfigureAwait(false); //request forumlist to get avatar 
                var variable = json["Variables"];
                await UpdateUserCreditsAsync(user);
                UpdateUserInfo(variable, user);
            }
        }

        public static async Task InitializationAsync()
        {
            await GetUserGroupsAsync().ConfigureAwait(false);
        }

        private static async Task GetUserGroupsAsync()
        {
            var url = ApiUrl.UserGroup;
            var json = await ApiBaseService.GetJson(url).ConfigureAwait(false);
            var userGroupObj = json["Variables"]["usergroups"].Value<JObject>();
            _userGroups = new List<UserGroup>();
            foreach (var token in userGroupObj)
            {
                var userGroup = token.Value;
                string title = userGroup["grouptitle"].ToString();
                string higher = userGroup["creditshigher"]?.ToString();
                string lower = userGroup["creditslower"]?.ToString();
                int readAccess = int.Parse(userGroup["readaccess"].ToString());
                _userGroups.Add(new UserGroup()
                {
                    Title = title,
                    ReadPermissionLevel = readAccess,
                    CreditRange = new CreditRange
                    {
                        UpperBound = lower == null?(int?)null:int.Parse(lower),
                        LowerBound = higher == null ? (int?)null : int.Parse(higher)
                    }
                }
                    );
               
            }
            
        }

//{
//  "Variables": 
//        {
//    "usergroups": 
//            {
//      "9": 
//                {
//        "type": "member",
//        "grouptitle": "乞丐",
//        "creditshigher": "-999999999",
//        "creditslower": "0",
//        "stars": "0",
//        "color": "",
//        "icon": "",
//        "readaccess": "0",
//        "allowgetattach": "1",
//        "allowgetimage": "1",
//        "allowmediacode": "0",
//        "maxsigsize": "0",
//        "allowbegincode": "0",
//        "userstatusby": "1"
//                },
//      "1": 
//                {
//        "type": "system",
//        "grouptitle": "黄金波比侠",
//        "stars": "51",
//        "color": "",
//        "icon": "",
//        "readaccess": "200",
//        "allowgetattach": "1",
//        "allowgetimage": "1",
//        "allowmediacode": "1",
//        "maxsigsize": "2000",
//        "allowbegincode": "1",
//        "userstatusby": "1"
//                }
//            }
//        }
//}

        private static IEnumerable<KeyValuePair<string,int>> GetUserCredits(IEnumerable<JToken> tokens)

        {
            Dictionary<string, int> credits = new Dictionary<string, int>();

            foreach (var token in tokens)
            {
                credits.Add(token["title"].Value<string>(), int.Parse(token["value"].ToString()));
            }
            return credits;
        }
    }
}
