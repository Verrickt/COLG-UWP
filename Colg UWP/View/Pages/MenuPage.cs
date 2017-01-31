using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace Colg_UWP.View.Pages
{
    public abstract class MenuPage : Page
    {
        private MainPage _mainPage
        {
            get
            {
                var frame = (Frame) Window.Current.Content;
                return (MainPage) frame.Content;
            }
        }

        protected ContainerPage Container => _mainPage.MenuContainer;

        protected Frame ContentFrame => _mainPage.ContentContainer.Frame;

        protected new Frame Frame => _mainPage.MenuContainer.Frame;

        public abstract string Title { get; set; }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            Container.TitleText = Title;
            base.OnNavigatedTo(e);
        }
    }
}
