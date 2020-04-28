using PMS.Algorithm;
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
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace PMS
{
    public class AddProjectManagerViewModel : BindableBase
    {

        //startTime i EndTime automatycznie z MainTask (pierwszego ES i ostatniego LF
        //Team automatycznie od managera, który dodaje
        //Project Status automatycznie na New

        #region variables

        private readonly PMSContext dbContext = new PMSContext();

        //PROJECT
        private string _Name = "";
        private double _Cost = 0;

        private ObservableCollection<Client> _Clients;
        private Client _SelectedClient;

        private ObservableCollection<MainTask> _ProjectMainTasks;
        private ObservableCollection<Resources> _ProjectResources;

        //ADD RESOURCES
        private string _NameResource = "";
        private double _CostResource = 0;

        //ADD MAIN TASK
        public string _NameMainTask = "";
        public DateTime _EarlyStart = DateTime.Now;
        public DateTime _EarlyFinish = DateTime.Now;
        public DateTime _LateStart = DateTime.Now;
        public DateTime _LateFinish = DateTime.Now;
        public int _Effort = 1;

        //ADD PRE MAINTASK
        private ObservableCollection<MainTask> _MainTaskWhereAddThePretask;
        private MainTask _SelectedMainTaskWhereAddThePretask;

        private ObservableCollection<MainTask> _MainTaskWhichIsPretask;
        private MainTask _SelectedMainTaskWhichIsPretask;


        //remove PRE MAINTASK
        private ObservableCollection<MainTask> _MainTaskWhereRemoveThePretask = null;
        private MainTask _SelectedMainTaskWhereRemoveThePretask = null;

        private ObservableCollection<MainTask> _RemovePretask = null;
        private MainTask _SelectedRemovePretask = null;

        #endregion


        #region command
        public ICommand AddProjectButton { get; set; }
        public ICommand AddResourcesButton { get; set; }
        
        public ICommand RemoveMainTaskButton { get; set; }
        public ICommand RemoveResourcesButton { get; set; }


        public ICommand AddMainTaskButton { get; set; }
        public ICommand AddPreMainTaskButton { get; set; }
        public ICommand RemovePreMainTaskButton { get; set; }

        public ICommand CalculateCostButton { get; set; }


        #endregion


        #region constructor

        public AddProjectManagerViewModel()
        {
            AddProjectButton = new DelegateCommand(AddProject);
            AddResourcesButton = new DelegateCommand(AddResources);
            AddMainTaskButton = new DelegateCommand(AddMainTask);
            RemoveResourcesButton = new DelegateCommand<object>(RemoveResources);
            RemoveMainTaskButton = new DelegateCommand<object>(RemoveMainTask);

            AddPreMainTaskButton = new DelegateCommand(AddPreMainTask);
            RemovePreMainTaskButton = new DelegateCommand(RemovePreMainTask);
            CalculateCostButton = new DelegateCommand(CalculateCost);
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

        public ObservableCollection<Client> Clients
        {
            get
            {
                if (_Clients == null)
                {
                    _Clients = new ObservableCollection<Client>(dbContext.Client);
                }
                return _Clients;
            }
            set
            {
                _Clients = value;
                RaisePropertyChanged("Clients");
            }
        }

        public Client SelectedClient
        {
            get
            {
                return _SelectedClient;
            }
            set
            {
                _SelectedClient = value;
                RaisePropertyChanged("SelectedClient");
            }
        }

        public ObservableCollection<MainTask> ProjectMainTasks
        {
            get
            {
                if (_ProjectMainTasks == null)
                {
                    _ProjectMainTasks = new ObservableCollection<MainTask>();
                }
                return _ProjectMainTasks;
            }
            set
            {
                _ProjectMainTasks = value;
                RaisePropertyChanged("ProjectMainTasks");
            }
        }

        public ObservableCollection<Resources> ProjectResources
        {
            get
            {
                if (_ProjectResources == null)
                {
                    _ProjectResources = new ObservableCollection<Resources>();
                }
                return _ProjectResources;
            }
            set
            {
                _ProjectResources = value;
                RaisePropertyChanged("ProjectResources");
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
                    _MainTaskWhereAddThePretask = new ObservableCollection<MainTask>(ProjectMainTasks);
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
                    _MainTaskWhichIsPretask = new ObservableCollection<MainTask>(ProjectMainTasks);
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
                    _MainTaskWhereRemoveThePretask = new ObservableCollection<MainTask>(ProjectMainTasks.Where(x => x.PrecedingMainTasks.Count() > 0));
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


        #endregion




        #region methods

        private void AddProject()
        {

            ManagerValidation MV = new ManagerValidation();
            bool correctForm = MV.AddProjectValidation(ProjectMainTasks, ProjectResources, Name);

            bool tmp2 = checkWhichAreCyclicDependency();

            if (tmp2 && correctForm && Cost > 0 && Clients != null)
            {

                DateTime startProject = DateTime.Now;
                DateTime endProject;


                startProject = ProjectMainTasks[0].EarlyStart;
                endProject = DateTime.Now;

                foreach (var mt in ProjectMainTasks)
                {
                    if (startProject.CompareTo(mt.EarlyStart) > 0)
                        startProject = mt.EarlyStart;

                    if (endProject.CompareTo(mt.LateFinish) < 0)
                        endProject = mt.LateFinish;
                }

                Project project = new Project()
                {
                    Name = Name,
                    Cost = Cost,
                    StartTime = startProject,
                    EndTime = endProject,
                    ClientID = SelectedClient.ClientID,
                    TeamID = Global.user.Team.TeamID,
                    ProjectStatusID = 1,


                };

                dbContext.Project.Add(project);
                dbContext.SaveChanges();

                //dodanie resources do projektu
                Project projectWithID = dbContext.Project.Where(x => x.Name == Name).FirstOrDefault();

                foreach (var r in ProjectResources)
                {
                    r.ProjectID = projectWithID.ProjectID;
                    dbContext.Resources.Add(r);
                    dbContext.SaveChanges();
                }


                //dodac najpier do kazdego main taska id projektu, potem zapisywa do bazy   CZY TO BEDZIE POTEM DOBRZE POWIAZANE?
                foreach (var r in ProjectMainTasks)
                    r.ProjectID = projectWithID.ProjectID;


                List<MainTask> calc = new List<MainTask>();
                List<MainTask> tmpTaskList = new List<MainTask>(ProjectMainTasks);

                while (tmpTaskList.Count() != 0)
                {
                    foreach (MainTask task in tmpTaskList.ToList())
                    {
                        bool tmp = check(calc, task.PrecedingMainTasks);

                        if (tmp)
                        {

                            dbContext.MainTask.Add(task);
                            dbContext.SaveChanges();

                            calc.Add(task);
                            tmpTaskList.Remove(task);
                        }
                    }
                }

                //zerowanie zmiennych itp

                ErrorMessage er = new ErrorMessage("Add project!");
                er.ShowDialog();
            }

        }



        public bool check(List<MainTask> calc, List<MainTask> postTask) //sprawdzamy jednego main taska
        {
            if (postTask.Count() == 0) //gdy nie ma poprzedzajacych jest ok
                return true;
            else
                foreach (var tmp in postTask) //sprawdzamy poprzedzzajace 
                    if (!calc.Contains(tmp)) //gdy sa w juz dodanych calc to ok
                        return false;

            return true;
        }


        bool checkWhichAreCyclicDependency()
        {
            List<MainTask> calc = new List<MainTask>();
            List<MainTask> tmpTaskList = new List<MainTask>(ProjectMainTasks);

            while (tmpTaskList.Count() != 0)
            {
                bool error = false;

                foreach (MainTask task in tmpTaskList.ToList())
                {
                    bool tmp = check(calc, task.PrecedingMainTasks);

                    if (tmp)
                    {
                        calc.Add(task);
                        tmpTaskList.Remove(task);
                        error = true;
                    }
                }

                if (!error)
                {
                    ErrorMessage e = new ErrorMessage("Cyclic dependency!");
                    e.ShowDialog();
                    return false;
                }
            }

            return true;
        }



        private void AddResources()
        {
            bool checkName = true;
            foreach (var t in ProjectResources)
            {

                if (t.Name == NameResource)
                    checkName = false;
            }

            if (checkName == true && CostResource > 0 && NameResource.Length > 0)
            {
                Resources resource = new Resources
                {
                    Name = NameResource,
                    Cost = CostResource
                };
                ProjectResources.Add(resource);
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
            Resources tmpRes = ProjectResources.Where(x => x.Name == tmp).FirstOrDefault();
            ProjectResources.Remove(tmpRes);
        }








        private void AddMainTask()
        {

            bool checkName = true;
            foreach (var t in ProjectMainTasks)
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
                    Status = false
                };
                ProjectMainTasks.Add(mainTask);
            }
            else
            {
                ErrorMessage er = new ErrorMessage("The name exists or effort <= 0!");
                er.ShowDialog();
            }

            setVariable();
        }

        private void RemoveMainTask(object name)
        {
            string tmpName = name.ToString();
            MainTask findMainTask = ProjectMainTasks.Where(x => x.Name == tmpName).FirstOrDefault();

            foreach (var mt in ProjectMainTasks)
            {
                foreach (var preMT in mt.PrecedingMainTasks.ToList())
                {
                    if (preMT.Name == findMainTask.Name)
                        mt.PrecedingMainTasks.Remove(findMainTask);
                }
            }

            ProjectMainTasks.Remove(findMainTask);

            setVariable();
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

            foreach (var t in SelectedMainTaskWhereAddThePretask.PrecedingMainTasks)
            {
                if (t.Name == SelectedMainTaskWhichIsPretask.Name)
                {
                    ErrorMessage er = new ErrorMessage("To zadanie jest juz tutaj dodane jako poprzedzajace!");
                    er.ShowDialog();
                    return;
                }
            }

            SelectedMainTaskWhereAddThePretask.PrecedingMainTasks.Add(SelectedMainTaskWhichIsPretask);

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

            setVariable();

        }


        private void setVariable()
        {
            //sztuczka zeby odswierzyly sie pretaski
            ObservableCollection<MainTask> tmp = new ObservableCollection<MainTask>(ProjectMainTasks);
            ProjectMainTasks = new ObservableCollection<MainTask>(tmp);


            MainTaskWhichIsPretask = new ObservableCollection<MainTask>(ProjectMainTasks);
            MainTaskWhereAddThePretask = new ObservableCollection<MainTask>(ProjectMainTasks);
            SelectedMainTaskWhereAddThePretask = null;
            SelectedMainTaskWhichIsPretask = null;

            MainTaskWhereRemoveThePretask = new ObservableCollection<MainTask>(ProjectMainTasks.Where(x => x.PrecedingMainTasks.Count() > 0));
            RemovePretask = null;
            SelectedRemovePretask = null;
            SelectedMainTaskWhereRemoveThePretask = null;

            NameMainTask = "";
            Effort = 0;
            EarlyStart = DateTime.Now;
            EarlyFinish = DateTime.Now;
            LateStart = DateTime.Now;
            LateFinish = DateTime.Now;
        }

        private void CalculateCost()
        {

            ManagerValidation MV = new ManagerValidation();
            bool correctForm = MV.CalculateCostValidation(ProjectMainTasks, ProjectResources);

            if (correctForm)
            {
                double tmp = 0;
                DateTime startProject = DateTime.Now;
                DateTime endProject;

                foreach (var resources in ProjectResources)
                    tmp += resources.Cost;

                if (ProjectMainTasks.Count() > 0)
                    startProject = ProjectMainTasks[0].EarlyStart;
                endProject = DateTime.Now;

                foreach (var mt in ProjectMainTasks)
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
        #endregion

    }
}
