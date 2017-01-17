using System.Threading.Tasks;
using Colg_UWP.Helper;
using Colg_UWP.Model;
using Colg_UWP.Service;

namespace Colg_UWP.ViewModel
{
    using IncrementalLoading;

    public class DiscussionVM : VMBase
    {
        public string ThreadId { get; set; }

        public Discussion Discussion
        {
            get { return _discussion; }
            set
            {
                _discussion = value;
                OnPropertyChanged();
                Refresh();
            }
        }

        private Discussion _discussion;

        public string ReplyMessage
        {
            get { return _replyMessage; }
            set { _replyMessage = value;OnPropertyChanged(); }
        }

        public string QuotedId { get; set; } = null;

        public IncrementalList<Reply,Discussion> ReplyList
        {
            get { return _replyList; }
            private set
            {
                _replyList = value;
                OnPropertyChanged();
            }
        }



        private IncrementalList<Reply,Discussion> _replyList;
        private string _replyMessage;
        private bool _showCommentBox;

        public bool ShowCommentBox
        {
            get { return _showCommentBox; }
            set { _showCommentBox = value; OnPropertyChanged();}
        }

        private string _subject;

        public string Subject
        {
            get { return _subject; }
            set { _subject = value;OnPropertyChanged(); }
        }

        public DiscussionVM()
        {
            
        }

        public void Refresh()
        {
            Discussion.Refresh();
            ReplyList = new IncrementalList<Reply,Discussion>(Discussion);
            ReplyList.DataFilled += (s, e) => Subject = e.Data.Markdown;
        }

        

        public void CancelPostNewReply()
        {
            ShowCommentBox = false;
        }

        public async Task<bool> PostNewReply()
        {
            string errorMessage = await ApiService.PostNewReply(Discussion.Id, ReplyMessage);
            var result = errorMessage == null;
            if (result)
            {
                ShowCommentBox = false;
            }
            return result;
        }
    }
}
