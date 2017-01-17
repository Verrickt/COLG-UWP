using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Colg_UWP.Service;

namespace Colg_UWP.Model
{
    public class NewsContainer:ModelBase,IIncrementalLoadable<News>
    {
        public int MaxCount { get; set; }

        public Func<Task<List<News>>> LoadMore
        {
            get;set;
        }

        public void Refresh()
        {
            Page = 1;
        }

        public NewsContainer()
        {
            LoadMore = () => ApiService.NewsList(Id, Page++);
        }
    }
}
