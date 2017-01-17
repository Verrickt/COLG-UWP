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

// The Content Dialog item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Colg_UWP.View.Controls
{
    public sealed partial class InAppNotification : ContentDialog
    {
        public InAppNotification()
        {
            this.InitializeComponent();
        }

        public static readonly DependencyProperty TitleTextProperty = DependencyProperty.Register(
            "TitleText", typeof(string), typeof(InAppNotification), new PropertyMetadata(default(string)));

        public string TitleText
        {
            get { return (string) GetValue(TitleTextProperty); }
            set { SetValue(TitleTextProperty, value); }
        }

        public static readonly DependencyProperty BodyTextProperty = DependencyProperty.Register(
            "BodyText", typeof(string), typeof(InAppNotification), new PropertyMetadata(default(string)));

        public string BodyText
        {
            get { return (string) GetValue(BodyTextProperty); }
            set { SetValue(BodyTextProperty, value); }
        }
    }
}
