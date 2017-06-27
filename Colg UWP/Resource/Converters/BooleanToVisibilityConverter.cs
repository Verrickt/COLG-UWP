using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace Colg_UWP.Resource.Converters
{
    /// <summary>
    /// This converter maps true to Visiblity.Visible and false to Visiblity.Collapsed 
    /// if IsInverted is set to false;
    /// </summary>
    internal class BooleanToVisibilityConverter : IValueConverter
    {
        public bool IsInverted { get; set; } = false;

        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is bool b)
            {
                return b^IsInverted ? Visibility.Visible : Visibility.Collapsed;
            }
            return Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotSupportedException();
        }
    }
}