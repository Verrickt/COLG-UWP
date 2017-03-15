using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Colg_UWP.Model;
using Colg_UWP.Util;
using Newtonsoft.Json.Linq;

namespace Colg_UWP.Service
{
    public class ReplyService:ApiBaseService
    {
        public static async Task<(int newcound,List<Reply> items)> GetReplysAsync(string postId, int page)
        {
            var json = await GetJson(ApiUrl.ReplyList(postId, page)).ConfigureAwait(false);
            var variable = json["Variables"].Value<JObject>();
            var replyArray = variable["postlist"].ToArray();
            var replys = GetReplyFromArrayAsync(replyArray);
            int newCount = Convert.ToInt32(variable["thread"]["replies"].ToString());
            return (newCount, replys);
        }

       

        public static async Task<(bool status,string message)> PostNewReplyAsync(string discussionID, string reply,string quotetid = null)
        {
            string url = quotetid == null ? ApiUrl.PostNewReply(discussionID) : ApiUrl.PostNewReplyWithQuote(discussionID, quotetid);
            Dictionary<string, string> parameters = new Dictionary<string, string>()
            {
                {"formhash",UserDataManager.GetActiveUser().FormHash },
                {"mobiletype","2" },
                {"message",reply },
            };
            var json = await GetJson(url, parameters).ConfigureAwait(false);
            string messageval= json["Message"]["messageval"]?.ToString();
            string messagestr = json["Message"]["messagestr"]?.ToString();
            bool status = messageval== "post_reply_succeed";
            return (status: status, message: messagestr);
        }

//Content of JSON    
//{
//  "Version": "4",
//  "Charset": "UTF-8",
//  "Variables": {
//    "auth": "8cedq4494tpFzL8K5fL7rKZbWKn2/4x8EeOCxKGc2k1HLWeBlZEGiA/tY1K86VHtfz2s2neethiIHcJBv+pXcSalLdc",
//    "saltkey": "iOaO6TRT",
//    "member_username": "???",
//    "member_avatar": "???",
//    "groupid": "60",
//    "formhash": "84a43774",
//    "ismoderator": "0",
//    "readaccess": "60",
//    "notice": {
//      "newpush": "0",
//      "newpm": "0",
//      "newprompt": "0",
//      "newmypost": "0"
//    },
//    "tid": "???",
//    "pid": "0",
//    "noticeauthor": null
//  },
//  "Message": {
//    "messageval": "word_banned",
//    "messagestr": "抱歉，您填写的内容包含不良信息而无法提交"
//  }
//}


    private static List<Reply> GetReplyFromArrayAsync(JToken[] postlist)
        {
            List<Reply> replys = new List<Reply>();
            foreach (var reply in postlist)
            {
                string position = (string)reply["position"];
                string author = (string)reply["author"];
                string timeStr = reply["dateline"].Value<string>();
                DateTime timeReplied = DateTime.Parse($"{timeStr} +08:00").ToLocalTime();
                string html = reply["message"].Value<string>();
                string avatar = reply["authoravatar"].Value<string>();
                string authorId = reply["authorid"].Value<string>();
                string replyId = (string) reply["pid"];
                string markdown = Html2Markdown.ToMarkdown(html);
                replys.Add(new Reply
                {
                    Id = replyId,
                    Author = author,
                    TimeReplied = timeReplied,
                    Message = html,
                    Markdown = markdown,
                    Avatar = avatar,
                    AuthorId = authorId,
                    Position = position
                });
            }
            return replys;
        }

        
    }
}
