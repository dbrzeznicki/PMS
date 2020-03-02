using PMS.ViewModel;
using System.Windows;
using System.Windows.Controls;


namespace PMS
{

    public partial class UserControlMenuItem : UserControl
    {
        /// <summary>
        /// 
        /// </summary>
        Admin _contextAdmin;
        Manager _contextManager;
        Employee _contextEmployee;

        public UserControlMenuItem(ItemMenu itemMenu, Admin contextAdmin)
        {
            InitializeComponent();

            _contextAdmin = contextAdmin;

            //expander do rozwinięcia menu, jeśli nie ma subitems to nie ma strzałki do rozwijania
            ExpanderMenu.Visibility = itemMenu.SubItemsMenu == null ? Visibility.Collapsed : Visibility.Visible;

            //lista menu, jeśli nie ma subitems to wyswietlana, inaczej nie
            ListViewItemMenu.Visibility = itemMenu.SubItemsMenu == null ? Visibility.Visible : Visibility.Collapsed;

            this.DataContext = itemMenu;
        }

        public UserControlMenuItem(ItemMenu itemMenu, Manager contextManager)
        {
            InitializeComponent();

            _contextManager = contextManager;

            //expander do rozwinięcia menu, jeśli nie ma subitems to nie ma strzałki do rozwijania
            ExpanderMenu.Visibility = itemMenu.SubItemsMenu == null ? Visibility.Collapsed : Visibility.Visible;

            //lista menu, jeśli nie ma subitems to wyswietlana, inaczej nie
            ListViewItemMenu.Visibility = itemMenu.SubItemsMenu == null ? Visibility.Visible : Visibility.Collapsed;

            this.DataContext = itemMenu;
        }

        public UserControlMenuItem(ItemMenu itemMenu, Employee contextEmployee)
        {
            InitializeComponent();

            _contextEmployee = contextEmployee;

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
            if (_contextAdmin != null)
                _contextAdmin.SwitchScreen(((SubItemMenu)((ListView)sender).SelectedItem).UserControl);
            if (_contextManager != null)
                _contextManager.SwitchScreen(((SubItemMenu)((ListView)sender).SelectedItem).UserControl);
            if (_contextEmployee != null)
                _contextEmployee.SwitchScreen(((SubItemMenu)((ListView)sender).SelectedItem).UserControl);
        }

    }
}
