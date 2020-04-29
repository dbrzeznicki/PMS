using DlhSoft.Windows.Controls;
using PMS.Algorithm;
using PMS.DAL;
using PMS.Model;
using PMS.Utils;
using PMS.ViewModel;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace PMS
{
    public class ProjectManagerViewModel : BindableBase
    {

        #region variables
        private PMSContext dbContext = new PMSContext();

        private ObservableCollection<Project> _Projects = new ObservableCollection<Project>();
        private Project _SelectedProject;

        private double _Cost = 0;

        private ObservableCollection<ProjectStatus> _ProjectStatus = new ObservableCollection<ProjectStatus>();
        private ProjectStatus _SelectedProjectStatus = null;

        private ObservableCollection<MainTask> _ListOfMaintask = new ObservableCollection<MainTask>();
        private MainTask _SelectedMaintask;

        private ObservableCollection<GanttChartItem> _Items = new ObservableCollection<GanttChartItem>();

        private ObservableCollection<Subtask> _ListOfSubtask = new ObservableCollection<Subtask>();

        private ObservableCollection<Resources> _ListOfResources = new ObservableCollection<Resources>();

        //ADD RESOURCES
        private string _NameResource = "";
        private double _CostResource = 0;

        //EDIT RESOURCES - lista resource juz jest wyzej
        private Resources _SelectedResource;
        private double _EditCostResource = 0;

        //ADD MAIN TASK
        public string _NameMainTask = "";
        public DateTime _EarlyStart = DateTime.Now;
        public DateTime _EarlyFinish = DateTime.Now;
        public DateTime _LateStart = DateTime.Now;
        public DateTime _LateFinish = DateTime.Now;
        public int _Effort = 1;

        //ADD PRE MAINTASK
        // liste main taskow juz mamy wyzej 
        private ObservableCollection<MainTask> _MainTaskWhereAddThePretask;
        private MainTask _SelectedMainTaskWhereAddThePretask;

        private ObservableCollection<MainTask> _MainTaskWhichIsPretask;
        private MainTask _SelectedMainTaskWhichIsPretask;


        //remove PRE MAINTASK
        private ObservableCollection<MainTask> _MainTaskWhereRemoveThePretask = null;
        private MainTask _SelectedMainTaskWhereRemoveThePretask = null;

        private ObservableCollection<MainTask> _RemovePretask = null;
        private MainTask _SelectedRemovePretask = null;

        //Edit MAIN TASK
        private MainTask _EditSelectedMainTask = null;
        public string _EditNameMainTask = "";
        public DateTime _EditEarlyStart = DateTime.Now;
        public DateTime _EditEarlyFinish = DateTime.Now;
        public DateTime _EditLateStart = DateTime.Now;
        public DateTime _EditLateFinish = DateTime.Now;
        public int _EditEffort = 0;

        #endregion


        #region command

        public ICommand ShowAddProjectButton { get; set; }
        public ICommand ChangeStatusButton { get; set; }
        public ICommand CalculateCostButton { get; set; }
        public ICommand UpdateCostButton { get; set; }
        public ICommand ChangeStatusMainTaskButton { get; set; }
        public ICommand AddResourcesButton { get; set; }
        public ICommand RemoveResourcesButton { get; set; }
        public ICommand EditResourcesButton { get; set; }
        public ICommand AddMainTaskButton { get; set; }
        public ICommand AddPreMainTaskButton { get; set; }
        public ICommand RemovePreMainTaskButton { get; set; }
        public ICommand RemoveMainTaskButton { get; set; }
        public ICommand EditMainTaskButton { get; set; }
        public ICommand GenerateRaportButton { get; set; }
        

        #endregion


        #region constructor
        public ProjectManagerViewModel()
        {
            ShowAddProjectButton = new DelegateCommand(ShowAddProject);
            ChangeStatusButton = new DelegateCommand(ChangeStatus);
            CalculateCostButton = new DelegateCommand(CalculateCost);
            UpdateCostButton = new DelegateCommand(UpdateCost);
            ChangeStatusMainTaskButton = new DelegateCommand<object>(ChangeStatusMainTask); //przekazuje id main taska
            AddResourcesButton = new DelegateCommand(AddResources);
            RemoveResourcesButton = new DelegateCommand<object>(RemoveResources);
            EditResourcesButton = new DelegateCommand(EditResources);
            AddMainTaskButton = new DelegateCommand(AddMainTask);
            AddPreMainTaskButton = new DelegateCommand(AddPreMainTask);
            RemovePreMainTaskButton = new DelegateCommand(RemovePreMainTask);
            RemoveMainTaskButton = new DelegateCommand<object>(RemoveMainTask);
            EditMainTaskButton = new DelegateCommand(EditMainTask);
            GenerateRaportButton = new DelegateCommand(GenerateRaport);
        }

        #endregion

        #region properties

        public ObservableCollection<Project> Projects
        {
            get
            {
                _Projects = new ObservableCollection<Project>(dbContext.Project.Where(x => x.TeamID == Global.user.TeamID));
                if (_Projects.Count() > 0)
                    SelectedProject = _Projects[0];
                return _Projects;
            }
        }

        public Project SelectedProject
        {
            get
            {
                return _SelectedProject;
            }
            set
            {
                _SelectedProject = value;
                ListOfMaintask = new ObservableCollection<MainTask>(dbContext.MainTask.Where(x => x.ProjectID == SelectedProject.ProjectID));
                ListOfResources = new ObservableCollection<Resources>(dbContext.Resources.Where(x => x.ProjectID == SelectedProject.ProjectID));
                Cost = SelectedProject.Cost;
                SelectedProjectStatus = SelectedProject.ProjectStatus;
                refreshGanttChart();
                RaisePropertyChanged("SelectedProject");
            }
        }

        public double Cost
        {
            get
            {
                return _Cost;
            }
            set
            {
                _Cost = value;
                RaisePropertyChanged("Cost");
            }
        }


        public ObservableCollection<ProjectStatus> ProjectStatus
        {
            get
            {
                _ProjectStatus = new ObservableCollection<ProjectStatus>(dbContext.ProjectStatus);
                return _ProjectStatus;
            }
        }

        public ProjectStatus SelectedProjectStatus
        {
            get
            {
                if (_SelectedProjectStatus == null)
                    _SelectedProjectStatus = SelectedProject.ProjectStatus;
                return _SelectedProjectStatus;
            }
            set
            {
                _SelectedProjectStatus = value;
                RaisePropertyChanged("SelectedProjectStatus");
            }
        }

        public ObservableCollection<MainTask> ListOfMaintask
        {
            get
            {
                if (SelectedProject != null)
                    _ListOfMaintask = new ObservableCollection<MainTask>(dbContext.MainTask.Where(x => x.ProjectID == SelectedProject.ProjectID));
                if (_ListOfMaintask.Count > 0)
                    SelectedMaintask = _ListOfMaintask[0];
                return _ListOfMaintask;
            }
            set
            {
                _ListOfMaintask = value;
                RaisePropertyChanged("ListOfMaintask");
            }
        }

        public MainTask SelectedMaintask
        {
            get
            {
                return _SelectedMaintask;
            }
            set
            {
                _SelectedMaintask = value;
                if (SelectedMaintask != null)
                    ListOfSubtask = new ObservableCollection<Subtask>(dbContext.Subtask.Where(x => x.MainTaskID == SelectedMaintask.MainTaskID));
                else
                    ListOfSubtask = new ObservableCollection<Subtask>();
                RaisePropertyChanged("SelectedMaintask");
            }
        }
        public ObservableCollection<GanttChartItem> Items
        {
            get
            {
                return _Items;
            }
            set
            {
                _Items = value;
                RaisePropertyChanged("Items");
            }
        }

        public ObservableCollection<Subtask> ListOfSubtask
        {
            get
            {
                if (SelectedMaintask != null)
                    _ListOfSubtask = new ObservableCollection<Subtask>(dbContext.Subtask.Where(x => x.MainTaskID == SelectedMaintask.MainTaskID));
                return _ListOfSubtask;
            }
            set
            {
                _ListOfSubtask = value;
                RaisePropertyChanged("ListOfSubtask");
            }
        }

        public ObservableCollection<Resources> ListOfResources
        {
            get
            {
                _ListOfResources = new ObservableCollection<Resources>(dbContext.Resources.Where(x => x.ProjectID == SelectedProject.ProjectID));
                if (_ListOfResources.Count() > 0)
                    SelectedResource = _ListOfResources[0];
                return _ListOfResources;
            }
            set
            {
                _ListOfResources = value;
                RaisePropertyChanged("ListOfResources");
            }
        }

        public Resources SelectedResource
        {
            get
            {
                return _SelectedResource;
            }
            set
            {
                _SelectedResource = value;

                RaisePropertyChanged("SelectedResource");
            }
        }

        public string NameResource
        {
            get
            {
                return _NameResource;
            }
            set
            {
                _NameResource = value;
                RaisePropertyChanged("NameResource");
            }
        }

        public double CostResource
        {
            get
            {
                return _CostResource;
            }
            set
            {
                _CostResource = value;
                RaisePropertyChanged("CostResource");
            }
        }

        public double EditCostResource
        {
            get
            {
                return _EditCostResource;
            }
            set
            {
                _EditCostResource = value;
                RaisePropertyChanged("EditCostResource");
            }
        }

        public string NameMainTask
        {
            get
            {
                return _NameMainTask;
            }
            set
            {
                _NameMainTask = value;
                RaisePropertyChanged("NameMainTask");
            }
        }

        public DateTime EarlyStart
        {
            get
            {
                return _EarlyStart;
            }
            set
            {
                _EarlyStart = value;
                RaisePropertyChanged("EarlyStart");
            }
        }

        public DateTime EarlyFinish
        {
            get
            {
                return _EarlyFinish;
            }
            set
            {
                _EarlyFinish = value;
                RaisePropertyChanged("EarlyFinish");
            }
        }

        public DateTime LateStart
        {
            get
            {
                return _LateStart;
            }
            set
            {
                _LateStart = value;
                RaisePropertyChanged("LateStart");
            }
        }

        public DateTime LateFinish
        {
            get
            {
                return _LateFinish;
            }
            set
            {
                _LateFinish = value;
                RaisePropertyChanged("LateFinish");
            }
        }

        public int Effort
        {
            get
            {
                return _Effort;
            }
            set
            {
                _Effort = value;
                RaisePropertyChanged("Effort");
            }
        }

        public ObservableCollection<MainTask> MainTaskWhereAddThePretask
        {
            get
            {
                if (_MainTaskWhereAddThePretask == null)
                {
                    _MainTaskWhereAddThePretask = new ObservableCollection<MainTask>(ListOfMaintask);
                }
                return _MainTaskWhereAddThePretask;
            }
            set
            {
                _MainTaskWhereAddThePretask = value;
                RaisePropertyChanged("MainTaskWhereAddThePretask");
            }
        }

        public MainTask SelectedMainTaskWhereAddThePretask
        {
            get { return _SelectedMainTaskWhereAddThePretask; }
            set
            {
                _SelectedMainTaskWhereAddThePretask = value;
                RaisePropertyChanged("SelectedMainTaskWhereAddThePretask");
            }
        }

        public ObservableCollection<MainTask> MainTaskWhichIsPretask
        {
            get
            {
                if (_MainTaskWhichIsPretask == null)
                {
                    _MainTaskWhichIsPretask = new ObservableCollection<MainTask>(ListOfMaintask);
                }
                return _MainTaskWhichIsPretask;
            }
            set
            {
                _MainTaskWhichIsPretask = value;
                RaisePropertyChanged("MainTaskWhichIsPretask");
            }
        }

        public MainTask SelectedMainTaskWhichIsPretask
        {
            get { return _SelectedMainTaskWhichIsPretask; }
            set
            {
                _SelectedMainTaskWhichIsPretask = value;
                RaisePropertyChanged("SelectedMainTaskWhichIsPretask");
            }
        }

        public ObservableCollection<MainTask> MainTaskWhereRemoveThePretask
        {
            get
            {
                if (_MainTaskWhereRemoveThePretask == null)
                {
                    _MainTaskWhereRemoveThePretask = new ObservableCollection<MainTask>(ListOfMaintask.Where(x => x.PrecedingMainTasks != null && x.PrecedingMainTasks.Count() > 0));
                }
                return _MainTaskWhereRemoveThePretask;
            }
            set
            {
                _MainTaskWhereRemoveThePretask = value;
                RaisePropertyChanged("MainTaskWhereRemoveThePretask");
            }
        }

        public MainTask SelectedMainTaskWhereRemoveThePretask
        {
            get { return _SelectedMainTaskWhereRemoveThePretask; }
            set
            {
                _SelectedMainTaskWhereRemoveThePretask = value;

                if (_SelectedMainTaskWhereRemoveThePretask != null)
                    RemovePretask = new ObservableCollection<MainTask>(_SelectedMainTaskWhereRemoveThePretask.PrecedingMainTasks);
                RaisePropertyChanged("SelectedMainTaskWhereRemoveThePretask");
            }
        }

        public ObservableCollection<MainTask> RemovePretask
        {
            get
            {
                if (_RemovePretask == null)
                {
                    _RemovePretask = new ObservableCollection<MainTask>();
                }
                return _RemovePretask;
            }
            set
            {
                _RemovePretask = value;
                RaisePropertyChanged("RemovePretask");
            }
        }

        public MainTask SelectedRemovePretask
        {
            get { return _SelectedRemovePretask; }
            set
            {
                _SelectedRemovePretask = value;
                RaisePropertyChanged("SelectedRemovePretask");
            }
        }


        public string EditNameMainTask
        {
            get
            {
                return _EditNameMainTask;
            }
            set
            {
                _EditNameMainTask = value;
                RaisePropertyChanged("EditNameMainTask");
            }
        }

        public DateTime EditEarlyStart
        {
            get
            {
                return _EditEarlyStart;
            }
            set
            {
                _EditEarlyStart = value;
                RaisePropertyChanged("EditEarlyStart");
            }
        }

        public DateTime EditEarlyFinish
        {
            get
            {
                return _EditEarlyFinish;
            }
            set
            {
                _EditEarlyFinish = value;
                RaisePropertyChanged("EditEarlyFinish");
            }
        }

        public DateTime EditLateStart
        {
            get
            {
                return _EditLateStart;
            }
            set
            {
                _EditLateStart = value;
                RaisePropertyChanged("EditLateStart");
            }
        }

        public DateTime EditLateFinish
        {
            get
            {
                return _EditLateFinish;
            }
            set
            {
                _EditLateFinish = value;
                RaisePropertyChanged("EditLateFinish");
            }
        }

        public int EditEffort
        {
            get
            {
                return _EditEffort;
            }
            set
            {
                _EditEffort = value;
                RaisePropertyChanged("EditEffort");
            }
        }

        public MainTask EditSelectedMainTask
        {
            get { return _EditSelectedMainTask; }
            set
            {
                _EditSelectedMainTask = value;

                EditNameMainTask = _EditSelectedMainTask.Name;
                EditEarlyStart = _EditSelectedMainTask.EarlyStart;
                EditEarlyFinish = _EditSelectedMainTask.EarlyFinish;
                EditLateStart = _EditSelectedMainTask.LateStart;
                EditLateFinish = _EditSelectedMainTask.LateFinish;
                EditEffort = _EditSelectedMainTask.Effort;
                RaisePropertyChanged("EditSelectedMainTask");
            }
        }

        #endregion


        #region methods
        private void ShowAddProject()
        {
            AddProjectWindow FPV = new AddProjectWindow();
            FPV.Show();
        }

        public void refreshGanttChart()
        {

            _Items = new ObservableCollection<GanttChartItem>();

            foreach (var a in ListOfMaintask)
            {
                if (a.Status == false)
                    _Items.Add(new GanttChartItem { Content = a.Name, Start = a.EarlyStart, Finish = a.LateFinish, IsBarReadOnly = true, IsCompleted = false });
                else
                    _Items.Add(new GanttChartItem { Content = a.Name, Start = a.EarlyStart, Finish = a.LateFinish, IsBarReadOnly = true, IsCompleted = true });
            }


            RaisePropertyChanged("Items");
        }

        private void CalculateCost()
        {

            ManagerValidation MV = new ManagerValidation();
            bool correctForm = MV.CalculateCostValidation(ListOfMaintask, ListOfResources);

            if (correctForm)
            {
                double tmp = 0;
                DateTime startProject = DateTime.Now;
                DateTime endProject;

                foreach (var resources in ListOfResources)
                    tmp += resources.Cost;

                if (ListOfMaintask.Count() > 0)
                    startProject = ListOfMaintask[0].EarlyStart;
                endProject = DateTime.Now;

                foreach (var mt in ListOfMaintask)
                {
                    if (startProject.CompareTo(mt.EarlyStart) > 0)
                        startProject = mt.EarlyStart;

                    if (endProject.CompareTo(mt.LateFinish) < 0)
                        endProject = mt.LateFinish;
                }

                TimeSpan interval = endProject - startProject;
                int days = interval.Days;

                int month = (int)Math.Ceiling(((double)days) / 30); // OK

                double allSalary = 0;
                foreach (var user in Global.user.Team.Users)
                    allSalary += user.Salary;

                allSalary = allSalary * month;

                tmp = tmp + allSalary;
                Cost = tmp;
            }
        }

        private void UpdateCost()
        {
            if (Cost > 0)
            {
                SelectedProject.Cost = Cost;
                dbContext.SaveChanges();

                ErrorMessage er = new ErrorMessage("Update cost successfully!");
                er.ShowDialog();
            }
        }

        private void ChangeStatus()
        {
            SelectedProject.ProjectStatus = SelectedProjectStatus;
            dbContext.SaveChanges();

            ErrorMessage er = new ErrorMessage("Project status update successfully!");
            er.ShowDialog();
        }

        private void ChangeStatusMainTask(object maintaskID)
        {

            int ID = int.Parse(maintaskID.ToString());

            MainTask mt = dbContext.MainTask.Where(x => x.MainTaskID == ID).SingleOrDefault();

            if (mt.Status == false)
                mt.Status = true;
            else
                mt.Status = false;

            dbContext.SaveChanges();

            ListOfMaintask = new ObservableCollection<MainTask>(dbContext.MainTask.Where(x => x.ProjectID == SelectedProject.ProjectID));
            refreshGanttChart();

            ErrorMessage er = new ErrorMessage("Main task status update successfully!");
            er.ShowDialog();
        }

        private void AddResources()
        {
            bool checkName = true;
            foreach (var t in ListOfResources)
                if (t.Name == NameResource)
                    checkName = false;

            if (checkName == true && CostResource > 0 && NameResource.Length > 0)
            {
                Resources resource = new Resources
                {
                    Name = NameResource,
                    Cost = CostResource,
                    Project = SelectedProject //czy dobrze???
                };

                dbContext.Resources.Add(resource);
                dbContext.SaveChanges();

                ListOfResources = new ObservableCollection<Resources>(dbContext.Resources.Where(x => x.ProjectID == SelectedProject.ProjectID));

                ErrorMessage er = new ErrorMessage("Operation successfully!");
                er.ShowDialog();
            }
            else
            {
                ErrorMessage er = new ErrorMessage("The name exists or cost <= 0!");
                er.ShowDialog();
            }

        }

        private void RemoveResources(object name)
        {
            string tmp = name.ToString();
            Resources tmpRes = ListOfResources.Where(x => x.Name == tmp).FirstOrDefault();

            dbContext.Resources.Remove(tmpRes);
            dbContext.SaveChanges();

            ErrorMessage er = new ErrorMessage("Operation successfully!");
            er.ShowDialog();

            ListOfResources = new ObservableCollection<Resources>(dbContext.Resources.Where(x => x.ProjectID == SelectedProject.ProjectID));
        }

        private void EditResources()
        {
            if (EditCostResource > 0)
            {
                SelectedResource.Cost = EditCostResource;
                dbContext.SaveChanges();

                ListOfResources = new ObservableCollection<Resources>(dbContext.Resources.Where(x => x.ProjectID == SelectedProject.ProjectID));

                ErrorMessage er = new ErrorMessage("Edit resource successfully!");
                er.ShowDialog();
            }
            else
            {
                ErrorMessage er = new ErrorMessage("The name exists or cost <= 0!");
                er.ShowDialog();
            }
        }







        private void AddMainTask()
        {

            bool checkName = true;
            foreach (var t in ListOfMaintask)
            {

                if (t.Name == NameMainTask)
                    checkName = false;
            }

            if (checkName == true && Effort > 0 && NameMainTask.Length > 0)
            {
                MainTask mainTask = new MainTask
                {
                    Name = NameMainTask,
                    Effort = Effort,
                    EarlyFinish = EarlyFinish,
                    EarlyStart = EarlyStart,
                    LateFinish = LateFinish,
                    LateStart = LateStart,
                    PrecedingMainTasks = new List<MainTask>(),
                    Status = false,
                    Project = SelectedProject
                };
                ListOfMaintask.Add(mainTask);

                dbContext.MainTask.Add(mainTask);
                dbContext.SaveChanges();


                setVariable();
            }
            else
            {
                ErrorMessage er = new ErrorMessage("The name exists or effort <= 0!");
                er.ShowDialog();
            }


        }





        private void AddPreMainTask()
        {
            if (SelectedMainTaskWhereAddThePretask == null || SelectedMainTaskWhichIsPretask == null)
            {
                ErrorMessage er = new ErrorMessage("Wybierz wartosci, nie moga byc null!");
                er.ShowDialog();
                return;
            }

            if (SelectedMainTaskWhereAddThePretask.Name == SelectedMainTaskWhichIsPretask.Name)
            {
                ErrorMessage er = new ErrorMessage("Nazwy nie mogá by takie same!");
                er.ShowDialog();
                return;
            }


            if (SelectedMainTaskWhereAddThePretask.PrecedingMainTasks != null)
            {
                foreach (var t in SelectedMainTaskWhereAddThePretask.PrecedingMainTasks)
                {
                    if (t.Name == SelectedMainTaskWhichIsPretask.Name)
                    {
                        ErrorMessage er = new ErrorMessage("To zadanie jest juz tutaj dodane jako poprzedzajace!");
                        er.ShowDialog();
                        return;
                    }
                }
            }

            SelectedMainTaskWhereAddThePretask.PrecedingMainTasks = new List<MainTask>();
            SelectedMainTaskWhereAddThePretask.PrecedingMainTasks.Add(SelectedMainTaskWhichIsPretask);

            dbContext.SaveChanges();


            setVariable();
        }

        private void RemovePreMainTask()
        {
            if (SelectedRemovePretask == null || SelectedMainTaskWhereRemoveThePretask == null)
            {
                ErrorMessage er = new ErrorMessage("Wybierz wartosci, nie moga byc null!");
                er.ShowDialog();
                return;
            }

            SelectedMainTaskWhereRemoveThePretask.PrecedingMainTasks.Remove(SelectedRemovePretask);

            dbContext.SaveChanges();

            setVariable();

        }


        private void setVariable()
        {
            //sztuczka zeby odswierzyly sie pretaski
            ListOfMaintask = new ObservableCollection<MainTask>(dbContext.MainTask.Where(x => x.ProjectID == SelectedProject.ProjectID));
            SelectedMaintask = _ListOfMaintask[0];


            MainTaskWhichIsPretask = new ObservableCollection<MainTask>(ListOfMaintask);
            MainTaskWhereAddThePretask = new ObservableCollection<MainTask>(ListOfMaintask);


            SelectedMainTaskWhereAddThePretask = null;
            SelectedMainTaskWhichIsPretask = null;

            MainTaskWhereRemoveThePretask = new ObservableCollection<MainTask>(ListOfMaintask.Where(x => x.PrecedingMainTasks != null && x.PrecedingMainTasks.Count() > 0));
            RemovePretask = null;
            SelectedRemovePretask = null;
            SelectedMainTaskWhereRemoveThePretask = null;

            NameMainTask = "";
            Effort = 0;
            EarlyStart = DateTime.Now;
            EarlyFinish = DateTime.Now;
            LateStart = DateTime.Now;
            LateFinish = DateTime.Now;

            EditNameMainTask = "";
            EditEffort = 0;
            EditEarlyStart = DateTime.Now;
            EditEarlyFinish = DateTime.Now;
            EditLateStart = DateTime.Now;
            EditLateFinish = DateTime.Now;

            refreshGanttChart();
        }


        private void RemoveMainTask(object maintaskID)
        {
            int ID = int.Parse(maintaskID.ToString());
            MainTask tmpMainTask = dbContext.MainTask.Where(x => x.MainTaskID == ID).SingleOrDefault();

            bool check = true;

            if (tmpMainTask.Subtasks != null && tmpMainTask.Subtasks.Count() > 0)
                check = false;

            if (check == true)
            {
                foreach (var mt in ListOfMaintask) //usuniecie pretask
                {
                    if (mt.PrecedingMainTasks != null)
                    {
                        for (int i = 0; i < mt.PrecedingMainTasks.Count(); i++)
                        {
                            if (mt.PrecedingMainTasks[i].Name == tmpMainTask.Name)
                                mt.PrecedingMainTasks.Remove(tmpMainTask);
                        }
                    }
                }

                dbContext.MainTask.Remove(tmpMainTask);
                dbContext.SaveChanges();

                setVariable();
            }
            else
            {
                ErrorMessage er = new ErrorMessage("Operation failed. The main task has subtasks!");
                er.ShowDialog();
            }

        }

        private void EditMainTask()
        {
            if (EditEffort > 0 && EditNameMainTask.Length > 0) //cos
            {
                EditSelectedMainTask.Name = EditNameMainTask;
                EditSelectedMainTask.LateStart = EditLateStart;
                EditSelectedMainTask.LateFinish = EditLateFinish;
                EditSelectedMainTask.EarlyStart = EditEarlyStart;
                EditSelectedMainTask.EarlyFinish = EditEarlyFinish;
                EditSelectedMainTask.Effort = EditEffort;

                dbContext.SaveChanges();

                setVariable();

                ErrorMessage er = new ErrorMessage("Edit main task successfully!");
                er.ShowDialog();
            }
            else
            {
                ErrorMessage er = new ErrorMessage("The name exists or cost <= 0!");
                er.ShowDialog();
            }
        }

        private void GenerateRaport()
        {
            GenerateProjectStatistic GPS = new GenerateProjectStatistic();
            GPS.generateWordDocument(SelectedProject);
        }
            
        #endregion

    }
}
