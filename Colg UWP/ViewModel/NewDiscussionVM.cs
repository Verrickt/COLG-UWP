using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Popups;
using Windows.UI.Xaml.Controls;
using AppStudio.Uwp.Commands;
using Colg_UWP.Model;
using Colg_UWP.Service;

namespace Colg_UWP.ViewModel
{
    public class NewDiscussionVM:VMBase
    {
        private Forum _forum;
        private string _subject;
        private string _message;
        private string _selectedDiscussionType;

        public RelayCommand<Frame> PostNewDiscussionCommand { get; set; }

        public string SelectedDiscussionType
        {
            get { return _selectedDiscussionType; }
            set { SetProperty(ref _selectedDiscussionType,value); }
        }

        public Forum Forum
        {
            get { return _forum; }
            set { SetProperty(ref _forum, value); }
        }

        public string Subject
        {
            get { return _subject; }
            set { SetProperty(ref _subject,value); }
        }

        public string Message
        {
            get { return _message; }
            set { SetProperty(ref _message,value); }
        }

        public NewDiscussionVM(Forum forum)
        {
            Forum = forum;
            PostNewDiscussionCommand = new RelayCommand<Frame>(
                async (frame) => {
                    if (await PostNewDiscussionAsync())
                    {
                        frame.GoBack();
                    }
                }
                );
        }

        private async Task<bool> PostNewDiscussionAsync()
        {

            if (Message.Trim().Length<=6||Subject.Trim().Length<=6)
            {
                await new MessageDialog("主题或信息这么短真的好么").ShowAsync();
                return false;
            }
            if (!_forum.PostTypes.ContainsValue(_selectedDiscussionType))
            {
                await new MessageDialog("请选择帖子类别").ShowAsync();
                return false;
            }

            string typeId = _forum.PostTypes[_selectedDiscussionType];

            (var status,var message) = await 
                DiscussionService.PostNewDiscussionAsync(_forum.Id,typeId, Subject, Message);

            if (!status)
            {
                await new MessageDialog(Message).ShowAsync();
            }

            return status;
        }

    }
}
