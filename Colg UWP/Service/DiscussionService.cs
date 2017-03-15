using Colg_UWP.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Web.Http;
using Colg_UWP.Util;

namespace Colg_UWP.Service
{
    public class DiscussionService:ApiBaseService
    {
        private static string successMsg = "非常感谢，您的主题已发布，现在将转入主题页，请稍候……[ 点击这里转入主题列表 ]";


        public static async Task<List<Discussion>> GetPopularDiscussionsAsync()
        {
            var json = await GetJson(ApiUrl.Home());
            var threads = json["threads"].ToArray();
            return GetDiscussionsFromArrayAsync(threads, null);
        }
        public static async Task<(int,List<Discussion>)> GetDiscussionsAsync(Forum forum, int page)
        {
            var json = await GetJson(ApiUrl.PostList(forum.Id, page)).ConfigureAwait(false);
            var variable = json["Variables"].Value<JObject>();
            UserService.UpdateUserInfo(variable, UserDataManager.GetActiveUser());
            Dictionary<string, string> catagoryDict = null;
            var forumThreads = variable["forum_threadlist"].ToArray();
            var catagoryObj = variable["threadtypes"];
            if (catagoryObj != null)
            {
                catagoryDict = JsonConvert.DeserializeObject<Dictionary<string, string>>(catagoryObj["types"]?.ToString());
            }
            forum.PostTypes = catagoryDict ?? new Dictionary<string, string>();
            int newCount = Convert.ToInt32(variable["forum"]["threads"].ToString());
            var items =  GetDiscussionsFromArrayAsync(forumThreads, catagoryDict);
            return (newCount, items);
        }

        public static async Task<(bool,string)> PostNewDiscussionAsync(string fid,string typeId,string subject,string message)
        {
            var url = ApiUrl.PostNewDiscussion(fid);
            var parameters = new Dictionary<string, string>()
            {
                {"mobiletype","2" },
                {"formhash",UserDataManager.GetActiveUser().FormHash},
                {"subject",subject },
                {"message",message },
                {"typeid",typeId??string.Empty },

            };

            

            var json = await ApiBaseService.GetJson(url, parameters);

            var msg = json["Message"]["messagestr"].ToString();

            return (msg==DiscussionService.successMsg, msg);

        }
   /*     Content of json
    *     {
  "Version": "4",
  "Charset": "UTF-8",
  "Variables": {
    "cookiepre": "5KaR_6d30_",
    "auth": "618dDj6tlrhltK44AoB57ELQNjj7RVwCohv9F3GmSd3TpVCAnwYzNUMmOx5wFxFz7hU/QdGzRguEYAOMjJ9UfR/4NAQ",
    "saltkey": "iOaO6TRT",
    "member_uid": "789572",
    "member_username": "???",
    "member_avatar": "???",
    "groupid": "60",
    "formhash": "???",
    "ismoderator": "0",
    "readaccess": "60",
    "notice": {
      "newpush": "0",
      "newpm": "0",
      "newprompt": "0",
      "newmypost": "0"
    },
    "tid": "6145075",
    "pid": "87949289"
  },
  "Message": {
    "messageval": "post_newthread_succeed",
    "messagestr": "非常感谢，您的主题已发布，现在将转入主题页，请稍候……[ 点击这里转入主题列表 ]"
  }
}*/

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
