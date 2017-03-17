using Colg_UWP.Service;
using System;
using System.Threading.Tasks;

namespace Colg_UWP.Model
{
    public class Article : ModelBase
    {
        public DateTime? Date { get; set; }
        public string Image { get; set; }
        public int Comments { get; set; }
        public string Remark { get; set; }
        public string Content { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }

        public async Task LoadContentAsync()
        {
            await ArticleService.LoadArticelContentAsync(this);
        }
    }
}