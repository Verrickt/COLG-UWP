using Windows.UI.Text.Core;
using Colg_UWP.Model;
using Colg_UWP.Util;

namespace Colg_UWP.ViewModel
{
    using IncrementalLoading;
    public class ForumVM : VMBase
    {
        public string ForumId { get; set; }
        private Forum _forum;
        public Forum Forum { get { return _forum; } set { SetProperty(ref _forum, value);Refresh(); } }
        private IncrementalList<Discussion,Forum> _discussionList;

        private static User _user { get; set; }

        static ForumVM()
        {
            _user = UserDataManager.GetUserData();
        }

        public ForumVM()
        {
            RefreshCommand = new RelayCommand(Refresh, () => true);
        }

        public bool CheckForPermission(int readPermission)
        {
            if (readPermission <= 0 || _user.ReadPermission >= readPermission)
            {
                return true;
            }
            else
            {
                InAppNotifier.Show($"抱歉，本帖要求阅读权限高于{readPermission}才能浏览");
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
            DiscussionList = new IncrementalList<Discussion, Forum>(Forum);
        }


    }
}
