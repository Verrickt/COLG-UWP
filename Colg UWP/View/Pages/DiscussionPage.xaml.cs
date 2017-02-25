using Colg_UWP.ViewModel;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Navigation;
using Colg_UWP.Annotations;
using Colg_UWP.Util;
using Colg_UWP.Model;
using UniversalMarkdown;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Colg_UWP.View.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class DiscussionPage : Page
    {
        private DiscussionVM _vm;


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
            var post = e.Parameter as Discussion;
            VM.Discussion = post;
            base.OnNavigatedTo(e);
        }

        private void Replys_RightTapped(object sender, RightTappedRoutedEventArgs e)
        {
            ListView listview = sender as ListView;
            ReplyFlyout.ShowAt(listview, e.GetPosition(listview));
        }

        private void GoToTop_Click(object sender, RoutedEventArgs e)
        {
            ReplyList?.ScrollIntoView(ReplyList?.Items[0]);
        }

        private void MarkdownTextBlock_OnOnMarkdownLinkTapped(object sender, OnMarkdownLinkTappedArgs e)
        {
            Windows.System.Launcher.LaunchUriAsync(new Uri(e.Link));
        }


        private void ReplyButton_OnClick(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(NewReplyPage), VM);
        }
    }
}
