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
    public class TeamAdminViewModel : BindableBase
    {

        #region variable

        PMSContext dbContext = new PMSContext();

        //Dodanie
        private string _Name = "";

        private ObservableCollection<User> _UserWithoutTeam;
        private User _SelectedUserWithoutTeam = null;
        bool _IsEnabledAddUserToTeamButton = true;
        bool _IsEnabledAddTeamButton = false;

        private ObservableCollection<User> _UserInNewTeam;


        //Edycja
        private string _EditName = "";

        private ObservableCollection<User> _EditUserWithoutTeam;
        private User _EditSelectedUserWithoutTeam = null;
        bool _EditIsEnabledAddUserToTeamButton = true;
        bool _EditIsEnabledAddTeamButton = false;

        private ObservableCollection<User> _EditUserInNewTeam;


        private ObservableCollection<Team> _EditTeamsList;
        private Team _EditSelectedTeam = null;
        #endregion



        #region command

        //dodanie
        public ICommand AddUserToTeamButton { get; set; }
        public ICommand RemoveUserWithTeamButton { get; set; }
        public ICommand CreateTeamButton { get; set; }

        //edycja
        public ICommand EditAddUserToTeamButton { get; set; }
        public ICommand EditRemoveUserWithTeamButton { get; set; }
        public ICommand EditCreateTeamButton { get; set; }

        #endregion


        #region constructor

        public TeamAdminViewModel()
        {
            CreateTeamButton = new DelegateCommand(CreateTeam);
            AddUserToTeamButton = new DelegateCommand(AddUserToTeam);
            RemoveUserWithTeamButton = new DelegateCommand<object>(RemoveUserWithTeam);

            EditCreateTeamButton = new DelegateCommand(EditCreateTeam);
            EditAddUserToTeamButton = new DelegateCommand(EditAddUserToTeam);
            EditRemoveUserWithTeamButton = new DelegateCommand<object>(EditRemoveUserWithTeam);
        }

        #endregion


        #region properties

        public string Name
        {
            get
            {
                return _Name;
            }
            set
            {
                _Name = value;
                RaisePropertyChanged("Name");
            }
        }

        public ObservableCollection<User> UserWithoutTeam
        {
            get
            {

                if (_UserWithoutTeam == null)
                {
                    _UserWithoutTeam = new ObservableCollection<User>(dbContext.User.Where(x => x.TeamID == null && x.UserRole.Name != "Admin"));
                }
                return _UserWithoutTeam;
            }
            set
            {
                _UserWithoutTeam = value;
                RaisePropertyChanged("UserWithoutTeam");
            }
        }

        public User SelectedUserWithoutTeam
        {
            get
            {
                return _SelectedUserWithoutTeam;
            }
            set
            {
                _SelectedUserWithoutTeam = value;

                RaisePropertyChanged("IsEnabledAddUserToTeamButton");
                RaisePropertyChanged("SelectedUserWithoutTeam");
            }
        }

        public ObservableCollection<User> UserInNewTeam
        {
            get
            {
                if (_UserInNewTeam == null)
                {
                    _UserInNewTeam = new ObservableCollection<User>();
                }
                return _UserInNewTeam;
            }
            set
            {
                _UserInNewTeam = value;
                RaisePropertyChanged("UserInNewTeam");
            }
        }

        public bool IsEnabledAddUserToTeamButton
        {
            get
            {
                if (_UserWithoutTeam.Count() <= 0 || _SelectedUserWithoutTeam == null)
                    _IsEnabledAddUserToTeamButton = false;
                else
                    _IsEnabledAddUserToTeamButton = true;
                return _IsEnabledAddUserToTeamButton;
            }
            set
            {
                _IsEnabledAddUserToTeamButton = value;
                RaisePropertyChanged("IsEnabledAddUserToTeamButton");
            }
        }

        public bool IsEnabledAddTeamButton
        {
            get
            {
                if (_UserInNewTeam.Count() > 0)
                    _IsEnabledAddTeamButton = true;
                else
                    _IsEnabledAddTeamButton = false;
                return _IsEnabledAddTeamButton;
            }
            set
            {
                _IsEnabledAddTeamButton = value;
                RaisePropertyChanged("IsEnabledAddTeamButton");
            }
        }

        public string EditName
        {
            get
            {
                return _EditName;
            }
            set
            {
                _EditName = value;
                RaisePropertyChanged("EditName");
            }
        }

        public ObservableCollection<User> EditUserWithoutTeam
        {
            get
            {

                if (_EditUserWithoutTeam == null)
                {
                    _EditUserWithoutTeam = new ObservableCollection<User>(dbContext.User.Where(x => x.TeamID == null && x.UserRole.Name != "Admin"));
                }
                return _EditUserWithoutTeam;
            }
            set
            {
                _EditUserWithoutTeam = value;
                RaisePropertyChanged("EditUserWithoutTeam");
            }
        }

        public User EditSelectedUserWithoutTeam
        {
            get
            {
                return _EditSelectedUserWithoutTeam;
            }
            set
            {
                _EditSelectedUserWithoutTeam = value;

                RaisePropertyChanged("EditIsEnabledAddUserToTeamButton");
                RaisePropertyChanged("EditSelectedUserWithoutTeam");
            }
        }

        public ObservableCollection<User> EditUserInNewTeam
        {
            get
            {//cos zmieniane 

                if (_EditUserInNewTeam == null)
                {
                    _EditUserInNewTeam = new ObservableCollection<User>();
                }

                return _EditUserInNewTeam;
            }
            set
            {
                _EditUserInNewTeam = value;
                RaisePropertyChanged("EditUserInNewTeam");
            }
        }


        public bool EditIsEnabledAddUserToTeamButton
        {
            get
            {
                if (_EditUserWithoutTeam.Count() <= 0 || _EditSelectedUserWithoutTeam == null || _EditSelectedTeam == null)
                    _EditIsEnabledAddUserToTeamButton = false;
                else
                    _EditIsEnabledAddUserToTeamButton = true;
                return _EditIsEnabledAddUserToTeamButton;
            }
            set
            {
                _EditIsEnabledAddUserToTeamButton = value;
                RaisePropertyChanged("EditIsEnabledAddUserToTeamButton");
            }
        }


        public bool EditIsEnabledAddTeamButton
        {
            get
            {
                if (_EditUserInNewTeam.Count() > 0)
                    _EditIsEnabledAddTeamButton = true;
                else
                    _EditIsEnabledAddTeamButton = false;
                return _EditIsEnabledAddTeamButton;
            }
            set
            {
                _EditIsEnabledAddTeamButton = value;
                RaisePropertyChanged("EditIsEnabledAddTeamButton");
            }
        }

        public ObservableCollection<Team> EditTeamsList
        {
            get
            {

                if (_EditTeamsList == null)
                {
                    _EditTeamsList = new ObservableCollection<Team>(dbContext.Team.ToList());
                }
                return _EditTeamsList;
            }
            set
            {
                _EditTeamsList = value;
                RaisePropertyChanged("EditTeamsList");
            }
        }

        public Team EditSelectedTeam
        {
            get
            {
                return _EditSelectedTeam;
            }
            set
            {
                _EditSelectedTeam = value;

                if (EditSelectedTeam != null)
                {
                    EditName = EditSelectedTeam.Name;
                    EditUserInNewTeam = new ObservableCollection<User>(EditSelectedTeam.Users.ToList());
                }
                
                EditUserWithoutTeam = new ObservableCollection<User>(dbContext.User.Where(x => x.TeamID == null && x.UserRole.Name != "Admin"));

                if (EditUserInNewTeam.Count() > 0)
                    EditIsEnabledAddTeamButton = true;
                else
                    EditIsEnabledAddTeamButton = false;

                RaisePropertyChanged("EditSelectedTeam");
                RaisePropertyChanged("EditIsEnabledAddUserToTeamButton");
            }
        }
        #endregion



        #region Methods

        private void AddUserToTeam()
        {
            UserInNewTeam.Add(SelectedUserWithoutTeam);
            UserWithoutTeam.Remove(SelectedUserWithoutTeam);
            RaisePropertyChanged("IsEnabledAddTeamButton");
        }

        private void EditAddUserToTeam()
        {
            EditUserInNewTeam.Add(EditSelectedUserWithoutTeam);
            EditUserWithoutTeam.Remove(EditSelectedUserWithoutTeam);
            RaisePropertyChanged("EditIsEnabledAddTeamButton");
        }


        private void RemoveUserWithTeam(object ID)
        {
            int val = Convert.ToInt32(ID);
            User user = dbContext.User.Find(val);

            UserInNewTeam.Remove(user);
            UserWithoutTeam.Add(user);

            RaisePropertyChanged("IsEnabledAddTeamButton");
        }


        private void EditRemoveUserWithTeam(object ID)
        {

            int val = Convert.ToInt32(ID);
            User user = dbContext.User.Find(val);

            AdminValidation AV = new AdminValidation();
            bool correctForm = AV.EditTeamRemoveUserValidation(user);
            if (correctForm)
            {
                EditUserInNewTeam.Remove(user);
                EditUserWithoutTeam.Add(user);

                RaisePropertyChanged("EditIsEnabledAddTeamButton");
            }
        }







        private void EditCreateTeam()
        {

            AdminValidation AV = new AdminValidation();
            bool correctForm = AV.EditTeamValidation(EditName, EditUserInNewTeam);

            if (correctForm)
            {

                editTeam();
                setVariableWhenEditTeam();
                setVariableWhenAddNewTeam();

                ErrorMessage er = new ErrorMessage("Team edit successfully!");
                er.ShowDialog();
            }
        }


        private void CreateTeam()
        {
            AdminValidation AV = new AdminValidation();
            bool correctForm = AV.EditTeamValidation(Name, UserInNewTeam);

            if (correctForm)
            {

                addTeam();
                setVariableWhenAddNewTeam();
                setVariableWhenEditTeam();

                ErrorMessage er = new ErrorMessage("Taam created successfully!");
                er.ShowDialog();
            }
        }

        #endregion

        #region helpMethods

        private void addTeam()
        {
            Team team = new Team
            {
                Name = Name,
                NumberOfPeople = UserInNewTeam.Count(),
                NumberOfProjects = 0,
                Users = UserInNewTeam
            };

            dbContext.Team.Add(team);
            dbContext.SaveChanges();
        }

        private void editTeam()
        {
            EditSelectedTeam.Name = EditName;
            EditSelectedTeam.NumberOfPeople = EditUserInNewTeam.Count();
            EditSelectedTeam.Users = EditUserInNewTeam;

            dbContext.SaveChanges();
        }

        private void setVariableWhenAddNewTeam()
        {
            UserWithoutTeam = new ObservableCollection<User>(dbContext.User.Where(x => x.TeamID == null && x.UserRole.Name != "Admin"));
            UserInNewTeam = new ObservableCollection<User>();
            SelectedUserWithoutTeam = null;
            Name = "";
            RaisePropertyChanged("IsEnabledAddTeamButton");
        }

        private void setVariableWhenEditTeam()
        {
            EditUserWithoutTeam = new ObservableCollection<User>(dbContext.User.Where(x => x.TeamID == null && x.UserRole.Name != "Admin"));
            EditUserInNewTeam = new ObservableCollection<User>();
            EditName = "";
            EditSelectedTeam = null;
            EditTeamsList = new ObservableCollection<Team>(dbContext.Team.ToList());
            EditSelectedUserWithoutTeam = null;
            RaisePropertyChanged("EditIsEnabledAddTeamButton");
        }
        #endregion
    }
}
