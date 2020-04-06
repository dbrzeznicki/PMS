using PMS.DAL;
using PMS.Model;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PMS
{
    public class EditUserViewModel : BindableBase
    {

        public readonly PMSContext dbContext = new PMSContext();
        #region variables

        private ObservableCollection<User> _Users { get; set; }
        private User _mySelectedUser;

        private string _VisibilityTeam = "Visible";
        private bool _CheckBoxAdress = false;
        private bool _IsEnabledEditButton = false;
        private string _FirstName = "";
        private string _LastName = "";
        private string _Password = "";
        private double _Salary;
        private string _PhoneNumber = "";
        private string _Email = "";
        private string _Team;
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
        private DateTime _HiredDate = DateTime.Now;
        private DateTime _FiredDate = DateTime.Now;
        #endregion

        #region command

        public ICommand EditUserButton { get; set; }

        #endregion

        #region constructor

        public EditUserViewModel()
        {
            EditUserButton = new DelegateCommand(EditUser);
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
        public string Team
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
        }
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
        public string VisibilityTeam
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
        }
        public bool CheckBoxAdress
        {
            get
            {
                return _CheckBoxAdress;

                
            }
            set
            {
                _CheckBoxAdress = value;
                RaisePropertyChanged("CheckBoxAdress");
                if (CheckBoxAdress == false)
                {
                    CorrespondenceStreet = ResidenceStreet;
                    CorrespondenceHouseNumber = ResidenceHouseNumber;
                    CorrespondenceApartmentNumber = ResidenceApartmentNumber;
                    CorrespondencePostcode = ResidencePostcode;
                    CorrespondenceCity = ResidenceCity;
                } 
                else
                {
                    CorrespondenceStreet = _CorrespondenceStreet;
                    CorrespondenceHouseNumber = _CorrespondenceHouseNumber;
                    CorrespondenceApartmentNumber = _CorrespondenceApartmentNumber;
                    CorrespondencePostcode = _CorrespondencePostcode;
                    CorrespondenceCity = _CorrespondenceCity;
                }
            }
        }
        public bool IsEnabledEditButton
        {
            get
            {
                return _IsEnabledEditButton;
            }
            set
            {
                _IsEnabledEditButton = value;
                RaisePropertyChanged("IsEnabledEditButton");
            }
        }


        public User MySelectedUser
        {
            get { return _mySelectedUser; }
            set
            {
                //_mySelectedUser was null.
                _mySelectedUser = value;
                RaisePropertyChanged("MySelectedUser");


                IsEnabledEditButton = true;

                FirstName = _mySelectedUser.FirstName;
                LastName = _mySelectedUser.LastName;
                Password = _mySelectedUser.Password;
                Salary = _mySelectedUser.Salary;
                PhoneNumber = _mySelectedUser.PhoneNumber;
                Email = _mySelectedUser.Email;
                if (_mySelectedUser.Team != null)
                {
                    VisibilityTeam = "Visible";
                    Team = _mySelectedUser.Team.Name;
                }
                else
                {
                    VisibilityTeam = "Collapsed";
                    Team = null;
                }

                ResidenceStreet = _mySelectedUser.ResidenceStreet;
                ResidenceHouseNumber = _mySelectedUser.ResidenceHouseNumber;
                ResidenceApartmentNumber = _mySelectedUser.ResidenceApartmentNumber;
                ResidencePostcode = _mySelectedUser.ResidencePostcode;
                ResidenceCity = _mySelectedUser.ResidenceCity;
                if (_mySelectedUser.CorrespondenceStreet == _mySelectedUser.ResidenceStreet)
                {
                    CheckBoxAdress = false;
                }

                else
                {
                    CheckBoxAdress = true;
                }

                CorrespondenceStreet = _mySelectedUser.CorrespondenceStreet;
                CorrespondenceHouseNumber = _mySelectedUser.CorrespondenceHouseNumber;
                CorrespondenceApartmentNumber = _mySelectedUser.CorrespondenceApartmentNumber;
                CorrespondencePostcode = _mySelectedUser.CorrespondencePostcode;
                CorrespondenceCity = _mySelectedUser.CorrespondenceCity;
                HiredDate = _mySelectedUser.HiredDate;
                FiredDate = _mySelectedUser.FiredDate;


            }
        }


        public ObservableCollection<User> Users
        {
            get
            {
                //PMSContext dbContext = new PMSContext();
                if (_Users == null)
                {
                    _Users = new ObservableCollection<User>(dbContext.User);
                }
                return _Users;
            }
            set
            {
                _Users = value;
                RaisePropertyChanged("Users");
            }
        }
        #endregion






        private void EditUser()
        {
            AdminValidation AV = new AdminValidation();
            bool corretForm = AV.EditUserValidation(FirstName, LastName, Password, Salary, PhoneNumber, Email, Team,
                MySelectedUser, FiredDate, HiredDate);

            if (corretForm)
            {
                if (MySelectedUser.UserRole.Name != "Admin")
                {
                    var team = dbContext.Team.Where(n => n.Name.Equals(_Team)).SingleOrDefault();
                    int teamID = team.TeamID;
                    MySelectedUser.TeamID = teamID;
                }

                MySelectedUser.FirstName = FirstName;
                MySelectedUser.LastName = LastName;

                MySelectedUser.Password = Password;

                MySelectedUser.Salary = Salary;
                MySelectedUser.PhoneNumber = PhoneNumber;
                MySelectedUser.Email = Email;
                MySelectedUser.ResidenceStreet = ResidenceStreet;
                MySelectedUser.ResidenceHouseNumber = ResidenceHouseNumber;
                MySelectedUser.ResidenceApartmentNumber = ResidenceApartmentNumber;
                MySelectedUser.ResidencePostcode = ResidencePostcode;
                MySelectedUser.ResidenceCity = ResidenceCity;
                MySelectedUser.CorrespondenceStreet = CorrespondenceStreet;
                MySelectedUser.CorrespondenceHouseNumber = CorrespondenceHouseNumber;
                MySelectedUser.CorrespondenceApartmentNumber = CorrespondenceApartmentNumber;
                MySelectedUser.CorrespondencePostcode = CorrespondencePostcode;
                MySelectedUser.CorrespondenceCity = CorrespondenceCity;
                MySelectedUser.HiredDate = HiredDate;
                MySelectedUser.FiredDate = FiredDate;

                dbContext.SaveChanges();


                Users = new ObservableCollection<User>(dbContext.User);

                ErrorMessage er = new ErrorMessage("User edit successfully!");
                er.ShowDialog();
            }
        }

        public List<UserRole> UsersRole
        {
            get
            {
                return dbContext.UserRole.ToList();
            }
        }

        public List<Team> Teams
        {
            get
            {
                return dbContext.Team.ToList();
            }
        }
    }
}
