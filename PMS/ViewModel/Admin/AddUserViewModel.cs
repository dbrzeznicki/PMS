using PMS.DAL;
using PMS.Model;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PMS
{
    public class AddUserViewModel : BindableBase
    {

        #region variable

        //Add user
        //public string _VisibilityTeam = "Visible"; //"Collapsed";
        private string _FirstName = "";
        private string _LastName = "";
        private string _Login = "";
        private string _Password = "";
        private string _UserRole = "Employee";
        private double _Salary;
        private string _PhoneNumber = "";
        private string _Email = "";
        //private string _Team;
        private string _ResidenceStreet;
        private string _ResidenceHouseNumber;
        private string _ResidenceApartmentNumber;
        private string _ResidencePostcode;
        private string _ResidenceCity;
        private string _CorrespondenceStreet;
        private string _CorrespondenceHouseNumber;
        private string _CorrespondenceApartmentNumber;
        private string _CorrespondencePostcode;
        private string _CorrespondenceCity;
        public DateTime _HiredDate = DateTime.Now;
        public DateTime _FiredDate;

        #endregion



        #region command

        public ICommand AddUserButton { get; set; }

        #endregion


        #region constructor

        public AddUserViewModel()
        {
            AddUserButton = new DelegateCommand(AddUser);
        }

        #endregion

        #region properties

        public string FirstName
        {
            get
            {
                return _FirstName;
            }
            set
            {
                _FirstName = value;
                RaisePropertyChanged("FirstName");
            }
        }
        public string LastName
        {
            get
            {
                return _LastName;
            }
            set
            {
                _LastName = value;
                RaisePropertyChanged("LastName");
            }
        }
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
        public string UserRole
        {
            get
            {

                return _UserRole;
            }
            set
            {
                _UserRole = value;
                //if(_UserRole.Equals("Admin"))
                //{
                //    VisibilityTeam = "Collapsed";
                //    Team = null;
                //}
                //else
                //    VisibilityTeam = "Visible";
                RaisePropertyChanged("UserRole");
            }
        }
        public double Salary
        {
            get
            {
                return _Salary;
            }
            set
            {
                _Salary = value;
                RaisePropertyChanged("Salary");
            }
        }
        public string PhoneNumber
        {
            get
            {
                return _PhoneNumber;
            }
            set
            {
                _PhoneNumber = value;
                RaisePropertyChanged("PhoneNumber");
            }
        }
        public string Email
        {
            get
            {
                return _Email;
            }
            set
            {
                _Email = value;
                RaisePropertyChanged("Email");
            }
        }
        /*public string Team
        {
            get
            {
                return _Team;
            }
            set
            {
                _Team = value;
                RaisePropertyChanged("Team");
            }
        }*/

        public string ResidenceStreet
        {
            get
            {
                return _ResidenceStreet;
            }
            set
            {
                _ResidenceStreet = value;
                RaisePropertyChanged("ResidenceStreet");
            }
        }
        public string ResidenceHouseNumber
        {
            get
            {
                return _ResidenceHouseNumber;
            }
            set
            {
                _ResidenceHouseNumber = value;
                RaisePropertyChanged("ResidenceHouseNumber");
            }
        }
        public string ResidenceApartmentNumber
        {
            get
            {
                return _ResidenceApartmentNumber;
            }
            set
            {
                _ResidenceApartmentNumber = value;
                RaisePropertyChanged("ResidenceApartmentNumber");
            }
        }
        public string ResidencePostcode
        {
            get
            {
                return _ResidencePostcode;
            }
            set
            {
                _ResidencePostcode = value;
                RaisePropertyChanged("ResidencePostcode");
            }
        }
        public string ResidenceCity
        {
            get
            {
                return _ResidenceCity;
            }
            set
            {
                _ResidenceCity = value;
                RaisePropertyChanged("ResidenceCity");
            }
        }
        public string CorrespondenceStreet
        {
            get
            {
                return _CorrespondenceStreet;
            }
            set
            {
                _CorrespondenceStreet = value;
                RaisePropertyChanged("CorrespondenceStreet");
            }
        }
        public string CorrespondenceHouseNumber
        {
            get
            {
                return _CorrespondenceHouseNumber;
            }
            set
            {
                _CorrespondenceHouseNumber = value;
                RaisePropertyChanged("CorrespondenceHouseNumber");
            }
        }
        public string CorrespondenceApartmentNumber
        {
            get
            {
                return _CorrespondenceApartmentNumber;
            }
            set
            {
                _CorrespondenceApartmentNumber = value;
                RaisePropertyChanged("CorrespondenceApartmentNumber");
            }
        }
        public string CorrespondencePostcode
        {
            get
            {
                return _CorrespondencePostcode;
            }
            set
            {
                _CorrespondencePostcode = value;
                RaisePropertyChanged("CorrespondencePostcode");
            }
        }
        public string CorrespondenceCity
        {
            get
            {
                return _CorrespondenceCity;
            }
            set
            {
                _CorrespondenceCity = value;
                RaisePropertyChanged("CorrespondenceCity");
            }
        }
        public DateTime HiredDate
        {
            get
            {
                return _HiredDate;
            }
            set
            {
                _HiredDate = value;
                RaisePropertyChanged("HiredDate");
            }
        }
        public DateTime FiredDate
        {
            get
            {
                return _FiredDate;
            }
            set
            {
                _FiredDate = value;
                RaisePropertyChanged("FiredDate");
            }
        }
        /*public string VisibilityTeam
        {
            get
            {
                return _VisibilityTeam;
            }
            set
            {
                _VisibilityTeam = value;
                RaisePropertyChanged("VisibilityTeam");
            }
        }*/

        #endregion


        private void AddUser()
        {
            PMSContext dbContext = new PMSContext();
            User user;

            AdminValidation AV = new AdminValidation();
            bool correctForm = AV.AddUserValidation(FirstName, LastName, Login, Password, Salary, PhoneNumber, Email, UserRole);

            if (correctForm == true)
            {
                if (CorrespondenceCity == null)
                {
                    user = AddUserWithoutCorrespondenceAdress();

                    dbContext.User.Add(user);
                    dbContext.SaveChanges();

                    ErrorMessage er = new ErrorMessage("User created successfully!");
                    er.ShowDialog();
                }

                else
                {
                    user = AddUserWithCorrespondenceAdress();
                    dbContext.User.Add(user);
                    dbContext.SaveChanges();

                    ErrorMessage er = new ErrorMessage("User created successfully!");
                    er.ShowDialog();
                }

            }
        }

        private User AddUserWithCorrespondenceAdress()
        {
            PMSContext dbContext = new PMSContext();
            var userRole = dbContext.UserRole.Where(n => n.Name.Equals(_UserRole)).SingleOrDefault();
            int userRoleID = userRole.UserRoleID;
            User user;


            user = new User()
            {
                UserRoleID = userRoleID,
                TeamID = null,
                FirstName = _FirstName,
                LastName = _LastName,
                Login = _Login,
                Password = _Password,
                Salary = _Salary,
                PhoneNumber = _PhoneNumber,
                Email = _Email,
                AccountCreationDate = DateTime.Now,
                HiredDate = _HiredDate,
                FiredDate = _HiredDate.AddYears(5),
                ResidenceStreet = _ResidenceStreet,
                ResidenceHouseNumber = _ResidenceHouseNumber,
                ResidenceApartmentNumber = _ResidenceApartmentNumber,
                ResidenceCity = _ResidenceCity,
                ResidencePostcode = _ResidencePostcode,
                CorrespondenceStreet = _CorrespondenceStreet,
                CorrespondenceHouseNumber = _CorrespondenceHouseNumber,
                CorrespondenceApartmentNumber = _CorrespondenceApartmentNumber,
                CorrespondencePostcode = _CorrespondencePostcode,
                CorrespondenceCity = _CorrespondenceCity
            };


            return user;
        }

        private User AddUserWithoutCorrespondenceAdress()
        {
            PMSContext dbContext = new PMSContext();
            var userRole = dbContext.UserRole.Where(n => n.Name.Equals(_UserRole)).SingleOrDefault();
            int userRoleID = userRole.UserRoleID;
            User user;

            user = new User()
            {
                UserRoleID = userRoleID,
                TeamID = null,
                FirstName = _FirstName,
                LastName = _LastName,
                Login = _Login,
                Password = _Password,
                Salary = _Salary,
                PhoneNumber = _PhoneNumber,
                Email = _Email,
                AccountCreationDate = DateTime.Now,
                HiredDate = _HiredDate,
                FiredDate = _HiredDate.AddYears(5),
                ResidenceStreet = _ResidenceStreet,
                ResidenceHouseNumber = _ResidenceHouseNumber,
                ResidenceApartmentNumber = _ResidenceApartmentNumber,
                ResidenceCity = _ResidenceCity,
                ResidencePostcode = _ResidencePostcode,
                CorrespondenceStreet = _ResidenceStreet,
                CorrespondenceHouseNumber = _ResidenceHouseNumber,
                CorrespondenceApartmentNumber = _ResidenceApartmentNumber,
                CorrespondencePostcode = _ResidencePostcode,
                CorrespondenceCity = _ResidenceCity
            };

            return user;
        }

        public List<UserRole> UsersRole
        {
            get
            {
                PMSContext dbContext = new PMSContext();
                return dbContext.UserRole.ToList();
            }
        }

        /*public List<Team> Teams
        {
            get
            {
                PMSContext dbContext = new PMSContext();
                return dbContext.Team.ToList();
            }
        }*/
    }
}
