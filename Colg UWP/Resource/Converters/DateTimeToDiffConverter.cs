using System;
using Windows.UI.Xaml.Data;

namespace Colg_UWP.Resource.Converters
{
    public class DateTimeToDiffConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is DateTime)
            {
                var toConvert = (DateTime)value;
                var now = DateTime.Now;
                var diff = now.Subtract(toConvert);
                if (diff.Days > 365)
                {
                    return ToInterval(diff.Days, 365, "年");
                }
                if (diff.Days > 30)
                {
                    return ToInterval(diff.Days, 30, "月");
                }
                if (diff.Days > 1)
                {
                    return ToInterval(diff.Days, 1, "天");
                }
                if (diff.Hours > 1)
                {
                    return ToInterval(diff.Hours, 1, "小时");
                }
                if (diff.Minutes > 1)
                {
                    return ToInterval(diff.Minutes, 1, "分钟");
                }
                return "刚刚";
            }
            else
                return value;
        }

        private string ToInterval(int value, int upperBound, string unit)
        {
            if (value > upperBound)
            {
                return $"{value / upperBound + unit}前";
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}