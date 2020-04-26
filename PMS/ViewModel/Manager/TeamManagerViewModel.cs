using PMS.DAL;
using PMS.Model;
using PMS.ViewModel;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS
{
    public class TeamManagerViewModel : BindableBase
    {

        #region variable
        private readonly PMSContext dbContext = new PMSContext();

        private ObservableCollection<User> _UsersList;
        private ObservableCollection<Subtask> _SubtaskSelectedUserList;

        private ObservableCollection<User> _Users;
        private User _SelectedUsers = null;
        #endregion


        #region command

        #endregion

        #region conctructor

        #endregion

        #region properties
        public ObservableCollection<User> UsersList
        {
            get
            {
                if (_UsersList == null)
                {
                    _UsersList = new ObservableCollection<User>(dbContext.User.Where(x=>x.TeamID == Global.user.TeamID));
                }
                return _UsersList;
            }
            set
            {
                _UsersList = value;
                RaisePropertyChanged("UsersList");
            }
        }

        public ObservableCollection<User> Users
        {
            get
            {
                if (_Users == null)
                {
                    _Users = new ObservableCollection<User>(dbContext.User.Where(x => x.TeamID == Global.user.TeamID));
                    SelectedUsers = Users[0];
                }
                return _Users;
            }
            set
            {
                _Users = value;
                RaisePropertyChanged("Users");
            }
        }

        public User SelectedUsers
        {
            get
            {
                return _SelectedUsers;
            }
            set
            {
                _SelectedUsers = value;
                SubtaskSelectedUserList = new ObservableCollection<Subtask>(dbContext.Subtask.Where(x => (x.UserID == SelectedUsers.UserID) &&
                    (x.SubtaskStatus.Name == "New" || x.SubtaskStatus.Name == "In progress")));
                RaisePropertyChanged("SelectedUsers");
            }
        }

        public ObservableCollection<Subtask> SubtaskSelectedUserList
        {
            get
            {
                if (_SubtaskSelectedUserList == null)
                    _SubtaskSelectedUserList = new ObservableCollection<Subtask>(dbContext.Subtask.Where(x=> (x.UserID == SelectedUsers.UserID) && 
                    (x.SubtaskStatus.Name == "New" || x.SubtaskStatus.Name == "In progress")));

                return _SubtaskSelectedUserList;
            }
            set
            {
                _SubtaskSelectedUserList = value;
                RaisePropertyChanged("SubtaskSelectedUserList");
            }
        }
        #endregion

        #region method

        #endregion
    }
}
