using System;
using Windows.UI.Xaml.Data;

namespace Colg_UWP.Resource
{
    /// <summary>
    /// A converter that does **Nothing!**
    ///when you x:Bind a GridView/ListView 's selectedItem Porperty Two-Way to 
    /// a property in you viewmodel,if you don't have a converter,compiler will complain.
    /// Reason is simple.x:Bind is created at compile time and GridView/ListView 's SelectedItem is of type object.
    /// So you have to provide such a mechanism to allow compiling to continue.
    /// </summary>
    class GenericObjectConverter:IValueConverter
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
