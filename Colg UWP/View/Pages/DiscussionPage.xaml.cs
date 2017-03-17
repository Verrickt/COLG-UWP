using Colg_UWP.Model;
using Colg_UWP.ViewModel;
using System;
using UniversalMarkdown;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Colg_UWP.View.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class DiscussionPage : Page
    {
        private DiscussionVM _vm;

        private Reply reply;

        public DiscussionPage()
        {
            this.InitializeComponent();
            this.NavigationCacheMode = NavigationCacheMode.Required;
        }

        public DiscussionVM VM
        {
            get { return _vm; }
            set { _vm = value; }
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            VM = VM ?? new DiscussionVM();
            VM.Discussion = e.Parameter as Discussion;
            base.OnNavigatedTo(e);
        }

        private void GoToTop_Click(object sender, RoutedEventArgs e)
        {
            ReplyList?.ScrollIntoView(ReplyList?.Items[0]);
        }

        private void MarkdownTextBlock_OnOnMarkdownLinkTapped(object sender, OnMarkdownLinkTappedArgs e)
        {
            Windows.System.Launcher.LaunchUriAsync(new Uri(e.Link));
        }

        private void ReplyListItem_OnRightTapped(object sender, RightTappedRoutedEventArgs e)
        {
            FrameworkElement element = sender as FrameworkElement;
            reply = (Reply)element.DataContext;
            ReplyFlyout.ShowAt(element, e.GetPosition(element));
        }

        private void Reply_OnClick(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(NewReplyPage), new ReplyVM(VM.Discussion, reply));
        }
    }
}