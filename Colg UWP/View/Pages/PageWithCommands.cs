using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace Colg_UWP.View.Pages
{
    public abstract class PageWithCommands : Page
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

        protected virtual TextBlock TitleTextBlock => _mainPage.MenuTitleTextBlock;

        protected virtual CommandBar CommandBar => _mainPage.ContentCommandBar;

        public virtual string Title { get; private set; }

        private void UpdateCommands(CommandBar target,CommandBar local)
        {
            void Update(IObservableVector<ICommandBarElement> source,
            IObservableVector<ICommandBarElement> commands)
            {
                source.Clear();
                commands.ToList().ForEach(source.Add);
            }

            Update(target.PrimaryCommands, local.PrimaryCommands);
            Update(target.SecondaryCommands, local.SecondaryCommands);

        }

        private void UpdateTitle()
        {
            TitleTextBlock.Text = Title;
        }

        public abstract CommandBar LocalCommandBar { get;  }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
           UpdateTitle();
           UpdateCommands(CommandBar,LocalCommandBar);
           base.OnNavigatedTo(e);
        }
    }
}
