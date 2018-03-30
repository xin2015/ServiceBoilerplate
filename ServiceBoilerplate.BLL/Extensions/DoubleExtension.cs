using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceBoilerplate.BLL.Extensions
{
    public static class DoubleExtension
    {
        public static double? Round(this double? d)
        {
            return d.HasValue ? Math.Round(d.Value) : d;
        }

        public static double? Round(this double? d, int digits)
        {
            return d.HasValue ? Math.Round(d.Value, digits) : d;
        }

        public static double? Round(this double? d, int digits, MidpointRounding mode)
        {
            return d.HasValue ? Math.Round(d.Value, digits, mode) : d;
        }
    }
}
