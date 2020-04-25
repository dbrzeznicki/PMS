using DlhSoft.Windows.Controls.Pert;
using PMS.Algorithm;
using PMS.Model;
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


        private ObservableCollection<NetworkDiagramItem> _items = new ObservableCollection<NetworkDiagramItem>();
        private ObservableCollection<PERTTask> _PERTTaskResult;
        private ObservableCollection<PERTTask> _PERTTaskList;
        private DateTime _StartProject = DateTime.Now;

        private PERT pertObject = new PERT();


        //dodanie PERTtaska
        private string _Name = "";
        private int _Cost = 0;

        //add pretask
        private ObservableCollection<PERTTask> _TaskWhereAddThePretask;
        private PERTTask _SelectedTaskWhereAddThePretask;

        private ObservableCollection<PERTTask> _TaskWhichIsPretask;
        private PERTTask _SelectedTaskWhichIsPretask;


        //remove pretask
        private ObservableCollection<PERTTask> _TaskWhereRemoveThePretask = null;
        private PERTTask _SelectedTaskWhereRemoveThePretask = null;

        private ObservableCollection<PERTTask> _RemovePretask = null;
        private PERTTask _SelectedRemovePretask = null;

        #endregion



        #region command

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
        }

        #endregion

        public ObservableCollection<PERTTask> PERTTaskResult
        {
            get
            {
                if (_PERTTaskResult == null)
                {
                    _PERTTaskResult = new ObservableCollection<PERTTask>();
                }
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
                {
                    _StartProject = DateTime.Now;
                }
                return _StartProject;
            }
            set
            {
                _StartProject = value;
                RaisePropertyChanged("StartProject");
            }
        }

        public int Cost
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
                {
                    _TaskWhereAddThePretask = new ObservableCollection<PERTTask>(PERTTaskList);
                }
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
            get { return _SelectedTaskWhereAddThePretask; }
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
                {
                    _TaskWhichIsPretask = new ObservableCollection<PERTTask>(PERTTaskList);
                }
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
            get { return _SelectedTaskWhichIsPretask; }
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
                {
                    _TaskWhereRemoveThePretask = new ObservableCollection<PERTTask>(PERTTaskList.Where(x=>x.PreTask.Count() > 0));
                }
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
            get { return _SelectedTaskWhereRemoveThePretask; }
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
                {
                    _RemovePretask = new ObservableCollection<PERTTask>();
                }
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
            get { return _SelectedRemovePretask; }
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
                    PERTTask C = new PERTTask("C", 5, new List<PERTTask> { });
                    PERTTask B = new PERTTask("B", 10, new List<PERTTask> { });
                    PERTTask A = new PERTTask("A", 12, new List<PERTTask> { });
                    PERTTask D = new PERTTask("D", 7, new List<PERTTask> { B, C });
                    PERTTask G = new PERTTask("G", 4, new List<PERTTask> { A });
                    PERTTask F = new PERTTask("F", 6, new List<PERTTask> { A });
                    PERTTask E = new PERTTask("E", 8, new List<PERTTask> { B });
                    PERTTask H = new PERTTask("H", 9, new List<PERTTask> { G });
                    PERTTask I = new PERTTask("I", 7, new List<PERTTask> { E, F, H });
                    PERTTask J = new PERTTask("J", 10, new List<PERTTask> { D, I });



                    _PERTTaskList.Add(A);
                    _PERTTaskList.Add(B);
                    _PERTTaskList.Add(C);
                    _PERTTaskList.Add(D);
                    _PERTTaskList.Add(E);
                    _PERTTaskList.Add(F);
                    _PERTTaskList.Add(G);
                    _PERTTaskList.Add(H);
                    _PERTTaskList.Add(I);
                    _PERTTaskList.Add(J);

                }

                //wyzerowanie posttask
                foreach (var t in _PERTTaskList)
                {
                    t.PostTask = new List<PERTTask>();
                }


                //ustawienie posttask
                for (int i = 0; i < _PERTTaskList.Count(); i++)
                {
                    if (_PERTTaskList[i].PreTask.Count() == 0)
                        continue;

                    for (int j = 0; j < _PERTTaskList[i].PreTask.Count(); j++)
                    {
                        //allTasks[i] przetrzymuje aktualne zadanie, któe przeszukujemy
                        PERTTask tmp = _PERTTaskList[i].PreTask[j];
                        tmp.PostTask.Add(_PERTTaskList[i]);
                    }
                }


                return _PERTTaskList;
            }
            set
            {
                _PERTTaskList = value;
                RaisePropertyChanged("PERTTaskList");
            }
        }


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
            {
                foreach (var preT in t.PreTask.ToList())
                {
                    if (preT.Name == findPERTTask.Name)
                        t.PreTask.Remove(findPERTTask);
                }
            }

            PERTTaskList.Remove(findPERTTask);

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
            Cost = 0;

        }


        private void AddPERTTask()
        {
            bool checkName = true;
            foreach (var t in PERTTaskList)
            {

                if (t.Name == Name)
                    checkName = false;
            }

            if (Name != null && Name.Length > 0 && Cost > 0 && checkName == true)
            {

                PERTTask perttask = new PERTTask(Name, Cost, new List<PERTTask>());
                PERTTaskList.Add(perttask);
            }
            else
            {
                ErrorMessage er = new ErrorMessage("The name exists or cost < 1!");
                er.ShowDialog();
            }

            TaskWhichIsPretask = new ObservableCollection<PERTTask>(PERTTaskList);
            TaskWhereAddThePretask = new ObservableCollection<PERTTask>(PERTTaskList);
            SelectedTaskWhereAddThePretask = null;
            SelectedTaskWhichIsPretask = null;

            TaskWhereRemoveThePretask = new ObservableCollection<PERTTask>(PERTTaskList.Where(x => x.PreTask.Count() > 0));
            RemovePretask = null;
            SelectedRemovePretask = null;
            SelectedTaskWhereRemoveThePretask = null;

            Name = "";
            Cost = 0;
        }




        private void AddPreTask()
        {
            if (SelectedTaskWhereAddThePretask==null || SelectedTaskWhichIsPretask == null)
            {
                ErrorMessage er = new ErrorMessage("Error 1!");
                er.ShowDialog();
                return;
            }

            if (SelectedTaskWhereAddThePretask.Name == SelectedTaskWhichIsPretask.Name)
            {
                ErrorMessage er = new ErrorMessage("Error 2!");
                er.ShowDialog();
                return;
            }

            foreach (var t in SelectedTaskWhereAddThePretask.PreTask)
            {
                if (t.Name == SelectedTaskWhichIsPretask.Name)
                {
                    ErrorMessage er = new ErrorMessage("Error 3!");
                    er.ShowDialog();
                    return;
                }
            }

            SelectedTaskWhereAddThePretask.PreTask.Add(SelectedTaskWhichIsPretask);

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
            Cost = 0;
        }






        private void RemovePreTask ()
        {
            if (SelectedRemovePretask == null || SelectedTaskWhereRemoveThePretask == null)
            {
                ErrorMessage er = new ErrorMessage("Error 1!");
                er.ShowDialog();
                return;
            }

            SelectedTaskWhereRemoveThePretask.PreTask.Remove(SelectedRemovePretask);

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
            Cost = 0;
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









































        public ObservableCollection<NetworkDiagramItem> items
        {
            get
            {
                _items = new ObservableCollection<NetworkDiagramItem>();


                List<NetworkDiagramItem> listItem = new List<NetworkDiagramItem>();



                if (PERTTaskResult != null && PERTTaskResult.Count() != 0)
                {

                    for (int i = 0; i < PERTTaskResult.Count(); i++)
                    {
                        NetworkDiagramItem item = new NetworkDiagramItem { DisplayedText = PERTTaskResult[i].Name, Content = PERTTaskResult[i].Name };

                        item.Effort = TimeSpan.Parse(PERTTaskResult[i].Cost + ":00:00:00");
                        item.EarlyStart = StartProject.AddDays(PERTTaskResult[i].ES);
                        item.EarlyFinish = StartProject.AddDays(PERTTaskResult[i].EF);
                        item.LateStart = StartProject.AddDays(PERTTaskResult[i].LS);
                        item.LateFinish = StartProject.AddDays(PERTTaskResult[i].LF);
                        item.Slack = item.LateStart - item.EarlyStart;
                        item.AssignmentsContent = "Tekst po najechaniu";

                        listItem.Add(item);
                    }



                    //add first

                    NetworkDiagramItem itemStart = new NetworkDiagramItem { DisplayedText = "Start", Content = "Start" };
                    itemStart.Effort = TimeSpan.Zero;
                    itemStart.EarlyStart = StartProject;
                    itemStart.EarlyFinish = StartProject;
                    itemStart.LateStart = StartProject;
                    itemStart.LateFinish = StartProject;
                    itemStart.Slack = TimeSpan.Zero;
                    itemStart.AssignmentsContent = "Tekst po najechaniu";
                    listItem.Add(itemStart);

                    //add last
                    int maxCS = PERTTaskResult.Max(x => x.CS);
                    PERTTask tmpPertTask = PERTTaskResult.Where(x => x.CS == maxCS).FirstOrDefault();
                    NetworkDiagramItem lastItem = new NetworkDiagramItem { DisplayedText = "End", Content = "End" };
                    lastItem.Effort = TimeSpan.Zero;
                    lastItem.EarlyStart = StartProject.AddDays(maxCS);
                    lastItem.EarlyFinish = StartProject.AddDays(maxCS);
                    lastItem.LateStart = StartProject.AddDays(maxCS);
                    lastItem.LateFinish = StartProject.AddDays(maxCS);
                    lastItem.Slack = TimeSpan.Zero;
                    lastItem.AssignmentsContent = "End";
                    listItem.Add(lastItem);

                    //w listItem mamy teraz wszystkie network item ale bez poprzednikow


                    //w pętli znów przeszukujemy wyniki z algorytmu
                    //pobieramy poprzednikow z wynikow
                    //jeseli 0 to dalej
                    //jesli nie null to wyszukujemy takie itemdiagram gdzie nazwy sa takie same i dodajemy   DLA WYGLADU
                    for (int i = 0; i < PERTTaskResult.Count(); i++)
                    {
                        List<PERTTask> poprzednicy = PERTTaskResult[i].PreTask;

                        if (poprzednicy.Count() == 0)
                        {
                            NetworkDiagramItem initItem = listItem.Where(x => x.DisplayedText == PERTTaskResult[i].Name).FirstOrDefault();
                            initItem.Predecessors.Add(new NetworkPredecessorItem { Item = itemStart });
                             //BLAD PRZY DODANIU 43 PO CONTINUE WYCHODZI Z PETLI I DALEJ NIE DOCHODZI
                        }


                        //tu blad w dodawaniu end 
                        if (PERTTaskResult[i].PostTask.Count() == 0)
                        {
                            NetworkDiagramItem endItem = listItem.Where(x => x.DisplayedText == PERTTaskResult[i].Name).FirstOrDefault();



                            lastItem.Predecessors.Add(new NetworkPredecessorItem { Item = endItem });
                        }



                        NetworkDiagramItem itemCoMaPoprzednika = listItem.Where(x => x.DisplayedText == PERTTaskResult[i].Name).FirstOrDefault();

                        for (int j = 0; j < poprzednicy.Count(); j++)
                        {
                            //item6.Predecessors.Add(new NetworkPredecessorItem { Item = item5 });

                            NetworkDiagramItem poprzednik = listItem.Where(x => x.DisplayedText == poprzednicy[j].Name).FirstOrDefault();
                            itemCoMaPoprzednika.Predecessors.Add(new NetworkPredecessorItem { Item = poprzednik });
                        }
                    }



                    //tu jeszcze bedzie dodanie all itemow do wykresy

                    for (int i = 0; i < listItem.Count(); i++)
                    {
                        _items.Add(listItem[i]);
                    }

                }

                return _items;
            }

            set
            {
                _items = value;
                RaisePropertyChanged("items");
            }
        }


    }
}
