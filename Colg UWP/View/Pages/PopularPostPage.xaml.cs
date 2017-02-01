using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using Colg_UWP.ViewModel;
using System;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Colg_UWP.View.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class PopularPostPage : MenuPage
    {
        public PopularPostDiscussion VM { get; set; }

        public override CommandBar LocalCommandBar => MyCommandBar;

        public PopularPostPage()
        {
            NavigationCacheMode = NavigationCacheMode.Required;
            InitializeComponent();
        }

        private void PostList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ContentFrame.Navigate(typeof(DiscussionPage),VM.SelectedPost);
        }

        private async void PopularPostView_OnLoading(FrameworkElement sender, object args)
        {
            if (VM == null)
            {
                VM = new PopularPostDiscussion();
                await VM.RefreshAsync();
            }
        }

        private async void Refresh_OnClick(object sender, RoutedEventArgs e)
        {
            await VM.RefreshAsync();
        }

        private void Top_OnClick(object sender, RoutedEventArgs e)
        {
            PostList.ScrollIntoView(PostList.Items[0]);
        }

        private void PostList_OnSizeChanged(object sender, SizeChangedEventArgs e)
        {
            //var gridview = sender as GridView;
            //var wrapgrid = (ItemsWrapGrid)gridview.ItemsPanelRoot;
            //int margin = 10;
            //int n = (int)(e.NewSize.Width / (400 + margin));
            //if (n != 0)
            //{
            //    wrapgrid.ItemWidth = e.NewSize.Width / n - margin;
            //}
        }
    }
}
