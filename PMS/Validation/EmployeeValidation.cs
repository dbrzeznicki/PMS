using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PMS
{
    public class EmployeeValidation
    {

        bool UrlValidation(string url)
        {
            if (Regex.IsMatch(url, @"^(http|https|ftp|)\://|[a-zA-Z0-9\-\.]+\.[a-zA-Z](:[a-zA-Z0-9]*)?/?([a-zA-Z0-9\-\._\?\,\'/\\\+&amp;%\$#\=~])*[^\.\,\)\(\s]$"))
                return true;
            else
            {
                ErrorMessage er = new ErrorMessage("Incorrect url!");
                er.ShowDialog();
                return false;
            }
        }

        bool DescriptionUrlValidation(string description)
        {
            if (description.Length >= 20)
                return true;
            else
            {
                ErrorMessage er = new ErrorMessage("Incorrect description! Description has less than 20 characters.");
                er.ShowDialog();
                return false;
            }
        }


        public bool WikiValidation(string url, string description)
        {
            if (UrlValidation(url) && DescriptionUrlValidation(description))
                return true;
            else
                return false;
        }




        bool NameTaskValidation(string nameTask)
        {
            if (nameTask.Length >= 6)
                return true;
            else
            {
                ErrorMessage er = new ErrorMessage("Incorrect name task! Description has less than 6 characters.");
                er.ShowDialog();
                return false;
            }
        }

        bool DescriptionTaskValidation(string description)
        {
            if (description.Length >= 20)
                return true;
            else
            {
                ErrorMessage er = new ErrorMessage("Incorrect description! Description has less than 20 characters.");
                er.ShowDialog();
                return false;
            }
        }


        public bool AddTaskValidation(string nameTask, string descriptionTask)
        {
            if (NameTaskValidation(nameTask) && DescriptionTaskValidation(descriptionTask))
                return true;
            else
                return false;
        }
    }
}
