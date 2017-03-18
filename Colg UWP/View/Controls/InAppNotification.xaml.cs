using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

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
            get { return (string)GetValue(TitleTextProperty); }
            set { SetValue(TitleTextProperty, value); }
        }

        public static readonly DependencyProperty BodyTextProperty = DependencyProperty.Register(
            "BodyText", typeof(string), typeof(InAppNotification), new PropertyMetadata(default(string)));

        public string BodyText
        {
            get { return (string)GetValue(BodyTextProperty); }
            set { SetValue(BodyTextProperty, value); }
        }
    }
}