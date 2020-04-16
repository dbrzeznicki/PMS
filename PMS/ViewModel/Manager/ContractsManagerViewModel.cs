using NPOI.XWPF.UserModel;
using PMS.DAL;
using PMS.Model;
using PMS.ViewModel;
using Prism.Commands;
using Prism.Mvvm;
using Spire.Xls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
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

        private ObservableCollection<User> _UserInTeamPayout;
        private User _SelectedUserInTeamPayout = null;
        private List<string> _Months;
        private string _SelectedMonth = "January";
        private DateTime _GrantedBonus = DateTime.Now;
        private double _BonusAmount = 0;
        private bool _IsEnabledAddBonusButton = false;

        #endregion


        #region command

        public ICommand GenerateContractButton { get; set; }
        public ICommand AddBonusButton { get; set; }
        public ICommand GeneratePayoutsListButton { get; set; }

        #endregion


        #region constructor

        public ContractsManagerViewModel()
        {
            GenerateContractButton = new DelegateCommand(GenerateContract);
            AddBonusButton = new DelegateCommand(AddBonus);
            GeneratePayoutsListButton = new DelegateCommand(GeneratePayoutsList);
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


        public ObservableCollection<User> UserInTeamPayout
        {
            get
            {
                if (_UserInTeamPayout == null)
                {
                    _UserInTeamPayout = new ObservableCollection<User>(dbContext.User.Where(x => x.TeamID == Global.user.TeamID));
                }
                return _UserInTeamPayout;
            }
            set
            {
                _UserInTeamPayout = value;
                RaisePropertyChanged("UserInTeamPayout");
            }
        }

        public User SelectedUserInTeamPayout
        {
            get
            {
                return _SelectedUserInTeamPayout;
            }
            set
            {
                _SelectedUserInTeamPayout = value;

                RaisePropertyChanged("SelectedUserInTeamPayout");
                RaisePropertyChanged("IsEnabledAddBonusButton");
            }
        }

        public List<string> Months
        {
            get
            {
                _Months = new List<string>() { "January", "February", "March", "April", "May", "June", "July", "August", "Semptember", "October", "November", "December" };
                _SelectedMonth = "January";
                return _Months;
            }
            set
            {
                _Months = value;
                RaisePropertyChanged("Months");
            }
        }

        public string SelectedMonth
        {
            get
            {
                return _SelectedMonth;
            }
            set
            {
                _SelectedMonth = value;
                RaisePropertyChanged("SelectedMonth");
            }
        }

        public double BonusAmount
        {
            get
            {
                return _BonusAmount;
            }
            set
            {
                _BonusAmount = value;
                RaisePropertyChanged("BonusAmount");
            }
        }

        public DateTime GrantedBonus
        {
            get
            {
                return _GrantedBonus;
            }
            set
            {
                _GrantedBonus = value;
                RaisePropertyChanged("GrantedBonus");
            }
        }

        public bool IsEnabledAddBonusButton
        {
            get
            {
                if (SelectedUserInTeamPayout != null )
                    _IsEnabledAddBonusButton = true;
                else
                    _IsEnabledAddBonusButton = false;

                return _IsEnabledAddBonusButton;
            }
            set
            {
                _IsEnabledAddBonusButton = value;
                RaisePropertyChanged("IsEnabledAddBonusButton");
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
            CreateWordContract();


            ErrorMessage er = new ErrorMessage("Generate contract successfully!");
            er.ShowDialog();
        }


        private void CreateWordContract()
        {
            System.Diagnostics.Process.Start("UMOWA_WZOR.docx");
        }

        private void GeneratePayoutsList()
        {
            
            Workbook workbook = new Workbook();
            Worksheet sheet = workbook.Worksheets[0];
            
            sheet.Name = "Payout bonus";

            sheet.Range["A1"].Text = "User";
            sheet.Range["B1"].Text = "Salary";
            sheet.Range["C1"].Text = "Payout bonus";
            sheet.Range["D1"].Text = "Salary + Payout bonus";

            for (int i = 0; i < UserInTeamPayout.Count; i++)
            {
                double bonus = 0;
                int cell = 2 + i;

                sheet.Range["A" + cell].Text = UserInTeamPayout[i].FullName;
                sheet.Range["B" + cell].Text = UserInTeamPayout[i].Salary.ToString();


                User user = dbContext.User.Find(UserInTeamPayout[i].UserID);

                List<PayoutBonus> pbtmp = new List<PayoutBonus>(dbContext.PayoutBonus.Where(x => x.UserID == user.UserID));

                List<PayoutBonus> pb = new List<PayoutBonus>();
                
                if (pbtmp.Count > 0)
                    foreach(var tmp in pbtmp)
                    {
                        string monthString = tmp.DateOfGrantiedBonuses.ToString("MMMM");
                        if (monthString == SelectedMonth && tmp.DateOfGrantiedBonuses.Year == DateTime.Now.Year)
                        {
                            pb.Add(tmp);
                        }
                    }


                if (pb.Count == 0)
                    sheet.Range["C" + cell].Text = "0";
                else
                {
                    bonus = 0;
                    foreach (var tmp in pb)
                    {
                        bonus += tmp.Value;
                    }
                    sheet.Range["C" + cell].Text = bonus.ToString();
                }

                double BandD = UserInTeamPayout[i].Salary + bonus;
                sheet.Range["D" + cell].Text = BandD.ToString();
            }


            //sheet.Range["A7"].NumberValue = 3.1415926;



            string title = DateTime.Now.ToString("MMMMddHHmmss") + ".xlsx";
            workbook.SaveToFile(title, ExcelVersion.Version2013);
            System.Diagnostics.Process.Start(title);

        }

        private void AddBonus()
        {
            ManagerValidation MV = new ManagerValidation();
            bool correctForm = MV.BonusAmountValidation(BonusAmount);

            if (correctForm)
            {
                PayoutBonus payoutBonus = new PayoutBonus()
                {
                    DateOfGrantiedBonuses = GrantedBonus,
                    Value = BonusAmount,
                    User = SelectedUserInTeamPayout
                };

                dbContext.PayoutBonus.Add(payoutBonus);
                dbContext.SaveChanges();

                GrantedBonus = DateTime.Now;
                BonusAmount = 0;

                ErrorMessage er = new ErrorMessage("Add payout bonus!");
                er.ShowDialog();
            }

        }
    }
}
