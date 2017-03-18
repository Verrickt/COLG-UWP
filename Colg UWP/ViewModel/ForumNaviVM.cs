using Colg_UWP.Util;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Colg_UWP.ViewModel
{
    using Model;
    using Service;
    using System;
    using System.Collections.ObjectModel;

    public class ForumNaviVM : VMBase
    {
        private List<Forum> Forums { get; set; }
        public Forum SelectedForum { get; set; }

        public Util.RelayCommand RefreshCommand { get; set; }

        private bool _isLoading;

        public bool IsLoading
        {
            get { return _isLoading; }
            set { SetProperty(ref _isLoading, value); }
        }


        public ObservableCollection<ForumContainer> ForumContainers { get; set; }

        public async Task RefreshAsync()
        {
            try
            {
                IsLoading = true;
                Forums = await ForumService.GetForumsAsync();
                ForumContainers.Clear();
                var containers = Forums.GroupBy(x => x.Catagory, (catagory, grouped) => new ForumContainer
                {
                    Catagory = catagory,
                    Forums
                 = new List<Forum>(grouped.OrderBy(f => f.Name[0]))
                });
                containers.ToList().ForEach(c => ForumContainers.Add(c));
            }
            catch (Exception e)
            {
                Logging.WriteLine($"Exception at ForumNaviVM.RefreshAsync{e}");
                InAppNotifier.Show("网络不给力啊,要不刷新试试?");
            }
            finally
            {
                IsLoading = false;
            }
           
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