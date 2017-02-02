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

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace Colg_UWP.View.Controls
{
    public sealed partial class ReplyDataTemplate : UserControl
    {

        public Reply Reply => this.DataContext as Reply;

        public ReplyDataTemplate()
        {
            this.InitializeComponent();
            this.DataContextChanged += (s, e) => this.Bindings.Update();

        }
    }
}
