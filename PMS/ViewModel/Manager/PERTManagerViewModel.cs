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
        private ObservableCollection<PERTTask> _PERTTaskList = new ObservableCollection<PERTTask>();
        private DateTime _StartProject = DateTime.Now;

        private PERT pertObject = new PERT();


        #endregion



        #region command

        public ICommand CalculateButton { get; set; }


        #endregion


        #region constructor

        public PERTManagerViewModel()
        {
            CalculateButton = new DelegateCommand(Calculate);
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


        public ObservableCollection<PERTTask> PERTTaskList
        {
            get
            {
                ObservableCollection<PERTTask> _PERTTaskList = new ObservableCollection<PERTTask>();


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


        public void Calculate()
        {
            PERTTaskResult = new ObservableCollection<PERTTask>(pertObject.criticalPath(PERTTaskList.ToList()));
            RaisePropertyChanged("items");

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
                            continue;
                        }

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
