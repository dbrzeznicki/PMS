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
using System.Windows.Input;

namespace PMS
{
    public class ProjectManagerViewModel : BindableBase
    {

        #region variables

        #endregion


        #region command

        public ICommand ShowAddProjectButton { get; set; }

        #endregion


        #region constructor
        public ProjectManagerViewModel()
        {
            ShowAddProjectButton = new DelegateCommand(ShowAddProject);

        }

        #endregion

        #region properties

        #endregion


        #region methods
        private void ShowAddProject()
        {
            AddProjectWindow FPV = new AddProjectWindow();
            FPV.Show();
        }
        #endregion

    }
}
