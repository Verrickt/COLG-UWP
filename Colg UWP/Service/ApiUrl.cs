using System;

namespace Colg_UWP.Service
{
    public class ApiUrl
    {
        private static string _baseUrl = "http://bbs.colg.cn/api/mobile/index.php?mobile=no&version=4";


        /// <summary>
        /// field required in body:
        /// subject 
        /// message 
        /// mobile type : 2
        /// typeid :thread type 
        /// formhash
        /// </summary>


        private static string _postNewDiscussion = _baseUrl+"&module=newthread&fid={0}&topicsubmit=1";

        private static int _itemsPerPage = 15;
        private static string _discussionList = _baseUrl+"&module=forumdisplay&submodule=checkpost&fid={0}&page={1}&tpp={2}&orderby=lastpost";
        private static string _replyList = _baseUrl+"&module=viewthread&submodule=checkpost&tid={0}&page={1}&ppp={2}&orderby=lastp";
        private static string _forumList = _baseUrl+"&module=forumindexcustom";
        private static string _newsList = "http://www.colg.cn/api/newslist?pid={0}&page={1}";
        private static string _validateLogin = _baseUrl+"&module=credit";
        private static string _postNewReply =_baseUrl+"&module=sendreply&tid={0}&extra=&replysubmit=1";
        private static string _favDiscussion = _baseUrl + "&module=myfavthread";

        /// <summary>
        /// Requires 
        /// mobiletype = 2, reppid = DiscussionID,reppost = DiscussionID,message = text to reply
        /// </summary>


        private static string _postNewReplyQuote = _baseUrl + "&module=sendreply&tid={0}&extra=&replysubmit=1&repquote={1}";
        /// <summary>
        /// 首页 热门讨论及热点
        /// </summary>

        private static string _home = "http://www.colg.cn/apiv2/home";
        private static string _newsContent = "http://www.colg.cn/api/detail?aid={0}";
        private static string _login = _baseUrl+"&module=login&action=login";

        private static string _logout =
            _baseUrl+"&module=login&action=logout";



        /// <summary>
        /// all the my posts that get replied
        /// </summary>

        private static string _notelist = "&module=mynotelist";

        /// <summary>
        /// Check all user groups
        /// </summary>
        private static string _userGroup = _baseUrl+"&module=usergroups";


        /// <summary>
        /// Sign up on mobile. Requires formhash on body
        /// </summary>
        private static string _mobileSign = _baseUrl+ "&module=mobilesign";


        public static string MobileSign = _mobileSign;
        public static string UserGroup=>_userGroup;
        public static string ForumList() => _forumList;
        public static string PostList(string fid, int page) => String.Format(_discussionList, fid, page, _itemsPerPage);
        public static string ReplyList(string tid, int page) => String.Format(_replyList, tid, page, _itemsPerPage);
        public static string ValidateLogin => _validateLogin;
        public static string PostNewReply(string tid) => String.Format(_postNewReply, tid);
        public static string PostNewDiscussion(string fid)=>String.Format(_postNewDiscussion,fid);
        public static string PostNewReplyWithQuote(string tid, string quotetid)
            => String.Format(_postNewReplyQuote, tid, quotetid);
        /// <summary>
        /// Url of the news with given pid and page.If not on the last page,there should be 10 newsitems in the returned json
        /// </summary>
        /// <param name="pid">
        /// Id for the news.1&2 for news,16 for prospective,17 for given categories.
        /// </param>
        /// <param name="page">Page of the list</param>
        /// <returns></returns>

        public static string ArticleList(string pid,int page)=>String.Format(_newsList,pid,page);
        public static string Logout => _logout;
        public static string Login => _login;
        public static string Home() => _home;
        public static string ArticleContent(string aid) => String.Format(_newsContent, aid);
    }
}
