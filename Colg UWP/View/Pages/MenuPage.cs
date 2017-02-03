using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Colg_UWP.View.Pages
{
    public abstract class MenuPage:PageWithCommands
    {
        protected Frame ContentFrame => _mainPage.ContentFrame;

        protected override CommandBar CommandBar => _mainPage.MenuCommandBar;

        protected override TextBlock TitleTextBlock => _mainPage.MenuTitleTextBlock;
    }
}
