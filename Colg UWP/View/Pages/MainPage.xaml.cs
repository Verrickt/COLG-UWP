using System;
using System.Collections.Generic;
using System.Linq;
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
        public Frame Main_ContentFrame => ContentFrame;


        private Stack<bool> _navigationStack = new Stack<bool>();
        //keep track of to which frame all the page navigation users triggerd belongs to

        public MainPage()
        {
            InitializeComponent();
        }

        private void Humburger_Click(object sender, RoutedEventArgs e)
        {
            MySplitView.IsPaneOpen = !MySplitView.IsPaneOpen;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            MenuFrame.Navigate(typeof(HomePage));
            ContentFrame.Navigate(typeof(PlaceHolderPage));
            MenuFrame.Navigated += MenuFrame_OnNavigated;
            ContentFrame.Navigated += ContentFrame_OnNavigated;
            SystemNavigationManager.GetForCurrentView().BackRequested += MainPage_BackRequested;
            UpdateUserInfo();

        }

        private void UpdateUserInfo()
        {
            var activeUser = Util.UserDataManager.GetActiveUser();
            string username = activeUser?.UserName;
            string avatar = activeUser?.Avatar;
            UserNameTextBlock.Text = username ?? "登录";
            var str = AvatarImageEx.Source?.ToString();
            if (str != avatar)
            {
                AvatarImageEx.Source = avatar;
            }
        }

        private void MenuFrame_OnNavigated(object sender, NavigationEventArgs e)
        {
            if (e.NavigationMode == NavigationMode.New || e.NavigationMode == NavigationMode.Forward)
                _navigationStack.Push(true);
            if (MenuFrame.Visibility == Visibility.Collapsed)
            {
                MenuFrame.Visibility = Visibility.Visible;
                ContentFrame.Visibility = Visibility.Collapsed;
            }
            UpdateBackButtonVisibility();
            UpdateIsPaneOpen();
            UpdateUserInfo();
        }

        private void ContentFrame_OnNavigated(object sender, NavigationEventArgs e)
        {
            if (e.NavigationMode == NavigationMode.New || e.NavigationMode == NavigationMode.Forward)
                _navigationStack.Push(false);
            if (ContentFrame.Visibility == Visibility.Collapsed)
            {
                MenuFrame.Visibility = Visibility.Collapsed;
                ContentFrame.Visibility = Visibility.Visible;
            }
            if (e.NavigationMode == NavigationMode.Back)
                if (ContentFrame.BackStackDepth == 0)
                {
                    if (MenuFrame.Visibility==Visibility.Collapsed)
                    {
                        MenuFrame.Visibility = Visibility.Visible;
                        ContentFrame.Visibility = Visibility.Collapsed;
                    }
                }

            UpdateIsPaneOpen();
            UpdateBackButtonVisibility();
        }

        private void UpdateBackButtonVisibility()
        {
            SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = _navigationStack.Count > 0
                ? AppViewBackButtonVisibility.Visible
                : AppViewBackButtonVisibility.Collapsed;
        }

        private void MainPage_BackRequested(object sender, BackRequestedEventArgs e)
        {



            bool isLastPageMenuPage = _navigationStack.Pop();

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
            SystemNavigationManager.GetForCurrentView().BackRequested -= MainPage_BackRequested;
            MenuFrame.Navigated -= MenuFrame_OnNavigated;
            ContentFrame.Navigated -= ContentFrame_OnNavigated;
        }

        private void MenuList_ItemClick(object sender, ItemClickEventArgs e)
        {
            MenuVM clicked = e.ClickedItem as MenuVM;
            if (clicked.TargetPage == null)
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

        private void UpdateIsPaneOpen()
        {
            bool
                keepPaneOpen = MySplitView.DisplayMode == SplitViewDisplayMode.CompactInline;
            if (!keepPaneOpen)
            {
                MySplitView.IsPaneOpen = false;
            }
        }


        private void MainPage_OnSizeChanged(object sender, SizeChangedEventArgs e)
        {
            double width = e.NewSize.Width;
            double wideWidth = (double) Application.Current.Resources["WideMinWidth"];

            if (width >= wideWidth)
            {
                ContentFrame.Visibility = Visibility.Visible;
                MenuFrame.Visibility = Visibility.Visible;
            }
            else
            {
                if (ContentFrame.CanGoBack)
                {
                    ContentFrame.Visibility = Visibility.Visible;
                    MenuFrame.Visibility = Visibility.Collapsed;
                }
                else
                {
                    ContentFrame.Visibility = Visibility.Collapsed;
                    MenuFrame.Visibility = Visibility.Visible;
                }
            }
        }
    }

}
