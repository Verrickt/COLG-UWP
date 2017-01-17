using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colg_UWP.Model
{
    public class LoginData
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string AvatarUrl{ get; set; }
        public string FirstChar => UserName?.FirstOrDefault().ToString();
        public int QuestionId { get; set; }
        public string QuestionAnswer { get; set; }
    }
}
