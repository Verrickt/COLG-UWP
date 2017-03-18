using Colg_UWP.Model;
using Colg_UWP.Service;
using Colg_UWP.Util;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Windows.UI.Popups;

namespace Colg_UWP.ViewModel
{
    public class PopularDiscussionVM : VMBase
    {
        public RelayCommand RefreshCommand { get; set; }

        public ObservableCollection<Discussion> PopularPosts;

        public PopularDiscussionVM()
        {
            PopularPosts = new ObservableCollection<Discussion>();
            RefreshCommand = new RelayCommand(async () => await RefreshAsync());
        }

        private bool _isLoading;

        public bool IsLoading
        {
            get { return _isLoading; }
            set { SetProperty(ref _isLoading, value); }
        }


        public async Task RefreshAsync()
        {
            try
            {
                IsLoading = true;
                List<Discussion> list = await DiscussionService.GetPopularDiscussionsAsync();
                PopularPosts.Clear();
                list.ForEach(post => PopularPosts?.Add(post));
            }
            catch(Exception e)
            {
                await new MessageDialog("网络不给力啊,刷新试试?").ShowAsync();
            }
            finally
            {
                IsLoading = false;
            }
            
        }
    }
}