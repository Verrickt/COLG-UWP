using Colg_UWP.Model;
using Newtonsoft.Json;
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
    public class DiscussionService:ApiBaseService
    {
        public static async Task<List<Discussion>> GetPopularDiscussionsAsync()
        {
            var json = await GetJson(ApiUrl.Home());
            var threads = json["threads"].ToArray();
            return GetDiscussionsFromArrayAsync(threads, null);
        }
        public static async Task<(int,List<Discussion>)> GetDiscussionsAsync(string forumId, int page)
        {
            var json = await GetJson(ApiUrl.PostList(forumId, page)).ConfigureAwait(false);
            var variable = json["Variables"].Value<JObject>();
            UserService.UpdateUserInfo(variable);
            Dictionary<string, string> catagoryDict = null;
            var forumThreads = variable["forum_threadlist"].ToArray();
            var catagoryObj = variable["threadtypes"];
            if (catagoryObj != null)
            {
                catagoryDict = JsonConvert.DeserializeObject<Dictionary<string, string>>(catagoryObj["types"]?.ToString());
            }
            int newCount = Convert.ToInt32(variable["forum"]["threads"].ToString());
            var items =  GetDiscussionsFromArrayAsync(forumThreads, catagoryDict);
            return (newCount, items);
        }
        private static List<Discussion> GetDiscussionsFromArrayAsync(JToken[] array, Dictionary<string, string> catagoryDict)
        {
            List<Discussion> discussionList = new List<Discussion>();
            foreach (var thread in array)
            {
                string threadId = thread.ValueForEitherName("tid", "thread_id");
                string author = thread["author"].Value<string>();
                string authorId = thread["authorid"].Value<string>();
                string subject = thread["subject"].Value<string>();
                string strLastReplyTime = thread["lastpost"]?.ToString();
                string lastPoster = thread["lastposter"]?.ToString();
                int reply = Convert.ToInt32(thread["replies"].ToString());
                string avatar = thread.ValueForEitherName("avatar", "cover_url");
                string typeId = thread["typeid"]?.ToString();
                string dateline = thread["dateline"].Value<string>();
                int readpermission = Convert.ToInt32(thread["readperm"]?.Value<string>());
                DateTime? timePosted = Helper.StringToDateTime(dateline);


                DateTime? lastReplyTime = Helper.StringToDateTime(strLastReplyTime);

                string catagory = "其他";

                if (catagoryDict?.ContainsKey(typeId) ?? false)
                {
                    catagory = catagoryDict[typeId];
                }
                else
                {
                    catagory = "其他";
                }

                discussionList.Add(new Discussion
                {
                    Catagory = catagory,
                    Id = threadId,
                    Author = author,
                    AuthorId = authorId,
                    Subject = subject,
                    TimePosted = timePosted,
                    TimeLastPosted = lastReplyTime,
                    LastPoster = lastPoster,
                    Reply = reply,
                    Avatar = avatar,
                    ReadPermission = readpermission
                });
            }
            return discussionList;
        }
       
    }
}
