using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using Colg_UWP.Model;
using Colg_UWP.ViewModel;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Colg_UWP.View.Pages
{
    /// <summary>
    ///     An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ForumNavigationPage : MenuPage
    {

        public ForumNavigationPage()
        {
            NavigationCacheMode = NavigationCacheMode.Required;
            InitializeComponent();
        }

        private ForumNaviVM VM;



        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.NavigationMode == NavigationMode.New)
            {
              
            }
            base.OnNavigatedTo(e);
        }


        private void ListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            var clicked = e.ClickedItem as Forum;
            Frame.Navigate(typeof(ForumPage), clicked);
        }


        

    
        private async void ForumNavigationPage_OnLoading(FrameworkElement sender, object args)
        {
            VM = new ForumNaviVM();
            await VM.RefreshAsync();
        }
    }
}
