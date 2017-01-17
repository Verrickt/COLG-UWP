using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Core;
using Windows.UI.Popups;
using Windows.UI.Xaml.Controls;

namespace Colg_UWP.Helper
{
    public class InAppNotifier
    {
        private static CoreDispatcher _dispatcher = null;

        public static CoreDispatcher Dispatcher
        {
            get { return _dispatcher; }
            set { _dispatcher = value; }
        }

        public static async void Show(string title, string detail)
        {
            await _dispatcher.RunAsync(
                CoreDispatcherPriority.Normal, async () =>
                {
                    var dialog = new View.Controls.InAppNotification() {TitleText = title, BodyText = detail};
                    var task = dialog.ShowAsync();
                    await Task.Delay(2000);
                    task.Cancel();
                });
        }

        public static async void Show(bool condition, string title1, string title2, string detail1, string detail2)
        {
            string title = condition ? title1 : title2;
            string detail = condition ? detail1 : detail2;
            Show(title,detail);
        }
    }
}
