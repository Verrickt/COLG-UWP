using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Colg_UWP.ViewModel
{
    using Model;
    using Service;
    using System.Collections.ObjectModel;

    public class ForumNaviVM : VMBase
    {
        private ObservableCollection<ForumContainer> _forumContainers;
        private List<Forum> Forums { get;  set; }

       

        public Forum SelectedForum { get; set; }

        public ObservableCollection<ForumContainer> ForumContainers
        {
            get { return _forumContainers; }
            set { _forumContainers = value;OnPropertyChanged(); }
        }


        public async Task InitAsync()
        {
            Forums = await ApiService.ForumListAsync();
            var containers = Forums.GroupBy(x => x.Catagory, (catagory, grouped) => new ForumContainer
            {
                Catagory = catagory,
                Forums
             = new List<Forum>(grouped)
            }).OrderBy(x=>x.Catagory[0]);
           ForumContainers = new ObservableCollection<ForumContainer>(containers);

        }

        public ForumNaviVM()
        {
            Forums = new List<Forum>();
            ForumContainers = new ObservableCollection<ForumContainer>();
        }
    }
}
