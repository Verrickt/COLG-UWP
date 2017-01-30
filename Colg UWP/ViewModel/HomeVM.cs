using System.Collections.Generic;
using System.Threading.Tasks;
using Colg_UWP.IncrementalLoading;

namespace Colg_UWP.ViewModel
{
    using Model;
    using Service;

    public class HomeVM : VMBase
    {
        private IncrementalList<Article,ArticleContainer> _news;
        private IncrementalList<Article, ArticleContainer> _hotNews;
        private IncrementalList<Article, ArticleContainer> _prospective;

        public IncrementalList<Article, ArticleContainer> News
        {
            get { return _news; }
            set
            {
                SetProperty(ref _news, value);
            }
        }

        public IncrementalList<Article, ArticleContainer> HotNews
        {
            get { return _hotNews; }
            set
            {
                SetProperty(ref _hotNews, value);
            }
        }

        public IncrementalList<Article, ArticleContainer> Prospective
        {
            get { return _prospective; }
            set
            {
                SetProperty(ref _prospective, value);
            }
        }

        private ArticleContainer _newsList;
        private ArticleContainer _hotNewsList;
        private ArticleContainer _prospectiveList;

        private bool _initialized = false;
        private List<ArticleContainer> _containers = new List<ArticleContainer>(3);


        private async Task InitAsync()
        {
            if (!_initialized)
            {
                _newsList = _newsList ?? await ArticleService.GetArticleContainerAsync("1");
                _prospectiveList = _prospectiveList ?? await ArticleService.GetArticleContainerAsync("16");
                _hotNewsList = _hotNewsList ?? await ArticleService.GetArticleContainerAsync("17");
                _containers.Add(_newsList);
                _containers.Add(_prospectiveList);
                _containers.Add(_hotNewsList);

                _initialized = true;
            }
        }


        public async Task RefreshAsync()
        {
            await InitAsync();

            _containers.ForEach(c=>c.Refresh());
           
            News = new IncrementalList<Article, ArticleContainer>(_newsList);
            Prospective = new IncrementalList<Article, ArticleContainer>(_prospectiveList);
            HotNews = new IncrementalList<Article, ArticleContainer>(_hotNewsList);
        }
    }
}
