using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Colg_UWP.Model;
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

        private static string _formhash;

        public static string Formhash
        {
            set {
                if (String.IsNullOrEmpty(_formhash))
                {
                    _formhash = value;
                }
            }
        }

        public static async Task<(bool status,string message)> PostNewReplyAsync(string tid, string reply,string quotetid = null)
        {
            string url = quotetid == null ? ApiUrl.PostNewReply(tid) : ApiUrl.PostNewReplyWithQuote(tid, quotetid);
            Dictionary<string, string> parameters = new Dictionary<string, string>()
            {
                {"formhash",_formhash },
                {"mobiletype","2" },
                {"message",reply }
            };
            var json = await GetJson(url, parameters).ConfigureAwait(false);
            string message= json["Message"]["messageval"]?.ToString();
            bool status = message== "post_reply_succeed";
            return (status: status, message: message);
        }

        private static List<Reply> GetReplyFromArrayAsync(JToken[] postlist)
        {
            List<Reply> replys = new List<Reply>();
            foreach (var reply in postlist)
            {
                string replyId = (string)reply["position"];
                string author = (string)reply["author"];
                string timeStr = reply["dateline"].Value<string>();
                DateTime timeReplied = DateTime.Parse($"{timeStr} +08:00").ToLocalTime();
                string html = reply["message"].Value<string>();
                string avatar = reply["authoravatar"].Value<string>();
                string authorId = reply["authorid"].Value<string>();
                string markdown = Html2Markdown.ToMarkdown(html);
                replys.Add(new Reply
                {
                    Id = replyId,
                    Author = author,
                    TimeReplied = timeReplied,
                    Message = html,
                    Markdown = markdown,
                    Avatar = avatar,
                    AuthorId = authorId
                });
            }
            return replys;
        }

        
    }
}
