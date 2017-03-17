using Colg_UWP.Service;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Colg_UWP.Model
{
    public class Discussion : ModelBase, IIncrementalLoad<Reply>
    {
        public string Author { get; set; }
        public string AuthorId { get; set; }
        public string Subject { get; set; }
        public DateTime? TimePosted { get; set; }
        public DateTime? TimeLastPosted { get; set; }
        public string LastPoster { get; set; }
        public string Avatar { get; set; }
        public string Catagory { get; set; }

        public int MaxCount
        {
            get { return _reply + 1; }//in case a discussion has no reply 
            set { _reply = value; }
        }

        public int ReadPermission { get; set; }
        public Func<Task<(int,List<Reply>)>> LoadMore { get; set; }

        public int Reply
        {
            get { return _reply; }
            set { _reply = value; }
        }

        private int _reply;
        public void Refresh()
        {
            Page = 1;
        }


        public Discussion()
        {
            LoadMore = async () =>
            {
                var result = await ReplyService.GetReplysAsync(Id, Page).ConfigureAwait(false);
                Page++;
                return result;
            };
        }
    }
}
