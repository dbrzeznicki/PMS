using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace PMS
{
    /// <summary>
    /// Interaction logic for Manager.xaml
    /// </summary>
    public partial class Manager : Window
    {
        public Manager()
        {
            InitializeComponent();
            StackPanelMain.Children.Add(new UserControlHomeManager());
        }

        private void ListViewMenu_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UserControl usc = null;
            StackPanelMain.Children.Clear();

            switch (((ListViewItem)((ListView)sender).SelectedItem).Name)
            {
                case "Home":
                    usc = new UserControlHomeManager();
                    StackPanelMain.Children.Add(usc);
                    break;
                case "Project":
                    usc = new UserControlProjectManager();
                    StackPanelMain.Children.Add(usc);
                    break;
                case "Team":
                    usc = new UserControlTeamManager();
                    StackPanelMain.Children.Add(usc);
                    break;
                case "Task":
                    usc = new UserControlTaskManager();
                    StackPanelMain.Children.Add(usc);
                    break;
                case "PERT":
                    usc = new UserControlPERTManager();
                    StackPanelMain.Children.Add(usc);
                    break;
                case "Clients":
                    usc = new UserControlClientsManager();
                    StackPanelMain.Children.Add(usc);
                    break;
                case "Contracts":
                    usc = new UserControlContractsManager();
                    StackPanelMain.Children.Add(usc);
                    break;
                case "Vacations":
                    usc = new UserControlVacationsManager();
                    StackPanelMain.Children.Add(usc);
                    break;
                case "Wiki":
                    usc = new UserControlWikiManager();
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
