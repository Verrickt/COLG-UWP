using Colg_UWP.Model;
using Colg_UWP.ViewModel;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

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
            VM = new ForumNaviVM();
            InitializeComponent();
        }

        private ForumNaviVM VM;

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            await VM.RefreshAsync();
            base.OnNavigatedTo(e);
        }


        private void ListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            var clicked = e.ClickedItem as Forum;
            Frame.Navigate(typeof(ForumPage), clicked);
        }

    }
}