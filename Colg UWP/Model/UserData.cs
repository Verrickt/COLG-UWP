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
        public Dictionary<string,string> Credits { get; set; }
        public string FormHash { get; set; }
        public override string ToString()
        {
            return
                $"UserName:{UserName} UserID:{UserID} AvatarUrl:{AvatarUrl} GroupTitle:{GroupTitle} ReadAccessLevel:{ReadPermission}";
        }
    }
}
