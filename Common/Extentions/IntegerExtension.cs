using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Extentions
{
    public static class IntegerExtension
    {
        public static long ToLong(this long? value)
        {
            long result = value ?? 0;
            return result;
        }
    }
}
