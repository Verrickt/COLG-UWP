using System.Collections.Generic;
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
        public Frame Main_ContentFrame => this.ContentFrame;


        private Stack<bool> NavigationStack = new Stack<bool>();
        //keep track of to which frame all the page navigation users triggerd belongs to

        public MainPage()
        {
            this.InitializeComponent();
        }

        private void Humburger_Click(object sender, RoutedEventArgs e)
        {
            MySplitView.IsPaneOpen = !MySplitView.IsPaneOpen;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            MenuFrame.Navigate(typeof(HomePage));
            ContentFrame.Navigate(typeof(DisplayPage));
            EnableGlobalBackRequest();
        }

        private void EnableGlobalBackRequest()
        {
            MenuFrame.Navigated += MenuFrame_OnNavigated;
            ContentFrame.Navigated += ContentFrame_OnNavigated;
            SystemNavigationManager.GetForCurrentView().BackRequested += MainPage_BackRequested;
        }

        private void DisableGlobalBackRequest()
        {
            SystemNavigationManager.GetForCurrentView().BackRequested -= MainPage_BackRequested;
            MenuFrame.Navigated -= MenuFrame_OnNavigated;
            ContentFrame.Navigated -= ContentFrame_OnNavigated;
        }

        private void MenuFrame_OnNavigated(object sender, NavigationEventArgs e)
        {
            if (e.NavigationMode == NavigationMode.New || e.NavigationMode == NavigationMode.Forward)
            {
                NavigationStack.Push(true);
            }
            UpdateBackButtonVisibility();
        }

        private void ContentFrame_OnNavigated(object sender, NavigationEventArgs e)
        {
            if (e.NavigationMode==NavigationMode.New||e.NavigationMode==NavigationMode.Forward)
            {
                NavigationStack.Push(false);
            }
            UpdateBackButtonVisibility();
        }

        private void UpdateBackButtonVisibility()
        {
            SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility =
                NavigationStack.Count>0 ? AppViewBackButtonVisibility.Visible : AppViewBackButtonVisibility.Collapsed;
        }

        private void MainPage_BackRequested(object sender, BackRequestedEventArgs e)
        {

            var isLastPageMenuPage = NavigationStack.Pop();

            if (isLastPageMenuPage)
            {
                MenuFrame.GoBack();
            }
            else
            {
                ContentFrame.GoBack();
            }
            e.Handled = true;
        }

        private void Page_Unloaded(object sender, RoutedEventArgs e)
        {
            DisableGlobalBackRequest();
        }

        private void MenuList_ItemClick(object sender, ItemClickEventArgs e)
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
                MenuFrame.Navigate(clicked.TargetPage);
            }
          
        }

      

        

      
    }
}
