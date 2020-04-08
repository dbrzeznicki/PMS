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

        private string _Name = "";

        private ObservableCollection<User> _UserWithoutTeam;
        private User _SelectedUserWithoutTeam = null;
        bool _IsEnabledAddUserToTeamButton = true;
        bool _IsEnabledAddTeamButton = false;

        private ObservableCollection<User> _UserInNewTeam;

        #endregion



        #region command

        public ICommand AddUserToTeamButton { get; set; }
        public ICommand RemoveUserWithTeamButton { get; set; }
        public ICommand CreateTeamButton { get; set; }

        #endregion


        #region constructor

        public TeamAdminViewModel()
        {
            CreateTeamButton = new DelegateCommand(CreateTeam);
            AddUserToTeamButton = new DelegateCommand(AddUserToTeam);
            RemoveUserWithTeamButton = new DelegateCommand<object>(RemoveUserWithTeam);
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

        #endregion



        #region Methods

        private void CreateTeam()
        {

            AdminValidation AV = new AdminValidation();
            bool correctForm = AV.AddTeamValidation(_Name, _UserInNewTeam);

            if (correctForm)
            {
                Team team = new Team
                {
                    Name = _Name,
                    NumberOfPeople = _UserInNewTeam.Count(),
                    NumberOfProjects = 0,
                    Users = _UserInNewTeam
                };

                dbContext.Team.Add(team);
                dbContext.SaveChanges();

                _UserWithoutTeam = new ObservableCollection<User>(dbContext.User.Where(x => x.TeamID == null && x.UserRole.Name != "Admin"));
                _UserInNewTeam = new ObservableCollection<User>();
                _Name = "";
                _IsEnabledAddTeamButton = false;

                RaisePropertyChanged("UserInNewTeam");
                RaisePropertyChanged("UserWithoutTeam");
                RaisePropertyChanged("Name");
                RaisePropertyChanged("IsEnabledAddTeamButton");

                ErrorMessage er = new ErrorMessage("Taam created successfully!");
                er.ShowDialog();
            }
        }


        private void AddUserToTeam()
        {
            _UserInNewTeam.Add(_SelectedUserWithoutTeam);
            _UserWithoutTeam.Remove(_SelectedUserWithoutTeam);

            if (_UserInNewTeam.Count() > 0)
                _IsEnabledAddTeamButton = true;
            else
                _IsEnabledAddTeamButton = false;
            //selectec = null


            RaisePropertyChanged("UserInNewTeam");
            RaisePropertyChanged("UserWithoutTeam");
            RaisePropertyChanged("IsEnabledAddTeamButton");

        }

        private void RemoveUserWithTeam(object ID)
        {
            int val = Convert.ToInt32(ID);
            User user = dbContext.User.Find(val);

            _UserInNewTeam.Remove(user);
            _UserWithoutTeam.Add(user);

            if (_UserInNewTeam.Count() > 0)
                _IsEnabledAddTeamButton = true;
            else
                _IsEnabledAddTeamButton = false;

            RaisePropertyChanged("UserInNewTeam");
            RaisePropertyChanged("UserWithoutTeam");
            RaisePropertyChanged("IsEnabledAddTeamButton");
        }

        #endregion

    }
}
