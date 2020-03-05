using MaterialDesignThemes.Wpf;
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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PMS
{
    public partial class Admin : Window
    {

        public Admin()
        {
            InitializeComponent();
            StackPanelMain.Children.Add(new UserControlUsersList());
        }


        private void ListViewMenu_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UserControl usc = null;
            StackPanelMain.Children.Clear();

            switch (((ListViewItem)((ListView)sender).SelectedItem).Name)
            {
                case "UsersList":
                    usc = new UserControlUsersList();
                    StackPanelMain.Children.Add(usc);
                    break;
                case "AddUser":
                    usc = new UserControlAddUser();
                    StackPanelMain.Children.Add(usc);
                    break;
                case "EditUser":
                    usc = new UserControlEditUser();
                    StackPanelMain.Children.Add(usc);
                    break;
                default:
                    break;
            }
        }

        private void CloseButton(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void ResizeButton(object sender, RoutedEventArgs e)
        {
            if (WindowState == WindowState.Maximized)
                WindowState = WindowState.Normal;
            else
                WindowState = WindowState.Maximized;
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }
    }
}

