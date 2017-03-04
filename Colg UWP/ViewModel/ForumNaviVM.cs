using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Colg_UWP.Util;

namespace Colg_UWP.ViewModel
{
    using Model;
    using Service;
    using System.Collections.ObjectModel;

    public class ForumNaviVM : VMBase
    {
        private List<Forum> Forums { get;  set; }
        public Forum SelectedForum { get; set; }

        public Util.RelayCommand RefreshCommand { get; set; }

        public ObservableCollection<ForumContainer> ForumContainers { get; set; }


        public async Task RefreshAsync()
        {
            Forums = await ForumService.GetForumsAsync();
            ForumContainers.Clear();
            var containers = Forums.GroupBy(x => x.Catagory, (catagory, grouped) => new ForumContainer
            {
                Catagory = catagory,
                Forums
             = new List<Forum>(grouped.OrderBy(f=>f.Name[0]))
            });
            containers.ToList().ForEach(c=>ForumContainers.Add(c));
        }

        public ForumNaviVM()
        {
            Forums = new List<Forum>();
            ForumContainers = new ObservableCollection<ForumContainer>();
            RefreshCommand = new RelayCommand(
                async () => await RefreshAsync());

        }

    }
}
