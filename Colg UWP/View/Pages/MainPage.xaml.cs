﻿using Windows.UI.Core;
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

        private  void Page_Loaded(object sender, RoutedEventArgs e)
        {
            EnableGlobalBackRequest();
            MyFrame.Navigate(typeof(HomePage));
            Title.Text = VM.TopMenuItems[0].DisplayName;
           

        }

        public void EnableGlobalBackRequest()
        {
            MyFrame.Navigated += MyFrame_Navigated;
            SystemNavigationManager.GetForCurrentView().BackRequested += MainPage_BackRequested;
        }

        public void DisableGlobalBackRequest()
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
                Title.Text = clicked.DisplayName;
                MyFrame.Navigate(clicked.TargetPage);
            }
          
        }
    }
}
