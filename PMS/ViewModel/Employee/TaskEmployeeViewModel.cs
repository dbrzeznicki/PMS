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
    public class TaskEmployeeViewModel : BindableBase
    {

        #region variable

        PMSContext dbContext = new PMSContext();


        //Add task
        private string _Name = "";
        private string _Description = "";
        private User _SelectedUser;
        public DateTime _EndTime = DateTime.Now;

        private ObservableCollection<string> _Priority;
        private string _SelectedPriority = "Very low";

        private bool _IsEnabledAddButton = false;


        //moje zadania
        private ObservableCollection<string> _Projects;
        private string _SelectedProject;
        private ObservableCollection<string> _Statuss;
        private string _SelectedStatus;
        private ObservableCollection<Subtask> _Subtasks { get; set; }
        private Subtask _SelectedSubtask;
        private bool _IsEnabledChangeStatusButton = false;

        private ObservableCollection<SubtaskStatus> _SubtaskStatus;
        private SubtaskStatus _SelectedSubtaskStatus;

        private string _SelectedName { get; set; }
        //zlecone przeze mnie
        private ObservableCollection<string> _StatusWhichICreated;
        private string _SelectedStatusWhichICreated;
        private ObservableCollection<Subtask> _SubtasksWhichICreated { get; set; }

        #endregion


        #region command

        public ICommand AddTaskButton { get; set; }
        public ICommand ChangeStatusButton { get; set; }

        #endregion


        #region constructor

        public TaskEmployeeViewModel()
        {
            AddTaskButton = new DelegateCommand(AddTask);
            ChangeStatusButton = new DelegateCommand(ChangeStatus);
        }

        #endregion

        #region properties

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
                FilterSubtask();
            }
        }

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
        public string Description
        {
            get
            {
                return _Description;
            }
            set
            {
                _Description = value;
                RaisePropertyChanged("Description");
            }
        }

        public User SelectedUser
        {
            get { return _SelectedUser; }
            set
            {
                _SelectedUser = value;
                RaisePropertyChanged("SelectedUser");

                IsEnabledAddButton = true;

            }
        }
        public DateTime EndTime
        {
            get
            {
                return _EndTime;
            }
            set
            {
                _EndTime = value;
                RaisePropertyChanged("EndTime");
            }
        }

        public ObservableCollection<string> Priority
        {
            get
            {
                PMSContext dbContext = new PMSContext();
                _Priority = new ObservableCollection<string>();
                _Priority.Insert(0, "Very low");
                _Priority.Insert(1, "Low");
                _Priority.Insert(2, "Medium");
                _Priority.Insert(3, "High");
                _Priority.Insert(4, "Very high");
                _SelectedPriority = "Very low";
                return _Priority;
            }
        }

        public string SelectedPriority
        {
            get
            {
                return _SelectedPriority;
            }
            set
            {
                _SelectedPriority = value;
                RaisePropertyChanged("SelectedPriority");
            }
        }

        public bool IsEnabledAddButton
        {
            get
            {
                return _IsEnabledAddButton;
            }
            set
            {
                _IsEnabledAddButton = value;
                RaisePropertyChanged("IsEnabledAddButton");
            }
        }


        public ObservableCollection<string> Projects
        {
            get
            {
                PMSContext dbContext = new PMSContext();
                _Projects = new ObservableCollection<string>(dbContext.Project.Where(c => c.TeamID == Global.user.TeamID).Select(c => c.Name).Distinct());
                _Projects.Insert(0, "All");
                _Projects.Insert(1, "Other");
                _SelectedProject = "All";
                return _Projects;
            }
        }

        public string SelectedProject
        {
            get
            {
                return _SelectedProject;
            }
            set
            {
                _SelectedProject = value;
                RaisePropertyChanged("SelectedProject");
                FilterSubtask();
            }
        }

        public ObservableCollection<string> Statuss
        {
            get
            {
                PMSContext dbContext = new PMSContext();
                _Statuss = new ObservableCollection<string>(dbContext.SubtaskStatus.Select(c => c.Name));
                _Statuss.Insert(0, "All");
                _SelectedStatus = "All";
                return _Statuss;
            }
        }

        public string SelectedStatus
        {
            get
            {
                return _SelectedStatus;
            }
            set
            {
                _SelectedStatus = value;
                RaisePropertyChanged("SelectedStatus");
                FilterSubtask();
            }
        }

        public ObservableCollection<Subtask> Subtasks
        {
            get
            {
                
                if (_Subtasks == null)
                {
                    _Subtasks = new ObservableCollection<Subtask>(dbContext.Subtask.Where(x => x.UserID == Global.user.UserID));
                }
                return _Subtasks;
            }
            set
            {
                _Subtasks = value;
                RaisePropertyChanged("Subtasks");
            }
        }


        public Subtask SelectedSubtask
        {
            get
            {
                return _SelectedSubtask;
            }
            set
            {
                _SelectedSubtask = value;
                RaisePropertyChanged("SelectedSubtask");

                if(SelectedSubtask != null && SelectedSubtaskStatus != null) 
                {
                    IsEnabledChangeStatusButton = true;
                } else
                {
                    IsEnabledChangeStatusButton = false;
                }
                
            }
        }

        public bool IsEnabledChangeStatusButton
        {
            get
            {
                return _IsEnabledChangeStatusButton;
            }
            set
            {
                _IsEnabledChangeStatusButton = value;
                RaisePropertyChanged("IsEnabledChangeStatusButton");
            }
        }


        public ObservableCollection<SubtaskStatus> SubtaskStatus
        {
            get
            {
                //PMSContext dbContext = new PMSContext();
                _SubtaskStatus = new ObservableCollection<SubtaskStatus>(dbContext.SubtaskStatus);
                //_SelectedSubtaskStatus = "Zakończone";
                return _SubtaskStatus;
            }
        }

        public SubtaskStatus SelectedSubtaskStatus
        {
            get
            {
                return _SelectedSubtaskStatus;
            }
            set
            {
                _SelectedSubtaskStatus = value;
                RaisePropertyChanged("SelectedSubtaskStatus");

                if (SelectedSubtask != null && SelectedSubtaskStatus != null)
                {
                    IsEnabledChangeStatusButton = true;
                }
                else
                {
                    IsEnabledChangeStatusButton = false;
                }
            }
        }



        public ObservableCollection<string> StatusWhichICreated
        {
            get
            {
                PMSContext dbContext = new PMSContext();
                _StatusWhichICreated = new ObservableCollection<string>(dbContext.SubtaskStatus.Select(c => c.Name));
                _StatusWhichICreated.Insert(0, "All");
                _SelectedStatusWhichICreated = "All";
                return _StatusWhichICreated;
            }
        }

        public string SelectedStatusWhichICreated
        {
            get
            {
                return _SelectedStatusWhichICreated;
            }
            set
            {
                _SelectedStatusWhichICreated = value;
                RaisePropertyChanged("SelectedStatusWhichICreated");
                FilterSubtaskWhichICreated();
            }
        }

        public ObservableCollection<Subtask> SubtasksWhichICreated
        {
            get
            {
                PMSContext dbContext = new PMSContext();
                if (_SubtasksWhichICreated == null)
                {
                    _SubtasksWhichICreated = new ObservableCollection<Subtask>(dbContext.Subtask.Where(x => x.WhoCreated == Global.user.UserID));
                }
                return _SubtasksWhichICreated;
            }
            set
            {
                _SubtasksWhichICreated = value;
                RaisePropertyChanged("SubtasksWhichICreated");
            }
        }


        #endregion


        private void AddTask()
        {
            EmployeeValidation EV = new EmployeeValidation();
            bool correctForm = EV.AddTaskValidation(Name, Description);
            
            if(correctForm)
            {
                PMSContext dbContext = new PMSContext();

                Subtask subtask = new Subtask
                {
                    Name = _Name,
                    Description = _Description,
                    StartTime = DateTime.Now,
                    EndTime = _EndTime,
                    MainTaskID = null,
                    SubtaskStatusID = 3,
                    Priority = _SelectedPriority,
                    UserID = SelectedUser.UserID,
                    WhoCreated = Global.user.UserID
                };


                RecentActivity ra = new RecentActivity
                {
                    DateAdded = DateTime.Now,
                    TeamID = (int)Global.user.TeamID,
                    Description = $"User {Global.user.FullName} has created a new task " +
                                  $"for {SelectedUser.FullName} called: {_Name}"
                };


                dbContext.Subtask.Add(subtask);
                dbContext.RecentActivity.Add(ra);
                dbContext.SaveChanges();

                ErrorMessage er = new ErrorMessage("Task created successfully!");
                er.ShowDialog();
            }
        }



        private void ChangeStatus()
        {
            //globalne db context?
            

            SelectedSubtask.SubtaskStatusID = SelectedSubtaskStatus.SubtaskStatusID;


            RecentActivity ra = new RecentActivity
            {
                DateAdded = DateTime.Now,
                TeamID = (int)Global.user.TeamID,
                Description = $"User {Global.user.FullName} has changed the status of a task from '{SelectedSubtask.Name}' to '{SelectedSubtaskStatus.Name}'"
            };


            dbContext.RecentActivity.Add(ra);
            dbContext.SaveChanges();


            Subtasks = new ObservableCollection<Subtask>(dbContext.Subtask.Where(x => x.UserID == Global.user.UserID));
            SelectedSubtask = null;
            ErrorMessage er = new ErrorMessage("Subtask status change successfully!");
            er.ShowDialog();
        }



        //pobierać tylko tych z teamu!!!
        public List<User> Users
        {
            get
            {
                PMSContext dbContext = new PMSContext();
                List<User> users2 = dbContext.User.Where(x => x.TeamID == Global.user.TeamID).ToList();
                User user = users2.Where(x => x.UserID == Global.user.UserID).SingleOrDefault();
                users2.Remove(user);
                return users2;
            }
        }

        public void FilterSubtask()
        {


            if (SelectedName == null)
                SelectedName = "";

            //PMSContext dbContext = new PMSContext();

            if (SelectedProject == "All")
                Subtasks = new ObservableCollection<Subtask>(dbContext.Subtask.Where(x => (x.UserID == Global.user.UserID) && (SelectedStatus == "All" ? true : x.SubtaskStatus.Name == SelectedStatus) && (x.Name.Contains(SelectedName))));
            else if (SelectedProject == "Other")
                Subtasks = new ObservableCollection<Subtask>(dbContext.Subtask.Where(x => (x.UserID == Global.user.UserID) && (x.MainTaskID == null) && (SelectedStatus == "All" ? true : x.SubtaskStatus.Name == SelectedStatus) && (x.Name.Contains(SelectedName))));
            else
            {
                Project project = dbContext.Project.Where(x => x.Name == SelectedProject).SingleOrDefault();

                ObservableCollection<MainTask> tmpMaintask = new ObservableCollection<MainTask>(dbContext.MainTask.Where(x => x.ProjectID == project.ProjectID));


                List<Subtask> SubtasksTMP = new List<Subtask>();
                foreach (MainTask mt in tmpMaintask)
                {
                    SubtasksTMP = SubtasksTMP.Concat(mt.Subtasks).ToList();
                }
                Subtasks = new ObservableCollection<Subtask>(SubtasksTMP.Where(x => (x.UserID == Global.user.UserID) && (SelectedStatus == "All" ? true : x.SubtaskStatus.Name == SelectedStatus) && (x.Name.Contains(SelectedName))));
            }

        }



        public void FilterSubtaskWhichICreated()
        {

           PMSContext dbContext = new PMSContext();

           SubtasksWhichICreated = new ObservableCollection<Subtask>(dbContext.Subtask.Where(x => (x.WhoCreated == Global.user.UserID) && (SelectedStatusWhichICreated == "All" ? true : x.SubtaskStatus.Name == SelectedStatusWhichICreated)));

        }

    }
}
