using Colg_UWP.IncrementalLoading;
using System.Threading.Tasks;

namespace Colg_UWP.ViewModel
{
    using Colg_UWP.Util;
    using Model;
    using Service;
    using System;
    using System.Collections.ObjectModel;

    public class HomeVM : VMBase
    {
        public ObservableCollection<ArticleContainerVM> PivotVMs
        {
            get { return _vms; }
            set { SetProperty(ref _vms, value); }
        }

        private bool _initialized = false;

        private ObservableCollection<ArticleContainerVM> _vms;

        public HomeVM()
        {
            PivotVMs = new ObservableCollection<ArticleContainerVM>();
            RefreshCommand = new RelayCommand(async()=>await RefreshAsync());
        }

        private bool _isLoading;

        public bool IsLoading
        {
            get { return _isLoading; }
            set { SetProperty(ref _isLoading, value); }
        }

        public RelayCommand RefreshCommand{ get; set; }

        private async Task InitAsync()
        {
            if (!_initialized)
            {
                var _newsList = await ArticleService.GetArticleContainerAsync("1");
                var _prospectiveList = await ArticleService.GetArticleContainerAsync("16");
                var _hotNewsList = await ArticleService.GetArticleContainerAsync("17");

                _vms.Add(new ArticleContainerVM(_newsList, "首页"));
                _vms.Add(new ArticleContainerVM(_hotNewsList, "资讯"));
                _vms.Add(new ArticleContainerVM(_prospectiveList, "前瞻"));

                _initialized = true;
            }
        }

        public async Task RefreshAsync()
        {
            try
            {
                IsLoading = true;
                await InitAsync();
                foreach (var PivotVM in PivotVMs)
                {
                    PivotVM.Refresh();
                }
            }
            catch(Exception e)
            {
                Logging.WriteLine($"Exception at HomeVM.RefreshAsync{e}");
                InAppNotifier.Show("网络不给力啊,刷新试试看?");
            }
            finally
            {
                IsLoading = false;
            }
        }
    }

    /// <summary>
    /// Wrapper class which is able to be DataBounded to Pivot
    /// </summary>

    public class ArticleContainerVM : VMBase
    {
        private IncrementalList<Article, ArticleContainer> _list;

        public IncrementalList<Article, ArticleContainer> List
        {
            get { return _list; }
            set { SetProperty(ref _list, value); }
        }

        private string _header;

        public string Header
        {
            get { return _header; }
            set { _header = value; }
        }

        public void Refresh()
        {
            List = new IncrementalList<Article, ArticleContainer>(_container);
        }

        private ArticleContainer _container;

        public ArticleContainerVM(ArticleContainer container, string header)
        {
            _container = container;
            Header = header;
            List = new IncrementalList<Article, ArticleContainer>(_container);
        }
    }
}