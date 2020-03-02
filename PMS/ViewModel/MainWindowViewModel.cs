using PMS.DAL;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace PMS.ViewModel
{
    public class MainWindowViewModel : BindableBase
    {

        #region Constructor
        public MainWindowViewModel()
        {
            ZalogujButton = new DelegateCommand(Zaloguj);
            ResetButton = new DelegateCommand(Reset);
        }

        #endregion


        #region Variable
        private readonly PMSContext dbContext = new PMSContext();
        private string _Login { get; set; }
        private string _Password { get; set; }

        public string Login
        {
            get
            {
                return _Login;
            }
            set
            {
                _Login = value;
                RaisePropertyChanged("Login");
            }
        }

        public string Password
        {
            get
            {
                return _Password;
            }
            set
            {
                _Password = value;
                RaisePropertyChanged("Password");
            }
        }

        #endregion


        #region Command
        public ICommand ZalogujButton { get; set; }
        public ICommand ResetButton { get; set; }

        #endregion

        #region Method
        public void Zaloguj()
        {
            var user = dbContext.User.Where(x => x.Login.Equals(Login) && x.Password.Equals(Password)).SingleOrDefault();

            var userRole = "";
            if (user != null)
            {
                userRole = dbContext.UserRole.Find(user.UserRoleID).Name.ToString();
                Global.user = user;
            }
                

            if (user == null)
            {
                ErrorMessage er = new ErrorMessage("Incorrect login or password!");
                er.ShowDialog();
            }


            else if (userRole.Equals("Admin"))
            {
                Admin adminWindow = new Admin();
                App.Current.MainWindow.Close();
                App.Current.MainWindow = adminWindow;
                adminWindow.Show();
            }

            else if (userRole.Equals("Manager"))
            {
                Manager managerWindow = new Manager();
                App.Current.MainWindow.Close();
                App.Current.MainWindow = managerWindow;
                managerWindow.Show();
            }

            else if (userRole.Equals("Employee"))
            {
                Employee employeeWindow = new Employee();
                App.Current.MainWindow.Close();
                App.Current.MainWindow = employeeWindow;
                employeeWindow.Show();
            }
        }

        private void Reset()
        {
            Login = "";
            Password = "";
        }

        #endregion
    }
}
