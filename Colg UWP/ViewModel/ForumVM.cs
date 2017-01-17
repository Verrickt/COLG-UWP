using Colg_UWP.Model;

namespace Colg_UWP.ViewModel
{
    using IncrementalLoading;
    public class ForumVM : VMBase
    {
        public string ForumId { get; set; }
        private Forum _forum;
        public Forum Forum { get { return _forum; } set { _forum = value; OnPropertyChanged(); } }
        private IncrementalList<Discussion,Forum> _postList;

        public IncrementalList<Discussion, Forum> PostList
        {
            get { return _postList; }
            set { _postList = value; OnPropertyChanged(); }
        }

        public void Refresh()
        {
            Forum.Refresh();
            PostList = new IncrementalList<Discussion, Forum>(Forum);
        }


    }
}
