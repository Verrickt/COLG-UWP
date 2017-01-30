using Colg_UWP.Model;
using Windows.UI.Xaml.Controls;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace Colg_UWP.View.Controls
{
    public sealed partial class ArticleTemplate : UserControl
    {
        public Article Header { get { return this.DataContext as Article; } }

        public ArticleTemplate()
        {
            this.InitializeComponent();
            this.DataContextChanged += (s, e) => Bindings.Update();
        }

        
    }
}
