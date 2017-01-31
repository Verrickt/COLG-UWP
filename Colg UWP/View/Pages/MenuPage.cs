using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Colg_UWP.View.Pages
{
    public class MenuPage:Page
    {
        protected Frame ContentFrame
        {
            get
            {
                var frame = (Frame)Window.Current.Content;
                var page = (MainPage)frame.Content;
                return page.ContentFrame;
            }
        }

       
    }
}
