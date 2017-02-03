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
        public Frame ContentFrame => this.Main_ContentFrame;

        public CommandBar ContentCommandBar => this.Main_ContentCommandBar;

        public CommandBar MenuCommandBar => this.Main_MenuCommandBar;

        public TextBlock MenuTitleTextBlock => Main_MenuTitleTextBlock;

        public TextBlock ContentTitleTextBlock => this.Main_ContentTitleTextBlock;


        public MainPage()
        {
            this.InitializeComponent();
        }

        private void Humburger_Click(object sender, RoutedEventArgs e)
        {
            MySplitView.IsPaneOpen = !MySplitView.IsPaneOpen;
        }

        private  void Page_Loaded(object sender, RoutedEventArgs e)
        {
            EnableGlobalBackRequest();
            Main_MenuFrame.Navigate(typeof(HomePage));
            ContentFrame.Navigate(typeof(DisplayPage));
        }

        private void EnableGlobalBackRequest()
        {
            Main_MenuFrame.Navigated += MainMenuFrameNavigated;
            SystemNavigationManager.GetForCurrentView().BackRequested += MainPage_BackRequested;
        }

        private void DisableGlobalBackRequest()
        {
            SystemNavigationManager.GetForCurrentView().BackRequested -= MainPage_BackRequested;
            Main_MenuFrame.Navigated -= MainMenuFrameNavigated;
        }

        private void MainMenuFrameNavigated(object sender, NavigationEventArgs e)
        {
            UpdateBackButtonVisibility();
        }

        private void Main_ContentFrame_OnNavigated(object sender, NavigationEventArgs e)
        {
            UpdateBackButtonVisibility();
        }

        private void UpdateBackButtonVisibility()
        {
            SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility =
                _canGoBack ? AppViewBackButtonVisibility.Visible : AppViewBackButtonVisibility.Collapsed;
        }

        private void MainPage_BackRequested(object sender, BackRequestedEventArgs e)
        {
           
            if (ContentFrame.CanGoBack)
            {
                ContentFrame.GoBack();
            }
            else
            {
                if (Main_MenuFrame.CanGoBack)
                {
                    Main_MenuFrame.GoBack();
                }
            }

            e.Handled = true;
        }

        private void Page_Unloaded(object sender, RoutedEventArgs e)
        {
            DisableGlobalBackRequest();
        }

        private void Menu_ItemClick(object sender, ItemClickEventArgs e)
        {
            var clicked = e.ClickedItem as MenuVM;
            bool 
                keepPaneOpen= MySplitView.DisplayMode==SplitViewDisplayMode.CompactInline;
            MySplitView.IsPaneOpen = keepPaneOpen;
            if (clicked.TargetPage==null)
            {
#pragma warning disable CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
                new ContentDialog()
                {
                    Content = "施工中(=ﾟωﾟ)=",
                    PrimaryButtonText = "Got it"
                }.ShowAsync();
#pragma warning restore CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
            }
            else
            {
                Main_MenuFrame.Navigate(clicked.TargetPage);
            }
          
        }

      

        private bool _canGoBack => Main_ContentFrame.CanGoBack || Main_MenuFrame.CanGoBack;
    }
}
