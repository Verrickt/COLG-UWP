using Colg_UWP.Model;
using Colg_UWP.Util;
using Colg_UWP.View.Pages;
using Windows.UI.Xaml.Controls;

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
            RefreshCommand = new RelayCommand(Refresh);
            JumpToReplyPageCommand = new RelayCommand<Frame>(
               (frame) => frame.Navigate(typeof(NewReplyPage), new ReplyVM(Discussion)),
               () => UserDataManager.GetActiveUser() != null
               );
        }

        private void Refresh()
        {
            Discussion.Refresh();
            ReplyList = new IncrementalList<Reply, Discussion>(Discussion);
        }
    }
}