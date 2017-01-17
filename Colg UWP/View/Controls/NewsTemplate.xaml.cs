using Colg_UWP.Model;
using Windows.UI.Xaml.Controls;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace Colg_UWP.View.Controls
{
    public sealed partial class NewsTemplate : UserControl
    {
        public News Header { get { return this.DataContext as News; } }

        public NewsTemplate()
        {
            this.InitializeComponent();
            this.DataContextChanged += (s, e) => Bindings.Update();
        }
    }
}
