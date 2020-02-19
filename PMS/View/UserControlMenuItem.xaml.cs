using PMS.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PMS
{

    public partial class UserControlMenuItem : UserControl
    {
        /// <summary>
        /// 
        /// </summary>
        Admin _context;
        public UserControlMenuItem(ItemMenu itemMenu, Admin context)
        {
            InitializeComponent();

            _context = context;

            //expander do rozwinięcia menu, jeśli nie ma subitems to nie ma strzałki do rozwijania
            ExpanderMenu.Visibility = itemMenu.SubItemsMenu == null ? Visibility.Collapsed : Visibility.Visible;

            //lista menu, jeśli nie ma subitems to wyswietlana, inaczej nie
            ListViewItemMenu.Visibility = itemMenu.SubItemsMenu == null ? Visibility.Visible : Visibility.Collapsed;

            this.DataContext = itemMenu;
        }

        /// <summary>
        /// Funkcja do zmiany user control 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ListViewMenu_SwitchScreen(object sender, SelectionChangedEventArgs e)
        {
            _context.SwitchScreen(((SubItemMenu)((ListView)sender).SelectedItem).UserControl);
        }
    }
}
