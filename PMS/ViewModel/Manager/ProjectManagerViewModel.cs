using PMS.Algorithm;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
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

        public ICommand OpenCocomoBasicButton { get; set; }
        public ICommand OpenCocomoIntermediateButton { get; set; }
        #endregion


        #region constructor

        public ProjectManagerViewModel()
        {
            OpenCocomoBasicButton = new DelegateCommand(OpenCocomoBasic);
            OpenCocomoIntermediateButton = new DelegateCommand(OpenCocomoIntermediate);
        }

        #endregion


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

    }
}
