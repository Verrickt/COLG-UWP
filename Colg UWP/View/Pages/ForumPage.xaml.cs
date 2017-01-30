using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using Colg_UWP.Util;
using Colg_UWP.Model;
using Windows.UI.Core;
using System;
using Windows.Foundation;
using Windows.Storage;
using Colg_UWP.ViewModel;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Colg_UWP.View.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ForumPage : MenuPage
    {

        public ForumPage()
        {
            this.InitializeComponent();
            this.NavigationCacheMode = NavigationCacheMode.Required;
        }

        public ForumVM VM { get; set; }


       

        private void EnableLocalBackRequest()
        {
            SystemNavigationManager.GetForCurrentView().BackRequested += BackRequested;
        }

        private  void DisableLocalBackRequest()
        {
            SystemNavigationManager.GetForCurrentView().BackRequested -= BackRequested;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            var frame = (Frame) Window.Current.Content;
            var page = (MainPage) frame.Content;
            page.DisableGlobalBackRequest();
            VM = VM ?? new ForumVM();
            VM.Forum = e.Parameter as Forum;
            ContentFrame.Navigate(typeof(DisplayPage));
            EnableLocalBackRequest();
            base.OnNavigatedTo(e);
        }

        private void BackRequested(object sender, BackRequestedEventArgs e)
        {
           
            e.Handled = true;
        }

      

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            DisableLocalBackRequest();
            var frame = (Frame) Window.Current.Content;
            var page = (MainPage) frame.Content;
            page.EnableGlobalBackRequest();
            base.OnNavigatedFrom(e);
        }



        private void DiscussionList_OnItemClick(object sender, ItemClickEventArgs e)
        {
            var discussion = e.ClickedItem as Model.Discussion;

            if (VM.CheckForPermission(discussion.ReadPermission))
            {
               
                ContentFrame.Navigate(typeof(DiscussionView), discussion);
            }
        }


        //private void HideInnerFrame()
        //{
        //    this.InnerFrame.Visibility=Visibility.Collapsed;
        //}

        //private void HideDiscussionList()
        //{
        //    this.DiscussionListGrid.Visibility=Visibility.Collapsed;
        //}

        //private void ShowDiscussionList(bool stretch)
        //{
        //    DiscussionListGrid.Visibility=Visibility.Visible;
        //    if (stretch)
        //    {
        //        DiscussionListGrid.Width = RelativeLayoutGrid.Width;
        //    }
        //    else
        //    {
        //        DiscussionListGrid.Width = 450d;
        //    }
        //}

        //private void ShowInnerFrame()
        //{
        //    InnerFrame.Visibility = Visibility.Visible;
        //}

      

        private void ForumPage_OnSizeChanged(object sender, SizeChangedEventArgs e)
        {
        //    Size size = e.NewSize;
        //    if (size.Width > (double)Application.Current.Resources["NormalMinWidth"])
        //    {
        //        ShowInnerFrame();
        //        ShowDiscussionList(false);
        //    }
        //    else
        //    {
        //        if (!String.IsNullOrEmpty(InnerFrame.CurrentSourcePageType?.Name))
        //        {
        //            HideDiscussionList();
        //            ShowInnerFrame();
        //        }
        //        else
        //        {
        //            ShowDiscussionList(true);
        //            HideInnerFrame();
        //        }
        //    }
        }
    }
}
