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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Colg_UWP.View.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ContainerPage : Page
    {
        public ContainerPage()
        {
            this.InitializeComponent();
        }

        public new Frame Frame => this.MyFrame;

        public string TitleText
        {
            set { this.TitleTextBlock.Text = value; }
        }


        public IObservableVector<ICommandBarElement> AppBarButtons
        {
            set
            {
                this.CommandBar.PrimaryCommands.Clear();
                value.ToList().ForEach(item=>CommandBar.PrimaryCommands.Add(item));
            }
        }
    }
}
