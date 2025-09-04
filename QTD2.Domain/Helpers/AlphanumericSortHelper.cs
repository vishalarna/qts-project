using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Helpers
{
    // Custom comparer for sorting strings to prioritize '§'  and then alphanumeric nature
    public class AlphaNumericSortHelper : IComparer<string>
    {
        public int Compare(string x, string y)
        {
            int ComparePrefixPriority(string s)
            {
                if (!string.IsNullOrEmpty(s))
                {
                    if (s.StartsWith("§", StringComparison.OrdinalIgnoreCase)) return 0;
                    else return 1;
                }
                return 1;
            }

            int prefixComparison = ComparePrefixPriority(x).CompareTo(ComparePrefixPriority(y));

            if (prefixComparison != 0)
            {
                return prefixComparison;
            }

            return AlphanumericCompare(x, y);
        }

        // Alphanumeric comparison logic (same as in the previous examples).
        private int AlphanumericCompare(string x, string y)
        {
            string[] partsX = (x ?? "").Split('.').Select(part => part.TrimStart('§')).ToArray();
            string[] partsY = (y ?? "").Split('.').Select(part => part.TrimStart('§')).ToArray();

            for (int i = 0; i < Math.Min(partsX.Length, partsY.Length); i++)
            {
                int result;
                if (int.TryParse(partsX[i], out int numX) && int.TryParse(partsY[i], out int numY))
                {
                    result = numX.CompareTo(numY);
                }
                else
                {
                    result = partsX[i].CompareTo(partsY[i]);
                }

                if (result != 0)
                {
                    return result;
                }
            }

            return partsX.Length.CompareTo(partsY.Length);
        }
    }
}
