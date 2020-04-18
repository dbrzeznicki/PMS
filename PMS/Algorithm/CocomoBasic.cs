using PMS.Validation;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PMS.Algorithm
{
    public class CocomoBasic : BindableBase
    {

        #region variables

        private double _Effort = 0;
        private double _Time = 0;
        private double _Staff = 0;
        private double _Productivity = 0;
        private int _KLOC;

        private List<string> _ProjectTypes;
        private string _SelectedProjectType = "Organic";

        private int model = 0;
        private double[,] table = new double[3, 4]
{
            {2.4, 1.05, 2.5, 0.38}, // wiersz o indeksie 0
            {3.0,1.12,2.5,0.35},
            {3.6,1.20,2.5,0.32}
        };

        #endregion


        #region command

        public ICommand CocomoBasicButton { get; set; }

        #endregion


        #region constructor

        public CocomoBasic()
        {
            CocomoBasicButton = new DelegateCommand(CocomoBasicA);
        }

        #endregion

        #region properies

        public double Effort
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

        public double Time
        {
            get
            {
                return _Time;
            }
            set
            {
                _Time = value;
                RaisePropertyChanged("Time");
            }
        }

        public double Staff
        {
            get
            {
                return _Staff;
            }
            set
            {
                _Staff = value;
                RaisePropertyChanged("Staff");
            }
        }

        public double Productivity
        {
            get
            {
                return _Productivity;
            }
            set
            {
                _Productivity = value;
                RaisePropertyChanged("Productivity");
            }
        }

        public int KLOC
        {
            get
            {
                return _KLOC;
            }
            set
            {
                _KLOC = value;
                RaisePropertyChanged("KLOC");
            }
        }

        public List<string> ProjectTypes
        {
            get
            {
                _ProjectTypes = new List<string> { "Organic", "Semi-detached", "Embedded" };

                return _ProjectTypes;
            }
        }

        public string SelectedProjectType
        {
            get
            {
                if (_SelectedProjectType == "Organic")
                    model = 0;
                else if (_SelectedProjectType == "Semi-detached")
                    model = 1;
                else if (_SelectedProjectType == "Embedded")
                    model = 2;

                return _SelectedProjectType;
            }
            set
            {
                _SelectedProjectType = value;
                RaisePropertyChanged("SelectedProjectType");
            }
        }
        #endregion


        private void CocomoBasicA()
        {
            AlgorithmValidation AV = new AlgorithmValidation();
            bool correctForm = AV.CocomoBasicValidation(KLOC);

            if (correctForm)
            {
                Effort = table[model, 0] * Math.Pow(_KLOC, table[model, 1]);
                Time = table[model, 2] * Math.Pow(_Effort, table[model, 3]);
                Staff = _Effort / _Time;
                Productivity = _KLOC / _Effort;
            }

        }
    }
}
