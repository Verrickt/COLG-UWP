using System;

namespace Colg_UWP.Model
{
    public class Credential
    {
        public string LoginName { get; set; } = String.Empty;
        public string Password { get; set; } = String.Empty;
        public int QuestionId { get; set; } = -1;
        public string QuestionAnswer { get; set; } = String.Empty;

        public override string ToString()
        {
            return $"LoginName {LoginName} QuestionID {QuestionId} Answer {QuestionAnswer}";
        }
    }
}