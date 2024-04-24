using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace average.library
{
    internal class ModeCalculator
    {
        public static double[] CalculateModeDouble(double[] values)
        {
            Dictionary<double, long> pairs = new Dictionary<double, long>();
            
            foreach(double value in values)
            {
                if (pairs.ContainsKey(value))
                {
                    pairs[value] = pairs[value] + 1;
                }
                else
                {
                    pairs.Add(value, 1);
                }
            }

            List<double> modes = new List<double>();

            long[] descending = pairs.Values.OrderDescending().ToArray();

            for(int index = 0; index < descending.Length; index++)
            {
                var next = descending[index + 1] < descending.Length ? descending[index + 1] : descending[index];
                if (descending[index].Equals(next))
                {
                    var value = pairs.)
                }
            }

        }

        public static decimal[] CalculateModeDecimal(decimal[] values) 
        {

        }

        public static long[] CalculateModeLong(long[] values) 
        {
            
        }
    }
}
