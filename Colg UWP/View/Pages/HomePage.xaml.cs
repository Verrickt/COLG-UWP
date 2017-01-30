using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Colg_UWP.View.Pages
{
    using ViewModel;

    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class HomePage : MenuPage
    {
        public HomePage()
        {
            this.NavigationCacheMode = NavigationCacheMode.Required;
            this.InitializeComponent();
        }

        public HomeVM VM;


        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.NavigationMode==NavigationMode.New)
            {
                VM = new HomeVM();
                await VM.RefreshAsync();
            }
            base.OnNavigatedTo(e);
        }


        private void GridView_ItemClick(object sender, ItemClickEventArgs e)
        {
            var clicked = e.ClickedItem as Model.Article;
            ContentFrame.Navigate(typeof(ArticlePage), clicked);
        }

        private void GridView_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            var gridview = sender as GridView;
            var wrapgrid = (ItemsWrapGrid) gridview.ItemsPanelRoot;
            double margin = 10;
            double newWidth = e.NewSize.Width;
            double itemWidth = wrapgrid.ItemWidth;

            int n = (int) (newWidth/(400d + margin));
            if (n != 0)
            {
                wrapgrid.ItemWidth = newWidth/n - margin;
            }
        }


        private async void Refresh_Click(object sender, RoutedEventArgs e)
        {
            await VM.RefreshAsync();
        }
    }
}
