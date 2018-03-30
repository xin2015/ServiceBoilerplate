using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceBoilerplate.BLL.Extensions
{
    public static class StringExtension
    {
        public static double? ToNullableDouble(this string text)
        {
            double data;
            if (double.TryParse(text, out data))
            {
                return data;
            }
            else
            {
                return null;
            }
        }
    }
}
