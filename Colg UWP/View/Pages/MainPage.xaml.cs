using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using Colg_UWP.Service;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Colg_UWP.View.Pages
{
    using ViewModel;
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {


        public MainPage()
        {
            this.InitializeComponent();
        }

        private void Humburger_Click(object sender, RoutedEventArgs e)
        {
            MySplitView.IsPaneOpen = !MySplitView.IsPaneOpen;
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            EnableBackRequest();
            MyFrame.Navigate(typeof(HomePage));
            Title.Text = VM.TopMenuItems[0].DisplayName;
            await ApiService.InitAsync();
            await ApiService.AutoLogin().ConfigureAwait(false);

        }

        public void EnableBackRequest()
        {
            MyFrame.Navigated += MyFrame_Navigated;
            SystemNavigationManager.GetForCurrentView().BackRequested += MainPage_BackRequested;
        }

        public void DisableBackRequest()
        {
            SystemNavigationManager.GetForCurrentView().BackRequested -= MainPage_BackRequested;
            MyFrame.Navigated -= MyFrame_Navigated;
        }

        private void MyFrame_Navigated(object sender, NavigationEventArgs e)
        {
            SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = ((Frame)sender).CanGoBack ? AppViewBackButtonVisibility.Visible : AppViewBackButtonVisibility.Collapsed;
        }

        private void MainPage_BackRequested(object sender, BackRequestedEventArgs e)
        {
            Frame rootFrame = MyFrame;
            if (rootFrame != null && rootFrame.CanGoBack)
            {
                e.Handled = true;
                rootFrame.GoBack();
            }
        }

        private void Page_Unloaded(object sender, RoutedEventArgs e)
        {
            DisableBackRequest();
        }

        private void Menu_ItemClick(object sender, ItemClickEventArgs e)
        {
            var clicked = e.ClickedItem as MenuVM;
            if (MySplitView.DisplayMode!=SplitViewDisplayMode.Inline)
            {
                MySplitView.IsPaneOpen = false;
            }
            if (clicked.TargetPageType==null)
            {
                new ContentDialog()
                {
                    Content = "施工中(=ﾟωﾟ)=",
                    PrimaryButtonText = "Got it"
                }.ShowAsync();
            }
            else
            {
                Title.Text = clicked.DisplayName;
                MyFrame.Navigate(clicked.TargetPageType);
            }
          
        }
    }
}
