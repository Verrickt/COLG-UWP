using System.Threading.Tasks;

namespace Colg_UWP.ViewModel
{
    using Model;

    public class ArticleVM : VMBase
    {
        private bool _isLoading;

        public bool IsLoading
        {
            get { return _isLoading; }
            set { SetProperty(ref _isLoading, value); }
        }

        public string ArticleId { get; set; }
        private Article _article;
        public Article Article { get { return _article; } set { SetProperty(ref _article, value); } }
        private string _articleInfo = string.Empty;
        private string _uri;
        public string ArticleInfo { get { return _articleInfo; } set { SetProperty(ref _articleInfo, value); } }

        public async Task InitAsync()
        {
            IsLoading = true;
            await Article.LoadContentAsync();
            IsLoading = false;
        }
    }
}