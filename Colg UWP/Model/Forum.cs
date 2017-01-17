using Colg_UWP.Service;
using System.Collections.Generic;
using System;
using System.Threading.Tasks;

namespace Colg_UWP.Model
{
    public class Forum:ModelBase,IIncrementalLoadable<Discussion>
    {
        public int MaxCount { get; set; }
        public Func<Task<List<Discussion>>> LoadMore { get; set; }
        public void Refresh()
        {
            Page = 1;
        }

        public string Name { get; set; }
        public string IconUri { get; set; }
        public string Catagory { get; set; }
        public string PostToday { get; set; }

       

        public Forum()
        {
            LoadMore = () => ApiService.PostListAsync(Id, Page++);
        }

        

        

       
    }

    public class ForumContainer
    {
        public List<Forum> Forums { get; set; }
        public string Catagory { get; set; }
    }
}
