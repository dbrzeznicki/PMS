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

namespace PMS
{
    public class HomeManagerViewModel : BindableBase
    {

        #region variable

        PMSContext dbContext = new PMSContext();

        private ObservableCollection<Subtask> _NewAndInProgressSubtasks;

        private ObservableCollection<Project> _ActiveProjects;

        private ObservableCollection<RecentActivity> _RecentActivities;

        private int _AllTask = 0;
        private int _NewTask = 0;
        private int _InProgressTask = 0;

        #endregion


        public ObservableCollection<Subtask> NewAndInProgressSubtasks
        {
            get
            {

                if (_NewAndInProgressSubtasks == null)
                {
                    _NewAndInProgressSubtasks = new ObservableCollection<Subtask>(dbContext.Subtask.Where(x => (x.UserID == Global.user.UserID) && ((x.SubtaskStatus.Name == "New") || (x.SubtaskStatus.Name == "In progress"))));
                }
                return _NewAndInProgressSubtasks;
            }
            set
            {
                _NewAndInProgressSubtasks = value;
                RaisePropertyChanged("NewAndInProgressSubtasks");
            }
        }

        public ObservableCollection<Project> ActiveProjects
        {
            get
            {

                if (_ActiveProjects == null)
                {
                    _ActiveProjects = new ObservableCollection<Project>(dbContext.Project.Where(c => (c.TeamID == Global.user.TeamID) && (c.ProjectStatus.Name == "In progress")));
                }
                return _ActiveProjects;
            }
            set
            {
                _ActiveProjects = value;
                RaisePropertyChanged("ActiveProjects");
            }
        }

        public ObservableCollection<RecentActivity> RecentActivities
        {
            get
            {

                if (_RecentActivities == null)
                {
                    _RecentActivities = new ObservableCollection<RecentActivity>(dbContext.RecentActivity.Where(c => c.TeamID == Global.user.TeamID)
                                                                                .OrderByDescending(x => x.DateAdded).Take(15));
                }
                return _RecentActivities;
            }
            set
            {
                _RecentActivities = value;
                RaisePropertyChanged("RecentActivities");
            }
        }

        public int AllTask
        {
            get
            {
                _AllTask = dbContext.Subtask.Where(x => (x.UserID == Global.user.UserID) && ((x.SubtaskStatus.Name == "New") || (x.SubtaskStatus.Name == "In progress"))).Count();
                return _AllTask;
            }
        }


        public int NewTask
        {
            get
            {
                _NewTask = dbContext.Subtask.Where(x => (x.UserID == Global.user.UserID) && (x.SubtaskStatus.Name == "New")).Count();
                return _NewTask;
            }
        }


        public int InProgressTask
        {
            get
            {
                _InProgressTask = dbContext.Subtask.Where(x => (x.UserID == Global.user.UserID) && (x.SubtaskStatus.Name == "In progress")).Count();
                return _InProgressTask;
            }
        }


    }
}
