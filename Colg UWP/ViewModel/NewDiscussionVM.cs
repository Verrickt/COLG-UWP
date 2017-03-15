using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Popups;
using Windows.UI.Xaml.Controls;
using Colg_UWP.Model;
using Colg_UWP.Service;
using System.Collections.ObjectModel;
using Colg_UWP.Util;

namespace Colg_UWP.ViewModel
{
    public class NewDiscussionVM:VMBase
    {
        private Forum _forum;
        private string _subject;
        private string _message;

        public RelayCommand<Frame> PostNewDiscussionCommand { get; set; }

        public string SelectedDiscussionType { get; set; }
      

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

        public ObservableCollection<string> DiscussionTypes { get; set; }


        /// <summary>
        /// PostTypes in forum maps typeId to TypeName.
        /// Reverse it's key and value so we can ask user to select a TypeName and
        /// get a typeId, makes it easier to call API
        /// </summary>
        private Dictionary<string, string> ReversedDictionary;

        public NewDiscussionVM(Forum forum)
        {
            Forum = forum;
            ReversedDictionary= forum.PostTypes.ToDictionary(
                p => p.Value, p => p.Key);

            DiscussionTypes = new ObservableCollection<string>(
                ReversedDictionary.Keys);

            SelectedDiscussionType = null;

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
            string typeId = null;
            if (Message.Trim().Length<=6||Subject.Trim().Length<=6)
            {
                await new MessageDialog("主题或信息这么短真的好么").ShowAsync();
                return false;
            }
            if (SelectedDiscussionType!=null&&DiscussionTypes.Count>0)
            {
                await new MessageDialog("请先选择帖子类别").ShowAsync();
                return false;
            }
            if (SelectedDiscussionType!=null)
            {
                typeId = ReversedDictionary[SelectedDiscussionType];
            }
            (var status,var message) = await 
                DiscussionService.PostNewDiscussionAsync(_forum.Id,typeId, Subject, Message);

            if (!status)
            {
                await new MessageDialog(message).ShowAsync();
            }
            else
            {
                InAppNotifier.Show("发表成功");
            }

            return status;
        }

    }
}
