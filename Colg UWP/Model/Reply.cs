using System;

namespace Colg_UWP.Model
{
    public class Reply:ModelBase
    {
        public string Author { get; set; }
        public string AuthorId { get; set; }
        public DateTime? TimeReplied { get; set; }
        public string Message { get; set; }
        public string Markdown { get; set; }
        public string Avatar { get; set; }
        public string Position { get; set; }
    }
}
