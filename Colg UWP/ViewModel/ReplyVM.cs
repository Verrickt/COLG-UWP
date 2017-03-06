using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Windows.UI.Popups;
using Windows.UI.Xaml.Controls;
using Colg_UWP.Model;
using Colg_UWP.Service;
using Colg_UWP.Util;

namespace Colg_UWP.ViewModel
{
    public class ReplyVM:VMBase
    {
        private string _replyText;
        private Discussion _discussion;
        private Reply _quotedReply;

        public string ReplyText
        {
            get { return _replyText; }
            set { SetProperty(ref _replyText,value); }
        }

        public Reply QuotedReply
        {
            get { return _quotedReply; }
            set { SetProperty(ref _quotedReply,value); }
        }

        public Discussion Discussion
        {
            get { return _discussion; }
            set { SetProperty(ref _discussion,value); }
        }


        public ReplyVM(Discussion discussion,Reply quotedReply=null)
        {
            Discussion = discussion;
            QuotedReply = quotedReply;
            ReplyText = String.Empty;
            PostNewReplayCommand = new RelayCommand<Frame>(
                async (frame) =>
                {
                    bool status = await PostNewReplyAsync();

                    if (status)
                    {
                        frame.GoBack();
                        ReplyText = String.Empty;
                    }
                },
                () =>  UserDataManager.GetActiveUser() != null
            );

        }
                

        private async Task<bool> PostNewReplyAsync()
        {
            if (ReplyText.Length<=6)
            {
                InAppNotifier.Show("这么短真的合适吗");
                return false;
            }


            (bool status, string message) = await ReplyService.PostNewReplyAsync(Discussion.Id, ReplyText, QuotedReply?.Id);


            if (!status)
            {
                InAppNotifier.Show(message);
            }
            else
            {
                InAppNotifier.Show("回复成功");
                ReplyText = string.Empty;
            }
            return status;
        }

        public RelayCommand<Frame> PostNewReplayCommand { get; set; }
    }
}
