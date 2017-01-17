using Colg_UWP.Service;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Colg_UWP.Model
{
    public class Discussion:ModelBase,IIncrementalLoadable<Reply>
    {
        public string Author { get; set; }
        public string AuthorId { get; set; }
        public string Subject { get; set; }
        public DateTime? TimePosted { get; set; }
        public DateTime? TimeLastPosted { get; set; }
        public string LastPoster { get; set; }
        public string Avatar { get; set; }
        public string Catagory { get; set; }
        public int MaxCount { get; set; }
        public int ReadPermission { get; set; }
        public Func<Task<List<Reply>>> LoadMore { get; set; }


        public void Refresh()
        {
            Page = 1;
        }


        public Discussion()
        {
            LoadMore = () => ApiService.ReplyList(Id, Page++);
        }

       

        
    }
}
