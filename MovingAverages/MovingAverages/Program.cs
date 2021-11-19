using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.UI.WebControls;

namespace MovingAverages
{
    class Program
    {
        static void Main(string[] args)
        {
            var average = new MovingAverage(k: 3);

            /*  0	0	    (0 + 0 + 0) / 3
                1	0.33	(0 + 0 + 1) / 3
                2	1	    (0 + 1 + 2) / 3
                3	2	    (1 + 2 + 3) / 3
            */

            var input = new[] { 0, 1, 2, 3 };
            List<double> result = new List<double>();
            foreach (double value in input)
            {
                var avg = average.calculate(value);
                result.Add(avg);
            }
            Console.WriteLine(string.Join(", ", result));

            average = new MovingAverage(k: 5);

            var input2 = new[] { 0, 1, -2, 3, -4, 5, -6, 7, -8, 9 };
            List<double> result2 = new List<double>();
            foreach (double value in input2)
            {
                var avg = average.calculate(value);
                result2.Add(avg);
            }
            Console.WriteLine(string.Join(", ", result2));
        }
    }

    public class MovingAverage
    {
        private readonly int t;
        private readonly int[] values;

        private int i = 0;
        private double sum = 0;

        public MovingAverage(int k)
        {
            if (k <= 0)
            {
                throw new ArgumentOutOfRangeException(
                   "k can't be negative or 0.");
            }

            t = k;
            values = new int[k];
        }

        public double calculate(double next)
        {
            sum = sum - values[i] + next;
            values[i] = (int)next;
            i = (i + 1) % t;

            return sum / t;
        }
    }

}
