using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace PMS.ViewModel
{
    public class SubItemMenu
    {
        public string Name { get; set; }
        public UserControl UserControl { get; set; }

        public SubItemMenu(string name, UserControl userControl = null)
        {
            Name = name;
            UserControl = userControl;
        }

    }
}

