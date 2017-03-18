using Colg_UWP.ViewModel;
using System;
using UniversalMarkdown;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace Colg_UWP.View.Controls
{
    public sealed partial class PostControl : UserControl
    {
        public PostControl()
        {
            this.InitializeComponent();
            this.DataContextChanged += (s, e) => this.Bindings.Update();
        }

        public ViewModel.DiscussionVM VM { get { return this.DataContext as DiscussionVM; } }

        private void Replys_RightTapped(object sender, RightTappedRoutedEventArgs e)
        {
            ListView listview = sender as ListView;
        }

        private void GoToTop_Click(object sender, RoutedEventArgs e)
        {
            ReplyList?.ScrollIntoView(ReplyList?.Items[0]);
        }

        private void MarkdownTextBlock_OnOnMarkdownLinkTapped(object sender, OnMarkdownLinkTappedArgs e)
        {
            Windows.System.Launcher.LaunchUriAsync(new Uri(e.Link));
        }
    }
}