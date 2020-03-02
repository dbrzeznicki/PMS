using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace PMS.ViewModel
{
    public class ItemMenu
    {
        public string Name { get; set; }
        public List<SubItemMenu> SubItemsMenu { get; set; }
        public PackIconKind Icon { get; set; }
        
       
        public ItemMenu(string name, List<SubItemMenu> subItemsMenu, PackIconKind icon)
        {
            Name = name;
            SubItemsMenu = subItemsMenu;
            Icon = icon;
        }
    }
}

