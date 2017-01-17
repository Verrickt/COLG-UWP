using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Colg_UWP.Helper;
using Colg_UWP.Model;
using Colg_UWP.Service;
using Colg_UWP.ViewModel;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Colg_UWP.View.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MySpace : Page
    {
        public MySpace()
        {
            this.InitializeComponent();
        }

        public MySpaceVM VM;

        private async void LogOutButton_OnClick(object sender, RoutedEventArgs e)
        {
            var result = await ApiService.LogOut();
            if (result==null)
            {
                this.Frame.GoBack();
                this.Frame.Navigate(typeof(LoginPage));
            }
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.NavigationMode==NavigationMode.New)
            {
                VM = new MySpaceVM();
                VM.InitAsync();
            }
            base.OnNavigatedTo(e);
        }

    
    }
}
