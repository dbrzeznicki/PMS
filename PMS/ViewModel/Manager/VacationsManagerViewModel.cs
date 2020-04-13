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
    public class VacationsManagerViewModel : BindableBase
    {
        #region variable

        PMSContext dbContext = new PMSContext();

        private ObservableCollection<User> _UserInTeam;
        private User _SelectedUserInTeam = null;

        private DateTime _StartVacation = DateTime.Now;
        private DateTime _EndVacation = DateTime.Now;

        private ObservableCollection<VacationType> _ListOfVacationTypes;
        private VacationType _SelectedVacationType = null;

        private bool _IsEnabledAddButton = false;

        #endregion


        #region command

        //dodanie
        public ICommand CreateVacationButton { get; set; }

        #endregion


        #region constructor

        public VacationsManagerViewModel()
        {
            CreateVacationButton = new DelegateCommand(CreateVacation);
        }

        #endregion

        #region properties

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

                RaisePropertyChanged("IsEnabledAddButton");
                RaisePropertyChanged("SelectedUserInTeam");
            }
        }


        public DateTime StartVacation
        {
            get
            {
                return _StartVacation;
            }
            set
            {
                _StartVacation = value;
                RaisePropertyChanged("StartVacation");
            }
        }

        public DateTime EndVacation
        {
            get
            {
                return _EndVacation;
            }
            set
            {
                _EndVacation = value;
                RaisePropertyChanged("EndVacation");
            }
        }

        public ObservableCollection<VacationType> ListOfVacationTypes
        {
            get
            {
                if (_ListOfVacationTypes == null)
                {
                    _ListOfVacationTypes = new ObservableCollection<VacationType>(dbContext.VacationType.ToList());
                }
                return _ListOfVacationTypes;
            }
            set
            {
                _ListOfVacationTypes = value;
                RaisePropertyChanged("ListOfVacationTypes");
            }
        }

        public VacationType SelectedVacationType
        {
            get
            {
                return _SelectedVacationType;
            }
            set
            {
                _SelectedVacationType = value;

                RaisePropertyChanged("IsEnabledAddButton");
                RaisePropertyChanged("SelectedVacationType");
            }
        }


        public bool IsEnabledAddButton
        {
            get
            {
                if (SelectedUserInTeam == null || SelectedVacationType == null)
                    _IsEnabledAddButton = false;
                else
                    _IsEnabledAddButton = true;

                return _IsEnabledAddButton;
            }
            set
            {
                _IsEnabledAddButton = value;
                RaisePropertyChanged("IsEnabledAddButton");
            }
        }
        #endregion


        private void CreateVacation()
        {
            ManagerValidation MV = new ManagerValidation();
            bool correctForm = MV.AddVacationValidation(StartVacation, EndVacation);

            if (correctForm)
            {

                addCaVacation();
                setVariableWhenAddVacation();

                ErrorMessage er = new ErrorMessage("Vacation added successfully!");
                er.ShowDialog();
            }
        }


        private void addCaVacation()
        {
            Vacation vacation = new Vacation
            {
                EndVacation = EndVacation,
                StartVacation = StartVacation,
                VacationType = SelectedVacationType,
                User = SelectedUserInTeam
            };

            dbContext.Vacation.Add(vacation);
            dbContext.SaveChanges();
        }

        private void setVariableWhenAddVacation()
        {
            SelectedVacationType = null;
            SelectedUserInTeam = null;
            StartVacation = DateTime.Now;
            EndVacation = DateTime.Now;
            EndVacation = EndVacation.AddDays(1);
            RaisePropertyChanged("IsEnabledAddButton");
        }
    }
}
