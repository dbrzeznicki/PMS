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

        #endregion


        #region constructor

        public ProjectManagerViewModel()
        {
            OpenCocomoBasicButton = new DelegateCommand(OpenCocomoBasic);
        }

        #endregion


        private void OpenCocomoBasic()
        {
            MessageBox.Show("ddddd");
            CocomoBasicView CBV = new CocomoBasicView();
            //App.Current.MainWindow.Close();
            //App.Current.MainWindow = CBV;
            CBV.ShowDialog();
        }

    }
}
