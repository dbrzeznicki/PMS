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
    public class UsersListViewModel : BindableBase
    {

        private readonly PMSContext dbContext = new PMSContext();

        //Users list
        private ObservableCollection<User> _Users { get; set; }

        //Filtered
        private ObservableCollection<User> _FilteredUsers { get; set; }
        private ObservableCollection<string> _FilteredUsersRole { get; set; }
        private string _SelectedUserRole { get; set; }
        private string _SelectedName { get; set; }

        public ICommand RemoveUserButton { get; set; }
        public ICommand AddAdressButton { get; set; }
        
        public UsersListViewModel()
        {
            RemoveUserButton = new DelegateCommand<object>(RemoveUser);
        }

        public ObservableCollection<User> Users
        {
            get
            {
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

        public ObservableCollection<User> FilteredUsers
        {
            get
            {
                if (_FilteredUsers == null)
                {
                    _FilteredUsers = new ObservableCollection<User>(dbContext.User);
                }
                return _FilteredUsers;
            }
            set
            {
                _FilteredUsers = value;
                RaisePropertyChanged("FilteredUsers");
            }
        }

        public ObservableCollection<string> FilteredUsersRole
        {
            get
            {
                _FilteredUsersRole = new ObservableCollection<string>(dbContext.UserRole.Select(c => c.Name).Distinct());
                _FilteredUsersRole.Insert(0, "All");
                _SelectedUserRole = "All";
                return _FilteredUsersRole;
            }
        }

        public string SelectedUserRole
        {
            get
            {
                return _SelectedUserRole;
            }
            set
            {
                _SelectedUserRole = value;
                RaisePropertyChanged("SelectedUserRole");
                FilterUser();
            }
        }

        public string SelectedName
        {
            get
            {
                return _SelectedName;
            }
            set
            {
                _SelectedName = value;
                RaisePropertyChanged("SelectedName");
                FilterUser();
            }
        }

        public List<UserRole> UsersRole
        {
            get
            {
                return dbContext.UserRole.ToList();
            }
        }

        private void RemoveUser(object ID)
        {
            int val = Convert.ToInt32(ID);
            User user = dbContext.User.Find(val);
            dbContext.User.Remove(user);
            dbContext.SaveChanges();
            _Users.Remove(user);
            _FilteredUsers.Remove(user);

        }

        private void FilterUser()
        {
            if (SelectedName == null)
                SelectedName = "";

            FilteredUsers = new ObservableCollection<User>(Users.Where(c => (SelectedUserRole == "All" ? true : c.UserRole.Name == SelectedUserRole)
            && c.FirstName.Contains(SelectedName)));
        }
    }
}
