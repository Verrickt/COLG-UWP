using Colg_UWP.Model;
using Colg_UWP.ViewModel;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Colg_UWP.View.Pages
{
    /// <summary>
    ///     An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class PopularPostPage : MenuPage
    {
        public PopularPostDiscussion VM { get; set; }

        public PopularPostPage()
        {
            NavigationCacheMode = NavigationCacheMode.Required;
            InitializeComponent();
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            if (VM == null)
            {
                VM = new PopularPostDiscussion();
                await VM.RefreshAsync();
            }

            base.OnNavigatedTo(e);
        }

        private void DiscussionList_OnItemClick(object sender, ItemClickEventArgs e)
        {
            var discussion = e.ClickedItem as Discussion;
            ContentFrame.Navigate(typeof(DiscussionPage), discussion);
        }
    }
}