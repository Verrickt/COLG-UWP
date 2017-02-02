using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Colg_UWP.Model;
using Colg_UWP.Service;
using Colg_UWP.Util;

namespace Colg_UWP.ViewModel
{
    public class PopularPostDiscussion:VMBase
    {
        public RelayCommand RefreshCommand{ get; set; }

        public ObservableCollection<Discussion> PopularPosts;

        public PopularPostDiscussion()
        {
            PopularPosts = new ObservableCollection<Discussion>();
            RefreshCommand = new RelayCommand(async()=>await RefreshAsync(),()=>true);
        }

        public async Task RefreshAsync()
        {
            List<Discussion> list = await DiscussionService.GetPopularDiscussionsAsync();
            PopularPosts.Clear();
            list.ForEach(post => PopularPosts?.Add(post));
        }

       
    }
}
