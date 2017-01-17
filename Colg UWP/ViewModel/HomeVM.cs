using System.Threading.Tasks;
using Colg_UWP.IncrementalLoading;

namespace Colg_UWP.ViewModel
{
    using Model;
    using Service;

    public class HomeVM : VMBase
    {
        private IncrementalList<News,NewsContainer> _news;
        private IncrementalList<News, NewsContainer> _hotNews;
        private IncrementalList<News, NewsContainer> _prospective;

        public IncrementalList<News, NewsContainer> News
        {
            get { return _news; }
            set
            {
                _news = value;
                OnPropertyChanged();
            }
        }

        public IncrementalList<News, NewsContainer> HotNews
        {
            get { return _hotNews; }
            set
            {
                _hotNews = value;
                OnPropertyChanged();
            }
        }

        public IncrementalList<News, NewsContainer> Prospective
        {
            get { return _prospective; }
            set
            {
                _prospective = value;
                OnPropertyChanged();
            }
        }


        private NewsContainer _newsList;
        private NewsContainer _hotNewsList;
        private NewsContainer _prospectiveList;

       
        public async Task RefreshAsync()
        {
            _newsList =_newsList??await ApiService.NewsByTypeId("1");
            _prospectiveList = _prospectiveList??await ApiService.NewsByTypeId("16");
            _hotNewsList = _hotNewsList??await ApiService.NewsByTypeId("17");
            News = new IncrementalList<News, NewsContainer>(_newsList);
            Prospective = new IncrementalList<News, NewsContainer>(_prospectiveList);
            HotNews = new IncrementalList<News, NewsContainer>(_hotNewsList);
        }
    }
}
