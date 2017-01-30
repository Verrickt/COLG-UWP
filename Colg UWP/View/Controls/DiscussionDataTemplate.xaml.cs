using Windows.UI.Xaml.Controls;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace Colg_UWP.View.Controls
{
    public sealed partial class DiscussionDataTemplate : UserControl
    {
        public Model.Discussion Post
        {
            get { return this.DataContext as Model.Discussion; }
        }
        public DiscussionDataTemplate()
        {
            this.InitializeComponent();
            this.DataContextChanged += (s, e) => this.Bindings.Update();
        }
    }
}
