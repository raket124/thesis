using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace master.Util
{
    public static class StringExtension
    {
        public static List<int> IndexesOf(this string input, string token)
        {
            var output = new List<int>();
            for (int i = input.IndexOf(token); i > -1; i = input.IndexOf(token, i + 1))
                output.Add(i);
            return output;
        }
    }
}
