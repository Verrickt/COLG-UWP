using System;

namespace Colg_UWP.Resource.Converters
{
    internal class DateTimeToCompressedStringConverter : Windows.UI.Xaml.Data.IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is DateTime)
            {
                DateTime time = (DateTime)value;
                TimeSpan diff = DateTime.Now.Subtract(time);
                string format;
                if (diff.TotalDays > 365d)
                {
                    format = "yy/MM/dd HH:mm";
                }
                else if (diff.TotalDays > 1d)
                {
                    format = "MM/dd HH:mm";
                }
                else
                {
                    format = "HH:mm";
                }
                return time.ToString(format);
            }
            else
            {
                return value;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}