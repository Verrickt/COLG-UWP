using Colg_UWP.Model;
using Windows.UI.Xaml.Controls;

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