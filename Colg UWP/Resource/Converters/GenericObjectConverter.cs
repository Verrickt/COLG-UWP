using System;
using Windows.UI.Xaml.Data;

namespace Colg_UWP.Resource.Converters
{
   /// <summary>
   /// Just to make compiler happy
   /// </summary>
    internal class GenericObjectConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return value;
        }
    }
}