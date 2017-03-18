using Colg_UWP.ViewModel;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Colg_UWP.View.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ArticlePage : Page
    {
        public ArticleVM VM = new ArticleVM();

        public ArticlePage()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (VM == null)
            {
                VM = new ArticleVM();
            }

            var article = e.Parameter as Model.Article;
            VM.Article = article;
            base.OnNavigatedTo(e);
        }

        private async void Page_Loading(FrameworkElement sender, object args)
        {
            await VM.InitAsync();
            this.Bindings.Update();
        }
    }
}