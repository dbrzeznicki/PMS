using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.Utils
{
    public class NormalDistribution
    {
        public static double calculateNormalDistribution(double number)
        {
            double sign = 1;
            if (number < 0) 
                sign = -1;
            return 0.5 * (1.0 + sign * ERF(Math.Abs(number) / Math.Sqrt(2)));
        }

        private static double ERF(double number)
        {
            double p = 0.3275911;
            double tmp1 = 0.254829592;
            double tmp2 = -0.284496736;
            double tmp3 = 1.421413741;
            double tmp4 = -1.453152027;
            double tmp5 = 1.061405429;
           
            number = Math.Abs(number);
            double t = 1 / (1 + p * number);
            return 1 - ((((((tmp5 * t + tmp4) * t) + tmp3) * t + tmp2) * t) + tmp1) * t * Math.Exp(-1 * number * number);
        }
    }
}
