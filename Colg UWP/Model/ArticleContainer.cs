using Colg_UWP.Service;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Colg_UWP.Model
{
    public class ArticleContainer : ModelBase, IIncrementalLoad<Article>
    {
        public int MaxCount { get; set; }

        public Func<Task<(int, List<Article>)>> LoadMore
        {
            get; set;
        }

        public void Refresh()
        {
            Page = 1;
        }

        public ArticleContainer()
        {
            LoadMore = async () =>
             {
                 var result = await ArticleService.GetArticlesAsync(Id, Page);
                 Page++;
                 return result;
             };
        }
    }
}