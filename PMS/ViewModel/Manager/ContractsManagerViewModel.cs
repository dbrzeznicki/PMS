using PMS.DAL;
using PMS.Model;
using PMS.ViewModel;
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
    public class ContractsManagerViewModel : BindableBase
    {


        #region variable

        private readonly PMSContext dbContext = new PMSContext();
        private ObservableCollection<User> _ListOfUsers;

        private ObservableCollection<User> _UserInTeam;
        private User _SelectedUserInTeam = null;
        private DateTime _FiredDate = DateTime.Now;
        private bool _IsEnabledGenerateContractButton = false;

        #endregion


        #region command

        public ICommand GenerateContractButton { get; set; }

        #endregion


        #region constructor

        public ContractsManagerViewModel()
        {
            GenerateContractButton = new DelegateCommand(GenerateContract);
        }

        #endregion


        #region properties

        public ObservableCollection<User> ListOfUsers
        {
            get
            {

                if (_ListOfUsers == null)
                {
                    _ListOfUsers = new ObservableCollection<User>(dbContext.User.Where(c => c.TeamID == Global.user.TeamID).OrderBy(x => x.FiredDate));
                }
                return _ListOfUsers;
            }
            set
            {
                _ListOfUsers = value;
                RaisePropertyChanged("ListOfUsers");
            }
        }

        public ObservableCollection<User> UserInTeam
        {
            get
            {
                if (_UserInTeam == null)
                {
                    _UserInTeam = new ObservableCollection<User>(dbContext.User.Where(x => x.TeamID == Global.user.TeamID));
                }
                return _UserInTeam;
            }
            set
            {
                _UserInTeam = value;
                RaisePropertyChanged("UserInTeam");
            }
        }

        public User SelectedUserInTeam
        {
            get
            {
                return _SelectedUserInTeam;
            }
            set
            {
                _SelectedUserInTeam = value;

                if (_SelectedUserInTeam != null)
                    FiredDate = _SelectedUserInTeam.FiredDate;
                RaisePropertyChanged("SelectedUserInTeam");
                RaisePropertyChanged("IsEnabledGenerateContractButton");
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
                RaisePropertyChanged("IsEnabledGenerateContractButton");
            }
        }


        public bool IsEnabledGenerateContractButton
        {
            get
            {
                if (SelectedUserInTeam != null && FiredDate.CompareTo(SelectedUserInTeam.FiredDate) > 0)
                    _IsEnabledGenerateContractButton = true;
                else
                    _IsEnabledGenerateContractButton = false;

                return _IsEnabledGenerateContractButton;
            }
            set
            {
                _IsEnabledGenerateContractButton = value;
                RaisePropertyChanged("IsEnabledGenerateContractButton");
            }
        }


        #endregion


        private void GenerateContract()
        {

            SelectedUserInTeam.FiredDate = FiredDate;

            dbContext.SaveChanges();

            ListOfUsers = new ObservableCollection<User>(dbContext.User.Where(c => c.TeamID == Global.user.TeamID).OrderBy(x => x.FiredDate));

            SelectedUserInTeam = null;
            FiredDate = DateTime.Now;
            //tutaj obsługe worda - tworzenie nowej umowy

            ErrorMessage er = new ErrorMessage("Generate contract successfully!");
            er.ShowDialog();
        }
    }
}
