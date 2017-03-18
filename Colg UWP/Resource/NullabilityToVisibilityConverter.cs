using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace Colg_UWP.Resource
{
    public class NullabilityToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            return value == null ? Visibility.Collapsed : Visibility.Visible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}