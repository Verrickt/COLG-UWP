using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using Colg_UWP.Helper;
using Colg_UWP.Model;
using Windows.UI.Core;
using System;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Colg_UWP.View.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ForumView : Page
    {
        public ForumView()
        {
            this.InitializeComponent();
            this.NavigationCacheMode = NavigationCacheMode.Required;
        }

      
       

        private void Refresh_Click(object sender, RoutedEventArgs e)
        {
            VM.Refresh();
        }

        public void EnableBackRequest()
        {
            SystemNavigationManager.GetForCurrentView().BackRequested += BackRequested;

        }
        public void DisableBackRequest()
        {
            SystemNavigationManager.GetForCurrentView().BackRequested -= BackRequested;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            var frame = (Frame)Window.Current.Content;
            var page = (MainPage)frame.Content;
            page.DisableBackRequest();
            
            if (e.NavigationMode == NavigationMode.New)
            {
                VM.Forum = e.Parameter as Forum;
                VM.Refresh();
            }
            EnableBackRequest();
            base.OnNavigatedTo(e);

        }

        private void BackRequested(object sender, BackRequestedEventArgs e)
        {
           
            if (InnerFrame.CanGoBack)
            {

                InnerFrame.GoBack();
                e.Handled = true;

            }
            else
            {
                if (!this.MySplitView.IsPaneOpen)
                {
                    this.MySplitView.IsPaneOpen = true;
                }
                else
                {
                    this.Frame.GoBack();
                }
            }
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            DisableBackRequest();
            var frame = (Frame)Window.Current.Content;
            var page = (MainPage)frame.Content;
            page.EnableBackRequest();
            base.OnNavigatedFrom(e);
        }


        private void GoToTop_Click(object sender, RoutedEventArgs e)
        {
            PostList.ScrollIntoView(PostList.Items[0]);
        }

        private async void PostList_OnItemClick(object sender, ItemClickEventArgs e)
        {
            var post = e.ClickedItem as Model.Discussion;
            var userdata = await UserDataManager.GetUserData();
            if (post.ReadPermission<=0||userdata?.ReadPermission>=post.ReadPermission)
            {
                if (MySplitView.OpenPaneLength>320)
                {
                    MySplitView.IsPaneOpen = false;
                }
                InnerFrame.Navigate(typeof(PostView), post);
            }
            else
            {
               InAppNotifier.Show("Oops", $"抱歉，本帖要求阅读权限高于{post.ReadPermission}才能浏览");
            }
        }

        private void PostList_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            //var gridview = sender as GridView;
            //var wrapgrid = (ItemsWrapGrid)gridview.ItemsPanelRoot;
            //int margin = 10;
            //int n = (int)(e.NewSize.Width / (400+margin));
            //if (n!=0)
            //{
            //    wrapgrid.ItemWidth = e.NewSize.Width / n-margin; 
            //}

        }
    }
}
