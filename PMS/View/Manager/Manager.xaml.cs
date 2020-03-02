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
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

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

            var myPMSSubItems = new List<SubItemMenu>();
            myPMSSubItems.Add(new SubItemMenu("Dashboard", new UserControlAddUser()));
            myPMSSubItems.Add(new SubItemMenu("Calendar", new UserControlUsersList()));
            var myPMS = new ItemMenu("My PMS", myPMSSubItems, PackIconKind.House);

            var tasksSubItems = new List<SubItemMenu>();
            tasksSubItems.Add(new SubItemMenu("Add task", new UserControlAddUser()));
            tasksSubItems.Add(new SubItemMenu("Task list", new UserControlAddUser()));
            var tasks = new ItemMenu("Tasks", tasksSubItems, PackIconKind.CalendarTask);

            var projectsSubItems = new List<SubItemMenu>();
            projectsSubItems.Add(new SubItemMenu("Add project"));
            projectsSubItems.Add(new SubItemMenu("Project list"));
            var projects = new ItemMenu("Projects", projectsSubItems, PackIconKind.Projector);

            var reportsSubItems = new List<SubItemMenu>();
            reportsSubItems.Add(new SubItemMenu("Financial"));
            var reports = new ItemMenu("Reports", reportsSubItems, PackIconKind.Report);

            var employeesSubItems = new List<SubItemMenu>();
            employeesSubItems.Add(new SubItemMenu("Add employee"));
            employeesSubItems.Add(new SubItemMenu("List employee"));
            var employees = new ItemMenu("Employees", employeesSubItems, PackIconKind.Worker);

            //var wikiSubItems = new List<SubItemMenu>();
            var wiki = new ItemMenu("Wiki", null, PackIconKind.Worker);

            Menu.Children.Add(new UserControlMenuItem(myPMS, this));
            Menu.Children.Add(new UserControlMenuItem(tasks, this));
            Menu.Children.Add(new UserControlMenuItem(projects, this));
            Menu.Children.Add(new UserControlMenuItem(reports, this));
            Menu.Children.Add(new UserControlMenuItem(employees, this));
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
    }
}
