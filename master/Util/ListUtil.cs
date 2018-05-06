using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace master.Util
{
    public static class ListUtil
    {
        public static void Trim<T>(this List<T> input, T token)
        {
            while (input.Count > 0 && input.First().Equals(token))
                input.RemoveAt(0);

            while (input.Count > 0 && input.Last().Equals(token))
                input.RemoveAt(input.Count - 1);
        }
    }
}
