using System.Linq;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using Colg_UWP.Model;
using Colg_UWP.ViewModel;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Colg_UWP.View.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ForumNavigationPage : MenuPage
    {
        public ForumNavigationPage()
        {
            this.NavigationCacheMode = NavigationCacheMode.Required;
            this.InitializeComponent();
        }

        private ForumNaviVM VM;

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.NavigationMode==NavigationMode.New)
            {
                VM = new ForumNaviVM();
                await VM.InitAsync();
            }

            base.OnNavigatedTo(e);
        }


       
    

        private void ListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            var clicked = e.ClickedItem as Forum;
            this.Frame.Navigate(typeof(ForumPage), clicked);
        }


        private void ZoomInView_OnSizeChanged(object sender, SizeChangedEventArgs e)
        {
            //var gridview = sender as GridView;
            //var wrapgrid = gridview.ItemsPanelRoot as ItemsWrapGrid;
            //int margin = 10;
            //int n = (int) (e.NewSize.Width/(300 + margin));
            //if (n!=0)
            //{
            //    wrapgrid.ItemWidth = e.NewSize.Width/n - margin;
            //}
        }

        public override string Title { get; set; } = "板块列表";
    }
}
