using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Colg_UWP.View.Pages
{
    public class PageWithCommands : Page
    {
        protected MainPage _mainPage
        {
            get
            {
                var frame =
                    (Frame) Window.Current.Content;
                return
                    (MainPage) frame.Content;
            }
        }

        public virtual IObservableVector<ICommandBarElement> PrimaryCommands { get; private set; }

        public virtual IObservableVector<ICommandBarElement> SecondaryCommands { get; private set; }

        public virtual string Title { get; private set; }
    }
}
