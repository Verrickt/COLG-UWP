using Colg_UWP.Model;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Colg_UWP.Util;

namespace Colg_UWP.Service
{
    public class ForumService:ApiBaseService
    {
        public static async Task<List<Forum>> GetForumsAsync()
        {
            var json = await GetJson(ApiUrl.ForumList()).ConfigureAwait(false);
            if (json != null)
            {
                List<Forum> forums = new List<Forum>();
                Dictionary<string, string> catagorys = new Dictionary<string, string>();
                var variable = json["Variables"];
                UserService.UpdateUserInfo(variable,UserDataManager.GetActiveUser());
                var catagoryObj = variable["catlist"].ToArray();
                foreach (var catagory in catagoryObj)
                {
                    string catagoryName = catagory["name"].Value<string>();
                    var forumsObj = catagory["forums"].Values<string>();
                    forumsObj.ToList().ForEach(x => catagorys.Add(x, catagoryName));
                }
                var forumlist = variable["forumlist"].ToArray();
                foreach (var forum in forumlist)
                {
                    string id = forum["fid"].Value<string>();
                    string name = forum["name"].Value<string>();
                    string iconUri = forum["icon"].Value<string>();
                    string postsToday = forum["todayposts"].Value<string>();
                    int threadNum = int.Parse(forum["threads"].Value<string>());
                    string catagory;
                    if (!catagorys.TryGetValue(id, out catagory))
                    {
                        catagory = "其他";
                    }
                    forums.Add(new Forum
                    {
                        Id = id,
                        Catagory = catagory,
                        IconUri = iconUri,
                        MaxCount = threadNum,
                        Name = name,
                        PostToday = postsToday
                    });
                }


                return forums;
            }
            return null;
        }
    }
}
