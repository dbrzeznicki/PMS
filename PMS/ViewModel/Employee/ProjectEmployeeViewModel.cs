using PMS.DAL;
using PMS.Model;
using PMS.ViewModel;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using OxyPlot;
using OxyPlot.Series;
using DlhSoft.Windows.Controls;

namespace PMS
{
    public class ProjectEmployeeViewModel : BindableBase
    {

        #region variable

        PMSContext dbContext = new PMSContext();

        private ObservableCollection<Project> _Projects;
        private Project _SelectedProject;
        private PlotModel _MyTaskPieChart = new PlotModel { Title = "My task" };
        private PlotModel _ProgressPieChart = new PlotModel { Title = "Progress" };
        private string _ProgressPercentage = "";
        private ObservableCollection<GanttChartItem> _Items;

        #endregion


        #region properties
        public ObservableCollection<Project> Projects
        {
            get
            {
                _Projects = new ObservableCollection<Project>(dbContext.Project.Where(c => c.TeamID == Global.user.TeamID));

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
                RaisePropertyChanged("SelectedProject");
                setChartMyTask();
                setProgressTask();
                refreshGanttChart();
            }
        }

        public PlotModel MyTaskPieChart
        {
            get
            {
                //piechart tworzy sie poprzez metode w selectedproject
                return _MyTaskPieChart;
            }
            set
            {
                _MyTaskPieChart = value;
                RaisePropertyChanged("MyTaskPieChart");
            }
        }

        public PlotModel ProgressPieChart
        {
            get
            {
                return _ProgressPieChart;
            }
            set
            {
                _ProgressPieChart = value;
                RaisePropertyChanged("ProgressPieChart");
            }
        }

        public string ProgressPercentage
        {
            get
            {
                return _ProgressPercentage;
            }
            set
            {
                _ProgressPercentage = value;
                RaisePropertyChanged("ProgressPercentage");
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


        #endregion




        private void setChartMyTask()
        {

            Project project = dbContext.Project.Where(x => x.Name == SelectedProject.Name).SingleOrDefault();
            List<MainTask> tmpMaintask = new List<MainTask>(dbContext.MainTask.Where(x => x.ProjectID == project.ProjectID));
            List<Subtask> SubtasksTMP = new List<Subtask>();
            foreach (MainTask mt in tmpMaintask)
            {
                SubtasksTMP = SubtasksTMP.Concat(mt.Subtasks).ToList();
            }
            List<Subtask> subtasks;
            subtasks = new List<Subtask>(SubtasksTMP.Where(x => (x.UserID == Global.user.UserID)));

            int newTask = subtasks.Where(x => (x.UserID == Global.user.UserID) && (x.SubtaskStatus.Name == "Nowe")).Count();
            int inProgressTask = subtasks.Where(x => (x.UserID == Global.user.UserID) && (x.SubtaskStatus.Name == "W trakcie")).Count();

            _MyTaskPieChart = new PlotModel { Title = "My task", PlotMargins = new OxyThickness(20, 20, 20, 20) };

            PieSeries seriesP1 = new PieSeries { StrokeThickness = 2.0, InsideLabelPosition = 0.6, AngleSpan = 360, StartAngle = 0 };

            if (newTask != 0 && inProgressTask != 0)
            {
                seriesP1.Slices.Add(new PieSlice("New tasks", newTask) { IsExploded = true });
                seriesP1.Slices.Add(new PieSlice("In progress tasks", inProgressTask) { IsExploded = true });
            }
            else if (newTask != 0 && inProgressTask == 0)
            {
                seriesP1.Slices.Add(new PieSlice("New tasks", newTask) { IsExploded = true });
            }
            else if (newTask == 0 && inProgressTask != 0)
            {
                seriesP1.Slices.Add(new PieSlice("In progress tasks", inProgressTask) { IsExploded = true });
            }
            else if (newTask == 0 && inProgressTask == 0)
            {
                seriesP1.Slices.Add(new PieSlice("No tasks", 0) { IsExploded = true });
            }

            _MyTaskPieChart.Series.Add(seriesP1);
            //PieChart.InvalidatePlot(true);
            RaisePropertyChanged("MyTaskPieChart");
        }


        private void setProgressTask()
        {

            _ProgressPieChart = new PlotModel { Title = "Progress", PlotMargins = new OxyThickness(20, 20, 20, 20), IsLegendVisible = false };

            PieSeries seriesP1 = new PieSeries
            {
                StrokeThickness = 2.0,
                AngleSpan = 360,
                StartAngle = 270,
                InnerDiameter = 0.4,
                OutsideLabelFormat = "",
                TickHorizontalLength = 0.00,
                TickRadialLength = 0.00,
                InsideLabelFormat = ""
            };



            Project project = dbContext.Project.Where(x => x.Name == SelectedProject.Name).SingleOrDefault();
            int allMainTask = project.MainTasks.Count();

            int trueMainTask = project.MainTasks.Where(x => x.Status == true).Count();

            int truePercetage = (trueMainTask * 100) / allMainTask;

            ProgressPercentage = truePercetage + "%";

            seriesP1.Slices.Add(new PieSlice("Success", truePercetage) { });
            seriesP1.Slices.Add(new PieSlice("No success", 100 - truePercetage) { });

            _ProgressPieChart.Series.Add(seriesP1);
            RaisePropertyChanged("ProgressPieChart");
        }





        




        public void refreshGanttChart()
        {

            Project project = dbContext.Project.Where(x => x.Name == SelectedProject.Name).SingleOrDefault();
            List<MainTask> tmpMaintask = new List<MainTask>(dbContext.MainTask.Where(x => x.ProjectID == project.ProjectID));
            List<Subtask> SubtasksTMP = new List<Subtask>();
            foreach (MainTask mt in tmpMaintask)
            {
                SubtasksTMP = SubtasksTMP.Concat(mt.Subtasks).ToList();
            }
            List<Subtask> subtasks;
            
            subtasks = new List<Subtask>(SubtasksTMP.Where(x => (x.UserID == Global.user.UserID)));



            _Items = new ObservableCollection<GanttChartItem>();

            foreach(var a in subtasks)
            {
                _Items.Add(new GanttChartItem { Content = a.Name, Start = a.StartTime, Finish = a.EndTime, IsBarReadOnly=true });
            }



                //{
                //    new GanttChartItem { Content = "Task 1" },
                //    new GanttChartItem { Content = "Task 1.1", Indentation = 1, Start = new DateTime(2020, 03, 12), Finish = new DateTime(2020, 03, 15)},
                //    new GanttChartItem { Content = "Task 1.2", Indentation = 1, Start = new DateTime(2020, 03, 16), Finish = new DateTime(2020, 03, 20)},
                //    new GanttChartItem { Content = "Task 2" },
                //    new GanttChartItem { Content = "Task 2.1", Indentation = 1, Start = new DateTime(2020, 03, 20), Finish = new DateTime(2020, 03, 22)},
                //    new GanttChartItem { Content = "Task 2.2", Indentation = 1}, //czarna kreska, a pod nią to co na dola az do kolejnego itemu bez daty
                //    new GanttChartItem { Content = "Task 2.2.1", Indentation = 2, Start = new DateTime(2020, 03, 23), Finish = new DateTime(2020, 03, 27)},
                //    new GanttChartItem { Content = "Task 2.2.2", Indentation = 2, Start = new DateTime(2020, 03, 28)},   //, IsMilestone = true
                //};



            RaisePropertyChanged("Items");
            //items[2].Predecessors.Add(new PredecessorItem { Item = items[1]});
        }






    }
}
