using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Colg_UWP.Helper;
using Colg_UWP.Model;
using Colg_UWP.Service;

namespace Colg_UWP.ViewModel
{
    public class LoginPageVM : VMBase
    {
        private int _currentQuestionId;
        private bool _showAnswerTextBox;
        private string _currentQuestion=string.Empty;
        private ObservableCollection<LoginData> _savedLogins;
        private LoginData _quickLoginData;

        public ObservableCollection<LoginData> SavedLogins
        {
            get { return _savedLogins; }
            set { _savedLogins = value;}
        }

        public LoginData QuickLoginData
        {
            get { return _quickLoginData; }
            set { _quickLoginData = value; }
        }

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

        public int CurrentQuestionId
        {
            get { return _currentQuestionId; }
            set
            {
                _currentQuestionId = value;
                ShowAnswerTextBox = _currentQuestionId != 0;
                OnPropertyChanged();
            }
        }

        public bool ShowAnswerTextBox
        {
            get { return _showAnswerTextBox; }
            set
            {
                _showAnswerTextBox = value;
                OnPropertyChanged();
            }
        }

        public bool? AutoLogin { get; set; } = false;

        public string CurrentUserName { get; set; } = string.Empty;

        public string CurrentPassword { get; set; } = string.Empty;

        public string CurrentAnswer { get; set; } = string.Empty;

        public string CurrentQuestion
        {
            get { return _currentQuestion; }
            set
            {
                _currentQuestion = value;
                CurrentQuestionId = SecurityQuestions.FindIndex(x=>x==_currentQuestion);
            }
        }


        /// <summary>
        /// Retrive saved login data from local storage
        /// </summary>
        /// <returns></returns>
        public async Task InitAsync()
        {
            SavedLogins = new ObservableCollection<LoginData>(await LoginDataManager.GetLoginDatas());
        }

        public async Task<bool> QuickLogin()
        {
            var errorMessage =
                await ApiService.Login(QuickLoginData.UserName, QuickLoginData.Password, QuickLoginData.QuestionId, QuickLoginData.QuestionAnswer);
           
            return errorMessage == null;
        }


        public async Task<bool> Login()
        {
             var errorMessage =
                await ApiService.Login(CurrentUserName, CurrentPassword, CurrentQuestionId, CurrentAnswer);
            return errorMessage == null;
        }

    }
}
