using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Colg_UWP.Model;
using Colg_UWP.ViewModel;
using Colg_UWP.Helper;
using UniversalMarkdown;
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

        private async void Send_OnClick(object sender, RoutedEventArgs e)
        {
            var result = await VM.PostNewReply();
            string message = result ? "回复成功" : "回复失败";
            InAppNotifier.Show(result, "回复成功", "回复失败", null, "请先登录");
        }

        private void Cancel_OnClick(object sender, RoutedEventArgs e)
        {
            VM.CancelPostNewReply();
        }

        private void ReplyButton_OnClick(object sender, RoutedEventArgs e)
        {
            VM.ShowCommentBox = true;
        }




    }
}
