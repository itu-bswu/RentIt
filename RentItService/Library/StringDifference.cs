using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RentItService.Library
{
    static class StringDifference
    {
        public static int DifferenceTo(this string word1, string word2)
        {
            var len1 = word1.Length;
            var len2 = word2.Length;

            var matrix = new int[len1 + 1, len2 + 1];

            int i;

            for (i = 0; i <= len1; i++)
            {
                matrix[i, 0] = i;
            }

            for (i = 0; i <= len2; i++)
            {
                matrix[0, i] = i;
            }

            for (i = 0; i < len1; i++)
            {
                int j;

                for (j = 0; j < len2; j++)
                {
                    var eql = (word1[i] != word2[j] ? 1 : 0);

                    var x = i + 1;
                    var y = j + 1;

                    var valueLeft = matrix[x - 1, y] + 1;
                    var valueTop = matrix[x, y - 1] + 1;
                    var valueDiag = matrix[x - 1, y - 1] + eql;

                    matrix[x, y] = Math.Min(Math.Min(valueLeft, valueTop), valueDiag);
                }
            }

            return matrix[len1, len2];
        }
    }
}
