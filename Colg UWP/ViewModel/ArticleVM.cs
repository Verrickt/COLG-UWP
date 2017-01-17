using System.Threading.Tasks;

namespace Colg_UWP.ViewModel
{
    using Model;

    public class ArticleVM : VMBase
    {




        public string ArticleId { get; set; }
        private News _news;
        public News News { get { return _news; } set { _news = value; OnPropertyChanged(); } }
        private string _articleInfo = string.Empty;
        private string _uri;
        public string ArticleInfo { get { return _articleInfo; } set { _articleInfo = value; OnPropertyChanged(); } }
        public async Task InitAsync()
        {
            await News.InitContent();
            OnPropertyChanged("News");
        }
    }
}
