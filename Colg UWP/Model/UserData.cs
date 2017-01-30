using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colg_UWP.Model
{
    public class UserData
    {
        public string UserName { get; set; }
        public string UserID { get; set; }
        public string HomeUrl { get; set; }
        public string AvatarUrl { get; set; }
        public string GroupTitle { get; set; }
        public int ReadPermission { get; set; }
        public List<string> Credits { get; set; }
        public DateTimeOffset? TimeRegisted { get; set; }

        public UserData()
        {
             Credits = new List<string>();
        }

        public override string ToString()
        {
            return
                $"UserName:{UserName} UserID:{UserID} AvatarUrl:{AvatarUrl} GroupTitle:{GroupTitle} ReadAccessLevel:{ReadPermission}";
        }
    }
}
