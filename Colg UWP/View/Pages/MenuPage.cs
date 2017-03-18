using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Colg_UWP.View.Pages
{
    public class MenuPage : Page
    {
        private MainPage _mainPage
        {
            get
            {
                var frame =
                    (Frame)Window.Current.Content;
                return
                    (MainPage)frame.Content;
            }
        }

        protected Frame ContentFrame => _mainPage.Main_ContentFrame;
    }
}