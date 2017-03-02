using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colg_UWP.Model
{
    public class User
    {
        public string UserName { get; set; }
        public string ID { get; set; }
        public string HomeUrl { get; set; }
        public string Avatar { get; set; }
        public string GroupTitle { get; set; }
        public int ReadPermission { get; set; }
        public List<string> Credits { get; set; }
        public DateTimeOffset? TimeRegisted { get; set; }

        public User()
        {
             Credits = new List<string>();
        }

        public override string ToString()
        {
            return
                $"UserName:{UserName} ID:{ID} Avatar:{Avatar} GroupTitle:{GroupTitle} ReadAccessLevel:{ReadPermission}";
        }
    }
}
