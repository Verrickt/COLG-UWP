using Colg_UWP.View.Controls;
using System;

namespace Colg_UWP.Util
{
    public class InAppNotifier
    {
        public static void Show(string content, TimeSpan showTime)
        {
            ToastNotification popup = new ToastNotification(content, showTime);
            popup.Show();
        }

        public static void Show(string content)
        {
            Show(content, TimeSpan.FromSeconds(2));
        }
    }
}