using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colg_UWP.Model
{
    public class Credential
    {
        public string LoginName { get; set; }
        public string Password { get; set; }
        public string Avatar{ get; set; }
        public int QuestionId { get; set; }
        public string QuestionAnswer { get; set; }

        public bool IsActive { get; set; }//indicated whether this logindata is used now

        public override string ToString()
        {
            return $"LoginName {LoginName} Password {Password} QuestionID {QuestionId} Answer {QuestionAnswer}";
        }
    }
}
