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

        public virtual IObservableVector<ICommandBarElement> PrimaryCommands
        {
            set
            {
                UpdateCommands(CommandBar.PrimaryCommands,value);
            }
        }

        public virtual IObservableVector<ICommandBarElement> SecondaryCommands
        {
            set
            {
                UpdateCommands(CommandBar.SecondaryCommands,value);
            }
        }

        protected virtual CommandBar CommandBar => _mainPage.ContentCommandBar;

        public virtual string Title { get; private set; }

        protected void UpdateCommands(IObservableVector<ICommandBarElement> source,
            IObservableVector<ICommandBarElement> commands)
        {
            source.Clear();
            commands.ToList().ForEach(source.Add);
        }
    }
}
