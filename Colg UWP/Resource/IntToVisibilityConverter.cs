using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace Colg_UWP.Resource
{
    /// <summary>
    /// A converter map any non-zero Int32 value to Visibility.Visible
    /// and zero value to Visibility.Collapsed
    /// </summary>
    public class IntToVisibilityConverter:IValueConverter
    {
       
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is int i)
            {
                return i == 0 ? Visibility.Collapsed : Visibility.Visible;
            }
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotSupportedException();
        }
    }
}
