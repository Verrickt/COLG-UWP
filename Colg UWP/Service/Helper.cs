using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colg_UWP.Service
{
    public static class Helper
    {
        public static DateTime? StringToDateTime(string str)
        {
            DateTime d;

            long tick;
            if (long.TryParse(str, out tick))
            {
                d = DateTimeOffset.FromUnixTimeSeconds(tick).DateTime;
                return d.ToLocalTime();
            }
            else
            {
                if (DateTime.TryParse(str + " +08:00", out d))
                {
                    return d.ToLocalTime();
                }
                else
                {
                    return null;
                }


            }
        }

        public static string ValueForEitherName(this JToken token, string key1, string key2)
        {
            var str1 = token[key1]?.ToString();
            var str2 = token[key2]?.ToString();

            if (str1 != null && str2 != null)
            {
                throw new ArgumentException("Both the key contains a valid value");
            }
            if (str1 == null && str2 == null)
            {
                throw new NullReferenceException("Can't find a value by both key");
            }
            return str1 ?? str2;
        }
    }
}
