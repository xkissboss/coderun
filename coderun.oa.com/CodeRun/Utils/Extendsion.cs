using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeRun.Utils
{
    public static class Extendsion
    {
        public static string Base64ToString(this string value)
        {
            try
            {
                return Encoding.UTF8.GetString(Convert.FromBase64String(value));
            }
            catch (Exception ex)
            {
                return string.Empty;
            }
        }
    }
}
