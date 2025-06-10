using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace desktop_AmzOpsApi.DAL
{
    public static class DbUtils
    {
        // For reference types (e.g., string, object)
        public static object ToDbValue<T>(T value) where T : class
        {
            return value == null ? DBNull.Value : value;
        }

        // For nullable value types (e.g., int?, bool?, DateTime?)
        public static object ToDbValue<T>(T? value) where T : struct
        {
            return value.HasValue ? (object)value.Value : DBNull.Value;
        }
    }

}
