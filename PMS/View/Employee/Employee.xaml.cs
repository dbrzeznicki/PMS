using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace PMS
{
    /// <summary>
    /// Interaction logic for Employee.xaml
    /// </summary>
    public partial class Employee : Window
    {
        public Employee()
        {
            InitializeComponent();
            StackPanelMain.Children.Add(new UserControlWikiEmployee());

        }

        private void ListViewMenu_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UserControl usc = null;
            StackPanelMain.Children.Clear();

            switch (((ListViewItem)((ListView)sender).SelectedItem).Name)
            {
                case "Home":
                    usc = new UserControlWikiEmployee();
                    StackPanelMain.Children.Add(usc);
                    break;
                case "Task":
                    usc = new UserControlTaskEmployee();
                    StackPanelMain.Children.Add(usc);
                    break;
                case "Project":
                    usc = new UserControlWikiEmployee();
                    StackPanelMain.Children.Add(usc);
                    break;
                case "Calendar":
                    usc = new UserControlWikiEmployee();
                    StackPanelMain.Children.Add(usc);
                    break;
                case "Wiki":
                    usc = new UserControlWikiEmployee();
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
