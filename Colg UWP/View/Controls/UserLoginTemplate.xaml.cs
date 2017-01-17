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

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace Colg_UWP.View.Controls
{
    public sealed partial class UserLoginTemplate : UserControl
    {
        private Model.LoginData UserLoginData => this.DataContext as Model.LoginData;

        public UserLoginTemplate()
        {
            this.InitializeComponent();
            DataContextChanged += (s, e) => this.Bindings.Update();
        }
    }
}
