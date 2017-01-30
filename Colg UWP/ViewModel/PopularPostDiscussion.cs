﻿using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Colg_UWP.Model;
using Colg_UWP.Service;

namespace Colg_UWP.ViewModel
{
    public class PopularPostDiscussion:VMBase
    {
        private ObservableCollection<Discussion> _popularPosts;

        public ObservableCollection<Discussion> PopularPosts
        {
            get { return _popularPosts; }
            set { SetProperty(ref _popularPosts, value); }
        }

        public Discussion SelectedPost { get; set; }

        public PopularPostDiscussion()
        {
            _popularPosts= new ObservableCollection<Discussion>();
        }

        public async Task RefreshAsync()
        {
            List<Discussion> list = await DiscussionService.GetPopularDiscussionsAsync();
            PopularPosts.Clear();
            list.ForEach(post => PopularPosts?.Add(post));
        }

    }
}
