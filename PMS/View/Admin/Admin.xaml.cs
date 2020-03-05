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
            StackPanelMain.Children.Add(new UserControlAddUser());

            var myPMSSubItems = new List<SubItemMenu>();
            myPMSSubItems.Add(new SubItemMenu("Add user", new UserControlAddUser()));
            myPMSSubItems.Add(new SubItemMenu("Users list", new UserControlUsersList()));
            var myPMS = new ItemMenu("User 1", myPMSSubItems, PackIconKind.House);

            var tasksSubItems = new List<SubItemMenu>();
            tasksSubItems.Add(new SubItemMenu("Edit user", new UserControlEditUser()));
            var tasks = new ItemMenu("User 2", tasksSubItems, PackIconKind.CalendarTask);

            //var wikiSubItems = new List<SubItemMenu>();
            var wiki = new ItemMenu("Wiki", null, PackIconKind.Worker);

            Menu.Children.Add(new UserControlMenuItem(myPMS, this));
            Menu.Children.Add(new UserControlMenuItem(tasks, this));
            Menu.Children.Add(new UserControlMenuItem(wiki, this));
        }

        internal void SwitchScreen(object sender)
        {
            var userControl = ((UserControl)sender);

            if (userControl != null)
            {
                StackPanelMain.Children.Clear();
                StackPanelMain.Children.Add(userControl);
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

