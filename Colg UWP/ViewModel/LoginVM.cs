using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Colg_UWP.Util;
using Colg_UWP.Model;
using Colg_UWP.Service;

namespace Colg_UWP.ViewModel
{
    public class LoginVM : VMBase
    {
        private ObservableCollection<LoginData> _savedLogins;

        public ObservableCollection<LoginData> SavedLogins
        {
            get { return _savedLogins; }
            set { _savedLogins = value; }
        }

        public LoginDataVM CurrentLogin { get; set; } = new LoginDataVM();

       
        public List<string> SecurityQuestions => new List<string>()
        {
            "安全提问(未设置请忽略)",
            "母亲的名字",
            "父亲的名字",
            "爷爷的名字",
            "父亲出生的城市",
            "您其中一位老师的名字",
            "您个人计算机的型号",
            "您最喜欢的餐馆名称",
            "驾驶执照最后四位数字"
        };

        public LoginVM()
        {
            SavedLogins = new ObservableCollection<LoginData>( LoginDataManager.GetLoginDataList());

        }
    }

    public class LoginDataVM : VMBase
    {
        private LoginData _data = new LoginData();
        private string _answer;
        private string _username;
        private string _password;
        private int _questionId;

        public string Username
        {
            get { return _username; }
            set { SetProperty(ref _username,value); }
        }

        public string Password
        {
            get { return _password; }
            set { SetProperty(ref _password, value); }
        }

        public int QuestionID
        {
            get { return _questionId; }
            set { SetProperty(ref _questionId,value); }
        }

        public string Answer
        {
            get { return _answer; }
            set { SetProperty(ref _answer,value); }
        }

        public LoginDataVM()
        {
            QuestionID = -1;
            Answer = String.Empty;
        }

        public async Task<bool> LoginAsync()
        {
            _data.UserName = Username;
            _data.Password = Password;
            if (QuestionID<0)
            {
                QuestionID = 0;
            }
            _data.QuestionId = QuestionID;
            _data.QuestionAnswer = Answer;
            var (isSuccess,message)=await LoginService.LoginAsync(_data);
            InAppNotifier.Show(message);
            return isSuccess;
        }
    }
}
