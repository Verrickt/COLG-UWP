using Colg_UWP.View.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Core;
using Windows.UI.Popups;
using Windows.UI.Xaml.Controls;

namespace Colg_UWP.Util
{
    public class InAppNotifier
    {
        public static void Show(string content,TimeSpan showTime)
        {
            ToastNotification popup = new ToastNotification(content,showTime);
            popup.Show();
        }
        public static void Show(string content)
        {
            Show(content, TimeSpan.FromSeconds(2));
        }

    }
}
