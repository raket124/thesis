using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace master.Utils
{
    static class EnumerableUtil
    {
        public static IEnumerable<object> Interleave<T, Y>(this IEnumerable<T> source1, IEnumerable<Y> source2)
        {

            if (source1 == null)
                throw new ArgumentNullException(nameof(source1));
            if (source2 == null)
                throw new ArgumentNullException(nameof(source2));

            using (var enumerator1 = source1.GetEnumerator())
            {
                using (var enumerator2 = source2.GetEnumerator())
                {
                    var continue1 = true;
                    var continue2 = true;
                    do
                    {
                        if (continue1 = enumerator1.MoveNext())
                            yield return enumerator1.Current;

                        if (continue2 = enumerator2.MoveNext())
                            yield return enumerator2.Current;
                    }
                    while (continue1 || continue2);
                }
            }
        }


        public static IEnumerable<T> Interleave<T>(this IEnumerable<T> source1, IEnumerable<T> source2)
        {

            if (source1 == null)
                throw new ArgumentNullException(nameof(source1));
            if (source2 == null)
                throw new ArgumentNullException(nameof(source2));

            using (var enumerator1 = source1.GetEnumerator())
            {
                using (var enumerator2 = source2.GetEnumerator())
                {
                    var continue1 = true;
                    var continue2 = true;
                    do
                    {
                        if (continue1 = enumerator1.MoveNext())
                            yield return enumerator1.Current;

                        if (continue2 = enumerator2.MoveNext())
                            yield return enumerator2.Current;
                    }
                    while (continue1 || continue2);
                }
            }
        }
    }
}
