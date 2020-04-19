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
        public ICommand OpenCocomoDetailButton { get; set; }
        public ICommand OpenFunctionPointButton { get; set; }
        public ICommand OpenCocomoIIStageIButton { get; set; }
        public ICommand OpenCocomoIIStageIIButton { get; set; }
        public ICommand OpenCocomoIIStageIIIButton { get; set; }
        #endregion


        #region constructor

        public ProjectManagerViewModel()
        {
            OpenCocomoBasicButton = new DelegateCommand(OpenCocomoBasic);
            OpenCocomoIntermediateButton = new DelegateCommand(OpenCocomoIntermediate);
            OpenCocomoDetailButton = new DelegateCommand(OpenCocomoDetail);

            //OpenFunctionPointButton = new DelegateCommand(OpenFunctionPoint);

            //OpenCocomoIIStageIButton = new DelegateCommand(OpenCocomoIIStageI);
            //OpenCocomoIIStageIIButton = new DelegateCommand(OpenCocomoIIStageII);
            //OpenCocomoIIStageIIIButton = new DelegateCommand(OpenCocomoIIStageIII);
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

        private void OpenCocomoDetail()
        {
            CocomoDetailView CDV = new CocomoDetailView();
            CDV.ShowDialog();
        }

        //private void OpenFunctionPoint()
        //{
        //    FunctionPointView FPV = new FunctionPointView();
        //    FPV.ShowDialog();
        //}

        //private void OpenCocomoIIStageI()
        //{
        //    CocomoIIStageIView CSV = new CocomoIIStageIView();
        //    CBV.ShowDialog();
        //}

        //private void OpenCocomoIIStageII()
        //{
        //    CocomoIIStageIIView CSV = new CocomoIIStageIIView();
        //    CBV.ShowDialog();
        //}

        //private void OpenCocomoIIStageIII()
        //{
        //    CocomoIIStageIIIView CSV = new CocomoIIStageIIIView();
        //    CBV.ShowDialog();
        //}

    }
}
