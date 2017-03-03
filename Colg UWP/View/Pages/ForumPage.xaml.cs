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

        

        
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
           
            VM = VM ?? new ForumVM();
            VM.Forum = e.Parameter as Forum;
            base.OnNavigatedTo(e);
        }




        private async void DiscussionList_OnItemClick(object sender, ItemClickEventArgs e)
        {
            var discussion = e.ClickedItem as Model.Discussion;

            if (await VM.CheckForPermission(discussion.ReadPermission))
            {
                ContentFrame.Navigate(typeof(DiscussionPage), discussion);
            }
        }



    }
}
