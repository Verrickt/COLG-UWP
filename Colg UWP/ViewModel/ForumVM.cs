using Colg_UWP.Model;
using Colg_UWP.Util;
using Colg_UWP.View.Pages;
using System;
using System.Threading.Tasks;
using Windows.UI.Popups;

namespace Colg_UWP.ViewModel
{
    using IncrementalLoading;
    using Windows.UI.Xaml.Controls;

    public class ForumVM : VMBase
    {
        public string ForumId { get; set; }
        private Forum _forum;
        public Forum Forum { get { return _forum; } set { SetProperty(ref _forum, value); Refresh(); } }
        private IncrementalList<Discussion, Forum> _discussionList;

        public RelayCommand<Frame> JumpToNewDiscussionPageCommand { get; set; }

        public ForumVM()
        {
            RefreshCommand = new RelayCommand(Refresh, () => true);
            JumpToNewDiscussionPageCommand = new RelayCommand<Frame>(
                frame => frame.Navigate(typeof(NewDiscussionPage), new NewDiscussionVM(Forum)),
                () => UserDataManager.GetActiveUser() != null
                );
        }

        public async Task<bool> CheckForPermission(int readPermission)
        {
            User user = UserDataManager.GetActiveUser();
            if (readPermission <= 0 || user?.UserGroup.ReadPermissionLevel >= readPermission)
            {
                return true;
            }
            else
            {
                await new MessageDialog($"抱歉，本帖要求阅读权限高于{readPermission}才能浏览").ShowAsync();
                return false;
            }
        }

        public RelayCommand RefreshCommand { get; set; }

        public IncrementalList<Discussion, Forum> DiscussionList
        {
            get { return _discussionList; }
            set { SetProperty(ref _discussionList, value); }
        }

        private void Refresh()
        {
            Forum.Refresh();
            JumpToNewDiscussionPageCommand.RaiseCanExecuteChanged();
            DiscussionList = new IncrementalList<Discussion, Forum>(Forum);
        }
    }
}