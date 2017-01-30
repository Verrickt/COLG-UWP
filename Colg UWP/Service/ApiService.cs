using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Colg_UWP.Helper;
using Colg_UWP.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Colg_UWP.Service
{
    public class ApiService : ApiBaseService
    {

        private static UserData _userData;
        private static List<LoginData> _loginDatas;



        public static async Task InitAsync()
        {
            _userData = await UserDataManager.GetUserData().ConfigureAwait(false);
            _loginDatas = await LoginDataManager.GetLoginDatasAsync().ConfigureAwait(false);
        }

        #region User

        public static async Task UpdateUserCredits()
        {
            var json = await GetJson(ApiUrl.ValidateLogin).ConfigureAwait(false);
            var variable = json["Variables"].Value<JObject>();
            var credits = GetUserCredits(variable.Properties().Where(x => x.Name.Contains("credit")).Select(x => x.First));
            _userData.Credits = credits;
        }

        #endregion

        #region Login
        public static async Task<string> Login(string username, string password, int questionid, string answer)
        {
            Dictionary<string, string> dictionary = new Dictionary<string, string>
            {
                {"username", username},
                {"password", password},
                {"questionid", questionid.ToString()},
                {"answer", answer}
            };

            var json = await GetJson(ApiUrl.Login, dictionary).ConfigureAwait(false);

            var errorMessage = GetErrorMessage(json);

            if (errorMessage == null)
            {
                SettingManager.Save(SettingNames.LoginName, username);
                SettingManager.Save(SettingNames.UserTriggeredLoginStatus, true);
                UpdateUserInfo(json["data"]);
                _loginDatas.RemoveAll(x => x.UserName == username);
                _loginDatas.Add(new LoginData()
                {
                    UserName = username,
                    Password = password,
                    QuestionId = questionid,
                    QuestionAnswer = answer
                }
                    );
            }

            return errorMessage;
        }


        public static async Task<string> LogOut()
        {
            var json = await GetJson(ApiUrl.Logout).ConfigureAwait(false);
            var username = SettingManager.Read<string>(SettingNames.LoginName);

            SettingManager.Save<string>(SettingNames.LoginName, string.Empty);
            SettingManager.Save(SettingNames.UserTriggeredLoginStatus, false);
            return GetErrorMessage(json);
        }

        public static async Task<string> AutoLogin()
        {
            var username = SettingManager.Read<string>(SettingNames.LoginName);
            if (!String.IsNullOrEmpty(username))
            {
                var loginData = _loginDatas.Single(x => x.UserName == username);
                return await Login(loginData.UserName, loginData.Password, loginData.QuestionId, loginData.QuestionAnswer).ConfigureAwait(false);
            }
            return null;
        }
        #endregion

        #region Reply

        public static async Task<List<Reply>> ReplyList(string postId, int page, bool skipFirst = false)
        {
            var json = await GetJson(ApiUrl.ReplyList(postId, page)).ConfigureAwait(false);
            var variable = json["Variables"].Value<JObject>();
            var replyArray = variable["postlist"].ToArray();
            var replys = ReplyFromArray(replyArray);
            if (skipFirst && page == 1)
            {
                replys.RemoveAt(0);
            }
            return replys;
        }

        public static async Task<string> PostNewReply(string tid, string message, string quotetid = null)
        {
            string url = quotetid == null ? ApiUrl.PostNewReply(tid) : ApiUrl.PostNewReplyWithQuote(tid, quotetid);
            Dictionary<string, string> parameters = new Dictionary<string, string>()
            {
                {"formhash",_userData.FormHash },
                {"mobiletype","2" },
                {"message",message }
            };
            var json = await ApiService.GetJson(url, parameters).ConfigureAwait(false);
            if (json?["Message"]?["messageval"]?.ToString() != "post_reply_succeed")
            {
                return json["Message"]["messageval"]?.ToString();
            }
            else
            {
                return null;
            }
        }

        #endregion

        #region Discussion
        public static async Task<List<Discussion>> PopularPostList()
        {
            var json = await GetJson(ApiUrl.Home());
            var threads = json["threads"].ToArray();
            return PostListFromArray(threads, null);
        }
        public static async Task<List<Discussion>> PostListAsync(string forumId, int page)
        {
            var json = await GetJson(ApiUrl.PostList(forumId, page)).ConfigureAwait(false);
            var variable = json["Variables"].Value<JObject>();
            UpdateUserInfo(variable);
            Debug.WriteLine(_userData.ToString());
            Dictionary<string, string> catagoryDict = null;
            var forumThreads = variable["forum_threadlist"].ToArray();
            var catagoryObj = variable["threadtypes"];
            if (catagoryObj != null)
            {
                catagoryDict = JsonConvert.DeserializeObject<Dictionary<string, string>>(catagoryObj["types"]?.ToString());
            }
            return PostListFromArray(forumThreads, catagoryDict);
        }

        #endregion  

        #region Forum
        public static async Task<List<Forum>> ForumListAsync()
        {
            var json = await GetJson(ApiUrl.ForumList()).ConfigureAwait(false);
            if (json != null)
            {
                List<Forum> forums = new List<Forum>();
                Dictionary<string, string> catagorys = new Dictionary<string, string>();
                var variable = json["Variables"];
                UpdateUserInfo(variable);
                Debug.WriteLine(_userData.ToString());
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


        #endregion

        #region Article
        public static async Task<ArticleContainer> NewsByTypeId(string pid)
        {
            var newsJson = await GetJson(ApiUrl.NewsList(pid, 1)).ConfigureAwait(false);
            int maxCount = Int32.Parse(newsJson["INFO"]["count"].ToString());
            return new ArticleContainer { Id = pid, MaxCount = maxCount };
        }
        public static async Task<List<Article>> NewsList(string aid, int page)
        {
            var newsJson = await GetJson(ApiUrl.NewsList(aid, page)).ConfigureAwait(false);
            var newsArray = newsJson["DATA"].ToArray();
            return NewsListFromArray(newsArray);
        }
        public static async Task InitNewsContent(Article news)
        {
            var json = await GetJson(ApiUrl.NewsContent(news.Id));
            news.Content = json["DATA"]["content"].ToString();
            news.Author = json["DATA"]["forum_user_info"]["username"].ToString();
        }

        #endregion

        #region Private Helper Methods
        private static Dictionary<string, string> GetUserCredits(IEnumerable<JToken> tokens)
        {
            Dictionary<string, string> credits = new Dictionary<string, string>();
            foreach (var token in tokens)
            {
                credits.Add(token["title"].Value<string>(), token["value"].ToString());
            }
            return credits;
        }

        private static string GetErrorMessage(JToken json)
        {
            var errorCode = Convert.ToInt32(json["error_code"].ToString());
            var errorMsg = json["error_msg"].Value<string>();
            if (errorCode != 0)
            {
                return errorMsg;
            }
            return null;
        }


        private static void UpdateUserInfo(JToken json)
        {
            string userid = json.ValueForEitherName("member_uid", "uid");
            string username = json.ValueForEitherName("member_username", "username");
            string avatar = json["member_avatar"]?.ToString().Replace("small", "big");
            int readAccessLevel = Convert.ToInt32(json["readaccess"]?.ToString());
            string grouptitle = json["group"]?["grouptitle"]?.ToString();
            string formhash = json["formhash"]?.ToString();
            _userData.UserID = userid;
            _userData.AvatarUrl = avatar;
            _userData.GroupTitle = _userData.GroupTitle ?? grouptitle;
            _userData.HomeUrl = null;
            _userData.UserName = username;
            _userData.ReadPermission = readAccessLevel;
            var currentLoginData = _loginDatas.SingleOrDefault(x => x.UserName == username);
            if (currentLoginData != null)
            {
                currentLoginData.AvatarUrl = avatar;
            }
        }


        private static List<Discussion> PostListFromArray(JToken[] array, Dictionary<string, string> catagoryDict)
        {
            List<Discussion> postList = new List<Discussion>();
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

                postList.Add(new Discussion
                {
                    Catagory = catagory,
                    Id = threadId,
                    Author = author,
                    AuthorId = authorId,
                    Subject = subject,
                    TimePosted = timePosted,
                    TimeLastPosted = lastReplyTime,
                    LastPoster = lastPoster,
                    MaxCount = reply,
                    Avatar = avatar,
                    ReadPermission = readpermission
                });
            }
            return postList;
        }

        


       

        private static List<Reply> ReplyFromArray(JToken[] postlist)
        {
            List<Reply> replys = new List<Reply>();
            foreach (var reply in postlist)
            {
                string replyId = (string)reply["position"];
                string author = (string)reply["author"];
                string timeReplied = reply["dateline"].Value<string>();
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

        #endregion


    }

    static class Helper
    {
        public static string ValueForEitherName(this JToken token, string key1, string key2)
        {
            var str1 = token[key1]?.ToString();
            var str2 = token[key2]?.ToString();

            if (str1 != null && str2 != null)
            {
                throw new ArgumentException("Both the key contains a valid value");
            }
            if (str1 == null && str2 == null)
            {
                throw new NullReferenceException("Can't find a value by both key");
            }
            return str1 ?? str2;
        }
    }
