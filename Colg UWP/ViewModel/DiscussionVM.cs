using System;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using Colg_UWP.Util;
using Colg_UWP.Model;
using Colg_UWP.Service;
using Colg_UWP.View.Pages;

namespace Colg_UWP.ViewModel
{
    using IncrementalLoading;

    public class DiscussionVM : VMBase
    {

        public Discussion Discussion
        {
            get { return _discussion; }
            set
            {
                SetProperty(ref _discussion, value);
                Refresh();
                JumpToReplyPageCommand.RaiseCanExecuteChanged();
            }
        }

        private Discussion _discussion;

        public string ReplyMessage
        {
            get { return _replyMessage; }
            set { SetProperty(ref _replyMessage, value); }
        }

        public string QuotedId { get; set; } = null;

        public IncrementalList<Reply, Discussion> ReplyList
        {
            get { return _replyList; }
            private set { SetProperty(ref _replyList, value); }
        }

        public RelayCommand RefreshCommand { get; set; }

        public RelayCommand<Frame> JumpToReplyPageCommand { get; set; }


        private IncrementalList<Reply, Discussion> _replyList;
        private string _replyMessage;
       

        public DiscussionVM()
        {
            ReplyMessage = String.Empty;
            RefreshCommand = new RelayCommand(Refresh);
            JumpToReplyPageCommand = new RelayCommand<Frame>(
               (frame) => frame.Navigate(typeof(NewReplyPage),this),
               () => UserDataManager.GetActiveUser() != null
               );

        }

        private void Refresh()
        {
            Discussion.Refresh();
            ReplyList = new IncrementalList<Reply, Discussion>(Discussion);
        }


        

        public async Task<bool> PostNewReplyAsync()
        {
            (bool status,string message)= await ReplyService.PostNewReplyAsync(Discussion.Id, ReplyMessage);

            if (!status)
            {
                InAppNotifier.Show(message);
            }
            else
            {
                ReplyMessage = string.Empty;
            }
            return status;
        }
    }
}
