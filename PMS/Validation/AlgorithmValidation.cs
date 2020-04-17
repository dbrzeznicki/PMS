using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PMS.Validation
{
    public class AlgorithmValidation
    {

        bool KLOCValidatin(int KLOC)
        {
            if (Regex.IsMatch(KLOC.ToString(), @"\d") && KLOC >= 2)
                return true;
            else
            {
                ErrorMessage er = new ErrorMessage("Incorrect KLOC! KLOC must be number and KLOC >= 2");
                er.ShowDialog();
                return false;
            }
        }



        public bool CocomoBasicValidation(int KLOC)
        {
            if (KLOCValidatin(KLOC))
                return true;
            else
                return false;
        }


    }
}
