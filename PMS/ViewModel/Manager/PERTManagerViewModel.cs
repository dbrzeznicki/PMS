using DlhSoft.Windows.Controls.Pert;
using PMS.Algorithm;
using PMS.Model;
using PMS.Utils;
using Prism.Commands;
//using PMS.Model;
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
    public class PERTManagerViewModel : BindableBase
    {

        #region variable
        private PERT pertObject = new PERT();


        private ObservableCollection<NetworkDiagramItem> _items = new ObservableCollection<NetworkDiagramItem>();
        private ObservableCollection<PERTTask> _PERTTaskResult;
        private ObservableCollection<PERTTask> _PERTTaskList;
        private DateTime _StartProject = DateTime.Now;

        //DODANIE PERT TASKA
        private string _Name = "";
        private int _Optimistic_a = 0;
        private int _MostLikely_m = 0;
        private int _Pessimistic_b = 0;

        //DODANIE PRE TASKA
        private ObservableCollection<PERTTask> _TaskWhereAddThePretask;
        private PERTTask _SelectedTaskWhereAddThePretask;
        private ObservableCollection<PERTTask> _TaskWhichIsPretask;
        private PERTTask _SelectedTaskWhichIsPretask;


        //USUNIECIE PRETASKA
        private ObservableCollection<PERTTask> _TaskWhereRemoveThePretask = null;
        private PERTTask _SelectedTaskWhereRemoveThePretask = null;
        private ObservableCollection<PERTTask> _RemovePretask = null;
        private PERTTask _SelectedRemovePretask = null;

        private double _ProbabilityOfCompletingDays = 0.0;
        private double _ProbabilityOfCompletingPercent = 0.0;
        private double _ProjectDaysTime = 0.0;
        private int _ProjectPercentTime = 0;
        #endregion


        #region command

        public ICommand CalculateProbabilityOfCompletingDaysButton { get; set; }
        public ICommand CalculateProbabilityOfCompletingPercentButton { get; set; }
        public ICommand CalculateButton { get; set; }
        public ICommand RemovePERTTaskButton { get; set; }
        public ICommand AddPERTTaskButton { get; set; }
        public ICommand AddPreTaskButton { get; set; }
        public ICommand RemovePreTaskButton { get; set; }
        public ICommand OpenCocomoBasicButton { get; set; }
        public ICommand OpenCocomoIntermediateButton { get; set; }
        public ICommand OpenCocomoDetailButton { get; set; }
        public ICommand OpenFunctionPointButton { get; set; }
        public ICommand OpenCocomoIIStageIButton { get; set; }
        public ICommand OpenCocomoIIStageIIButton { get; set; }
        public ICommand OpenCocomoIIStageIIIButton { get; set; }

        #endregion


        #region constructor

        public PERTManagerViewModel()
        {
            CalculateButton = new DelegateCommand(Calculate);
            RemovePERTTaskButton = new DelegateCommand<object>(RemovePERTTask);
            AddPERTTaskButton = new DelegateCommand(AddPERTTask);
            AddPreTaskButton = new DelegateCommand(AddPreTask);
            RemovePreTaskButton = new DelegateCommand(RemovePreTask);

            OpenCocomoBasicButton = new DelegateCommand(OpenCocomoBasic);
            OpenCocomoIntermediateButton = new DelegateCommand(OpenCocomoIntermediate);
            OpenCocomoDetailButton = new DelegateCommand(OpenCocomoDetail);
            OpenFunctionPointButton = new DelegateCommand(OpenFunctionPoint);
            OpenCocomoIIStageIButton = new DelegateCommand(OpenCocomoIIStageI);
            OpenCocomoIIStageIIButton = new DelegateCommand(OpenCocomoIIStageII);
            OpenCocomoIIStageIIIButton = new DelegateCommand(OpenCocomoIIStageIII);

            CalculateProbabilityOfCompletingDaysButton = new DelegateCommand(CalculateProbabilityOfCompletingDays);
            CalculateProbabilityOfCompletingPercentButton = new DelegateCommand(CalculateProbabilityOfCompletingPercent);
        }

        #endregion


        #region properties
        public ObservableCollection<PERTTask> PERTTaskResult
        {
            get
            {
                if (_PERTTaskResult == null)
                    _PERTTaskResult = new ObservableCollection<PERTTask>();

                return _PERTTaskResult;
            }
            set
            {
                _PERTTaskResult = value;
                RaisePropertyChanged("PERTTaskResult");
            }
        }

        public DateTime StartProject
        {
            get
            {
                if (_StartProject == null)
                    _StartProject = DateTime.Now;

                return _StartProject;
            }
            set
            {
                _StartProject = value;
                RaisePropertyChanged("StartProject");
            }
        }

        public int Optimistic_a
        {
            get
            {
                return _Optimistic_a;
            }
            set
            {
                _Optimistic_a = value;
                RaisePropertyChanged("Optimistic_a");
            }
        }

        public int Pessimistic_b
        {
            get
            {
                return _Pessimistic_b;
            }
            set
            {
                _Pessimistic_b = value;
                RaisePropertyChanged("Pessimistic_b");
            }
        }

        public int MostLikely_m
        {
            get
            {
                return _MostLikely_m;
            }
            set
            {
                _MostLikely_m = value;
                RaisePropertyChanged("MostLikely_m");
            }
        }

        public double ProbabilityOfCompletingDays
        {
            get
            {
                return _ProbabilityOfCompletingDays;
            }
            set
            {
                _ProbabilityOfCompletingDays = value;
                RaisePropertyChanged("ProbabilityOfCompletingDays");
            }
        }

        public double ProbabilityOfCompletingPercent
        {
            get
            {
                return _ProbabilityOfCompletingPercent;
            }
            set
            {
                _ProbabilityOfCompletingPercent = value;
                RaisePropertyChanged("ProbabilityOfCompletingPercent");
            }
        }

        public double ProjectDaysTime
        {
            get
            {
                return _ProjectDaysTime;
            }
            set
            {
                _ProjectDaysTime = value;
                RaisePropertyChanged("ProjectDaysTime");
            }
        }
        public int ProjectPercentTime
        {
            get
            {
                return _ProjectPercentTime;
            }
            set
            {
                _ProjectPercentTime = value;
                RaisePropertyChanged("ProjectPercentTime");
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

        public ObservableCollection<PERTTask> TaskWhereAddThePretask
        {
            get
            {
                if (_TaskWhereAddThePretask == null)
                    _TaskWhereAddThePretask = new ObservableCollection<PERTTask>(PERTTaskList);

                return _TaskWhereAddThePretask;
            }
            set
            {
                _TaskWhereAddThePretask = value;
                RaisePropertyChanged("TaskWhereAddThePretask");
            }
        }

        public PERTTask SelectedTaskWhereAddThePretask
        {
            get
            {
                return _SelectedTaskWhereAddThePretask;
            }
            set
            {
                _SelectedTaskWhereAddThePretask = value;
                RaisePropertyChanged("SelectedTaskWhereAddThePretask");
            }
        }

        public ObservableCollection<PERTTask> TaskWhichIsPretask
        {
            get
            {
                if (_TaskWhichIsPretask == null)
                    _TaskWhichIsPretask = new ObservableCollection<PERTTask>(PERTTaskList);

                return _TaskWhichIsPretask;
            }
            set
            {
                _TaskWhichIsPretask = value;
                RaisePropertyChanged("TaskWhichIsPretask");
            }
        }

        public PERTTask SelectedTaskWhichIsPretask
        {
            get
            {
                return _SelectedTaskWhichIsPretask;
            }
            set
            {
                _SelectedTaskWhichIsPretask = value;
                RaisePropertyChanged("SelectedTaskWhichIsPretask");
            }
        }

        public ObservableCollection<PERTTask> TaskWhereRemoveThePretask
        {
            get
            {
                if (_TaskWhereRemoveThePretask == null)
                    _TaskWhereRemoveThePretask = new ObservableCollection<PERTTask>(PERTTaskList.Where(x => x.PreTask.Count() > 0));

                return _TaskWhereRemoveThePretask;
            }
            set
            {
                _TaskWhereRemoveThePretask = value;
                RaisePropertyChanged("TaskWhereRemoveThePretask");
            }
        }

        public PERTTask SelectedTaskWhereRemoveThePretask
        {
            get
            {
                return _SelectedTaskWhereRemoveThePretask;
            }
            set
            {
                _SelectedTaskWhereRemoveThePretask = value;

                if (_SelectedTaskWhereRemoveThePretask != null)
                    RemovePretask = new ObservableCollection<PERTTask>(_SelectedTaskWhereRemoveThePretask.PreTask);
                RaisePropertyChanged("SelectedTaskWhereRemoveThePretask");
            }
        }

        public ObservableCollection<PERTTask> RemovePretask
        {
            get
            {
                if (_RemovePretask == null)
                    _RemovePretask = new ObservableCollection<PERTTask>();

                return _RemovePretask;
            }
            set
            {
                _RemovePretask = value;
                RaisePropertyChanged("RemovePretask");
            }
        }

        public PERTTask SelectedRemovePretask
        {
            get
            {
                return _SelectedRemovePretask;
            }
            set
            {
                _SelectedRemovePretask = value;
                RaisePropertyChanged("SelectedRemovePretask");
            }
        }

        public ObservableCollection<PERTTask> PERTTaskList
        {
            get
            {
                if (_PERTTaskList == null)
                {
                    _PERTTaskList = new ObservableCollection<PERTTask>();
                    initializePertTaskList();
                }
                resertPostTask();
                setPostTask();

                return _PERTTaskList;
            }
            set
            {
                _PERTTaskList = value;
                RaisePropertyChanged("PERTTaskList");
            }
        }

        public ObservableCollection<NetworkDiagramItem> items
        {
            get
            {
                _items = new ObservableCollection<NetworkDiagramItem>();

                if (PERTTaskResult != null && PERTTaskResult.Count() != 0)
                {
                    List<NetworkDiagramItem> listItem = new List<NetworkDiagramItem>();
                    NetworkDiagramItem itemStart = setFirstNetworkDiagramItem(listItem);
                    setNetworkDiagramItems(listItem);
                    NetworkDiagramItem lastItem = setLastNetworkDiagramItem(listItem);
                    setDependencies(listItem, itemStart, lastItem);
                    setItemsList(listItem);
                }

                return _items;
            }

            set
            {
                _items = value;
                RaisePropertyChanged("items");
            }
        }







        #endregion


        #region method 

        private void Calculate()
        {
            PERTTaskResult = new ObservableCollection<PERTTask>(pertObject.criticalPath(PERTTaskList.ToList()));
            RaisePropertyChanged("items");
        }

        private void RemovePERTTask(object name)
        {
            string tmpName = name.ToString();
            PERTTask findPERTTask = PERTTaskList.Where(x => x.Name == tmpName).FirstOrDefault();

            foreach (var t in PERTTaskList)
                foreach (var preT in t.PreTask.ToList())
                    if (preT.Name == findPERTTask.Name)
                        t.PreTask.Remove(findPERTTask);

            PERTTaskList.Remove(findPERTTask);
            setVariable();
        }

        private void AddPERTTask()
        {
            bool checkName = true;
            foreach (var t in PERTTaskList)
                if (t.Name == Name)
                    checkName = false;

            if (Name != null && Name.Length > 0 && Optimistic_a > 0 && Pessimistic_b > 0 && MostLikely_m > 0 && checkName == true)
            {
                PERTTask perttask = new PERTTask(Name, Optimistic_a, Pessimistic_b, MostLikely_m, new List<PERTTask>());
                PERTTaskList.Add(perttask);
            }
            else
            {
                ErrorMessage er = new ErrorMessage("The name exists or cost < 1!");
                er.ShowDialog();
            }

            setVariable();
        }

        private void AddPreTask()
        {
            if (SelectedTaskWhereAddThePretask == null || SelectedTaskWhichIsPretask == null)
            {
                ErrorMessage er = new ErrorMessage("Error - null value!");
                er.ShowDialog();
                return;
            }

            if (SelectedTaskWhereAddThePretask.Name == SelectedTaskWhichIsPretask.Name)
            {
                ErrorMessage er = new ErrorMessage("Error - cyclic dependency!");
                er.ShowDialog();
                return;
            }

            foreach (var t in SelectedTaskWhereAddThePretask.PreTask)
                if (t.Name == SelectedTaskWhichIsPretask.Name)
                {
                    ErrorMessage er = new ErrorMessage("Error - the dependency exists!");
                    er.ShowDialog();
                    return;
                }

            SelectedTaskWhereAddThePretask.PreTask.Add(SelectedTaskWhichIsPretask);
            setVariable();
        }

        private void RemovePreTask()
        {
            if (SelectedRemovePretask == null || SelectedTaskWhereRemoveThePretask == null)
            {
                ErrorMessage er = new ErrorMessage("Error - null value!!");
                er.ShowDialog();
                return;
            }

            SelectedTaskWhereRemoveThePretask.PreTask.Remove(SelectedRemovePretask);
            setVariable();
        }

        private void OpenCocomoBasic()
        {
            CocomoBasicView CBV = new CocomoBasicView();
            CBV.ShowDialog();
        }

        private void OpenCocomoIntermediate()
        {
            CocomoIntermediateView CBV = new CocomoIntermediateView();
            CBV.ShowDialog();
        }

        private void OpenCocomoDetail()
        {
            CocomoDetailView CDV = new CocomoDetailView();
            CDV.ShowDialog();
        }

        private void OpenFunctionPoint()
        {
            FunctionPointMethodView FPV = new FunctionPointMethodView();
            FPV.ShowDialog();
        }

        private void OpenCocomoIIStageI()
        {
            CocomoIIStageIView CSV = new CocomoIIStageIView();
            CSV.ShowDialog();
        }

        private void OpenCocomoIIStageII()
        {
            CocomoIIStageIIView CSV = new CocomoIIStageIIView();
            CSV.ShowDialog();
        }

        private void OpenCocomoIIStageIII()
        {
            CocomoIIStageIIIView CSV = new CocomoIIStageIIIView();
            CSV.ShowDialog();
        }

        #endregion


        #region helpMethod

        private void initializePertTaskList()
        {
            PERTTask A = new PERTTask("A", 1, 2, 3, new List<PERTTask> { });
            PERTTask B = new PERTTask("B", 2, 3, 4, new List<PERTTask> { });
            PERTTask C = new PERTTask("C", 1, 2, 3, new List<PERTTask> { A });
            PERTTask D = new PERTTask("D", 1, 2, 3, new List<PERTTask> { A });
            PERTTask E = new PERTTask("E", 3, 4, 5, new List<PERTTask> { B });
            PERTTask F = new PERTTask("F", 2, 4, 6, new List<PERTTask> { D, E });
            PERTTask G = new PERTTask("G", 1, 3, 5, new List<PERTTask> { C });
            PERTTask H = new PERTTask("H", 3, 5, 7, new List<PERTTask> { C });
            PERTTask I = new PERTTask("I", 5, 7, 9, new List<PERTTask> { F, H });

            _PERTTaskList.Add(A);
            _PERTTaskList.Add(B);
            _PERTTaskList.Add(C);
            _PERTTaskList.Add(D);
            _PERTTaskList.Add(E);
            _PERTTaskList.Add(F);
            _PERTTaskList.Add(G);
            _PERTTaskList.Add(H);
            _PERTTaskList.Add(I);
        }

        private void resertPostTask()
        {
            foreach (var t in _PERTTaskList)
                t.PostTask = new List<PERTTask>();
        }

        private void setPostTask()
        {
            for (int i = 0; i < _PERTTaskList.Count(); i++)
            {
                if (_PERTTaskList[i].PreTask.Count() == 0)
                    continue;

                for (int j = 0; j < _PERTTaskList[i].PreTask.Count(); j++)
                {
                    PERTTask tmp = _PERTTaskList[i].PreTask[j];
                    tmp.PostTask.Add(_PERTTaskList[i]);
                }
            }
        }

        private NetworkDiagramItem setFirstNetworkDiagramItem(List<NetworkDiagramItem> listItem)
        {
            NetworkDiagramItem itemStart = new NetworkDiagramItem { DisplayedText = "Start", Content = "Start" };
            itemStart.Effort = TimeSpan.Zero;
            itemStart.EarlyStart = StartProject;
            itemStart.EarlyFinish = StartProject;
            itemStart.LateStart = StartProject;
            itemStart.LateFinish = StartProject;
            itemStart.Slack = TimeSpan.Zero;
            listItem.Add(itemStart);

            return itemStart;
        }

        private void setNetworkDiagramItems(List<NetworkDiagramItem> listItem)
        {
            for (int i = 0; i < PERTTaskResult.Count(); i++)
            {
                NetworkDiagramItem item = new NetworkDiagramItem { DisplayedText = PERTTaskResult[i].Name, Content = PERTTaskResult[i].Name };

                item.Effort = TimeSpan.Parse(PERTTaskResult[i].Expected_t + ":00:00:00");
                item.EarlyStart = StartProject.AddDays(PERTTaskResult[i].ES);
                item.EarlyFinish = StartProject.AddDays(PERTTaskResult[i].EF);
                item.LateStart = StartProject.AddDays(PERTTaskResult[i].LS);
                item.LateFinish = StartProject.AddDays(PERTTaskResult[i].LF);
                item.Slack = item.LateStart - item.EarlyStart;

                listItem.Add(item);
            }
        }


        private NetworkDiagramItem setLastNetworkDiagramItem(List<NetworkDiagramItem> listItem)
        {
            int maxCS = PERTTaskResult.Max(x => x.CS);
            PERTTask tmpPertTask = PERTTaskResult.Where(x => x.CS == maxCS).FirstOrDefault();
            NetworkDiagramItem lastItem = new NetworkDiagramItem { DisplayedText = "End", Content = "End" };
            lastItem.Effort = TimeSpan.Zero;
            lastItem.EarlyStart = StartProject.AddDays(maxCS);
            lastItem.EarlyFinish = StartProject.AddDays(maxCS);
            lastItem.LateStart = StartProject.AddDays(maxCS);
            lastItem.LateFinish = StartProject.AddDays(maxCS);
            lastItem.Slack = TimeSpan.Zero;
            listItem.Add(lastItem);

            return lastItem;
        }

        private void setVariable()
        {
            //sztuczka zeby odswierzyly sie pretaski
            ObservableCollection<PERTTask> tmp = new ObservableCollection<PERTTask>(PERTTaskList);
            PERTTaskList = new ObservableCollection<PERTTask>(tmp);

            TaskWhichIsPretask = new ObservableCollection<PERTTask>(PERTTaskList);
            TaskWhereAddThePretask = new ObservableCollection<PERTTask>(PERTTaskList);
            SelectedTaskWhereAddThePretask = null;
            SelectedTaskWhichIsPretask = null;

            TaskWhereRemoveThePretask = new ObservableCollection<PERTTask>(PERTTaskList.Where(x => x.PreTask.Count() > 0));
            RemovePretask = null;
            SelectedRemovePretask = null;
            SelectedTaskWhereRemoveThePretask = null;

            Name = "";
            Pessimistic_b = 0;
            Optimistic_a = 0;
            MostLikely_m = 0;
        }

        private void setDependencies(List<NetworkDiagramItem> listItem, NetworkDiagramItem itemStart, NetworkDiagramItem lastItem)
        {
            for (int i = 0; i < PERTTaskResult.Count(); i++)
            {
                List<PERTTask> poprzednicy = PERTTaskResult[i].PreTask;

                if (poprzednicy.Count() == 0)
                {
                    NetworkDiagramItem initItem = listItem.Where(x => x.DisplayedText == PERTTaskResult[i].Name).FirstOrDefault();
                    initItem.Predecessors.Add(new NetworkPredecessorItem { Item = itemStart });
                }

                if (PERTTaskResult[i].PostTask.Count() == 0)
                {
                    NetworkDiagramItem endItem = listItem.Where(x => x.DisplayedText == PERTTaskResult[i].Name).FirstOrDefault();
                    lastItem.Predecessors.Add(new NetworkPredecessorItem { Item = endItem });
                }

                NetworkDiagramItem itemCoMaPoprzednika = listItem.Where(x => x.DisplayedText == PERTTaskResult[i].Name).FirstOrDefault();

                for (int j = 0; j < poprzednicy.Count(); j++)
                {
                    NetworkDiagramItem poprzednik = listItem.Where(x => x.DisplayedText == poprzednicy[j].Name).FirstOrDefault();
                    itemCoMaPoprzednika.Predecessors.Add(new NetworkPredecessorItem { Item = poprzednik });
                }
            }
        }

        private void setItemsList(List<NetworkDiagramItem> listItem)
        {
            for (int i = 0; i < listItem.Count(); i++)
                _items.Add(listItem[i]);
        }

        private void CalculateProbabilityOfCompletingDays()
        {
            double allvarianceCP = 0;
            int maxCost = 0;

            foreach (PERTTask t in PERTTaskResult)
                if (t.CC == true)
                {
                    allvarianceCP += t.Variance;
                    maxCost += t.Expected_t;
                }

            double wynik = (ProjectDaysTime - maxCost) / Math.Sqrt(allvarianceCP);

            ProbabilityOfCompletingDays = Math.Round(NormalDistribution.calculateNormalDistribution(wynik), 2);

            ProbabilityOfCompletingDays *= 100;
        }


        

        private void CalculateProbabilityOfCompletingPercent()
        {


            double allvarianceCP = 0;
            int maxCost = 0;

            foreach (PERTTask t in PERTTaskResult)
                if (t.CC == true)
                {
                    allvarianceCP += t.Variance;
                    maxCost += t.Expected_t;
                }


            for (double day = 0; day < maxCost + 50; day += 0.1)
            {
                double wynik = (day - maxCost) / Math.Sqrt(allvarianceCP);
                ProbabilityOfCompletingPercent = Math.Round(NormalDistribution.calculateNormalDistribution(wynik), 2);
                ProbabilityOfCompletingPercent *= 100;
                if (ProbabilityOfCompletingPercent >= ProjectPercentTime)
                {
                    ProbabilityOfCompletingPercent = Math.Round(day, 2);
                    break;
                }     
            }
        }
        #endregion
    }
}
