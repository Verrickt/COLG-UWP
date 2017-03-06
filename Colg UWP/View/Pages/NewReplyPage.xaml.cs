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
using Colg_UWP.Model;
using Colg_UWP.ViewModel;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Colg_UWP.View.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class NewReplyPage : Page
    {



        public NewReplyPage()
        {
            this.InitializeComponent();
        }

        private ReplyVM VM;

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            VM = e.Parameter as ReplyVM;
            Bindings.Update();
            base.OnNavigatedTo(e);
        }


    }
}
