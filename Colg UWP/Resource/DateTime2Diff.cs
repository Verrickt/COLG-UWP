using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;

namespace Colg_UWP.Resource
{
    public class DateTime2Diff : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is DateTime)
            {
                var toConvert = (DateTime)value;
                var now = DateTime.Now;
                var diff = now.Subtract(toConvert);
                if (diff.Days>365)
                {
                    return ToInterval(diff.Days, 365, "年");
                }
                if (diff.Days>30)
                {
                    return ToInterval(diff.Days, 30, "月");
                }
                if (diff.Days>1)
                {
                    return ToInterval(diff.Days, 1, "天");
                }
                if (diff.Hours>1)
                {
                    return ToInterval(diff.Hours, 1, "小时");
                }
                if (diff.Minutes>1)
                {
                    return ToInterval(diff.Minutes, 1, "分钟");
                }
                return "刚刚";
            }
            else
                return value;

        }

        public string ToInterval(int value,int upper,string upperUnit)
        {
            if (value>upper)
            {
                return $"{value/upper+upperUnit}前";
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
