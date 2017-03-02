using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Colg_UWP.Model;
using Colg_UWP.Util;
using Colg_UWP.Service;
using Colg_UWP.ViewModel;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Colg_UWP.View.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class LoginPage : MenuPage
    {


        public LoginPage()
        {
            this.InitializeComponent();
        }


        private ViewModel.LoginVM VM;

        private async void LoginButton_OnClick(object sender, RoutedEventArgs e)
        {
            var succeed = await VM.CredentialVM.LoginAsync();

            if (succeed)
            {
                JumpToUserSpace();
            }

          
        }


        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.NavigationMode == NavigationMode.New)
            {
                VM = new LoginVM();
            }
            base.OnNavigatedTo(e);
        }


        private  void LoginPage_OnLoaded(object sender, RoutedEventArgs e)
        {
            if (LoginDataManager.GetLoginDataList().Any(i=>i.IsActive))
            {
                JumpToUserSpace();
            }
            else
            {
                this.Bindings.Update();
            }
        }

        private void JumpToUserSpace()
        {
            this.Frame.GoBack();
            this.Frame.Navigate(typeof(MySpace));
        }


        private async void SavedLoginDatas_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {



            bool succeed = await VM.QuickLoginAsync();

            if (succeed)
            {
                JumpToUserSpace();
            }

        }


      

        private void Delete_OnClick(object sender, RoutedEventArgs e)
        {
             var button = sender as Button;
             var credential = button.DataContext as Credential;
             VM.RemoveSavedLogin(credential);

        }
    }
}
