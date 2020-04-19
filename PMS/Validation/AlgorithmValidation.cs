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

        /*bool NumberOfElementsValidatin(int[] NumberOfElements)
        {
            int tmp = 0;
            for(int i=0; i < 5; i++)
            {
                if (Regex.IsMatch(NumberOfElements[i].ToString(), @"[0-9]"))
                    tmp = 0;
                else
                {
                    tmp = 1;
                    break;
                }
            }

            if (tmp == 0)
                return true;
            else
            {
                ErrorMessage er = new ErrorMessage("Incorrect number of elements. Value must be > 0.");
                er.ShowDialog();
                return false;
            }
        }*/

        public bool CocomoBasicValidation(int KLOC)
        {
            if (KLOCValidatin(KLOC))
                return true;
            else
                return false;
        }


        //public bool FunctionPointValidation(int[] NumberOfElements)
        //{
        //    if (NumberOfElementsValidatin(NumberOfElements))
        //        return true;
        //    else
        //        return false;
        //}
    }
}
