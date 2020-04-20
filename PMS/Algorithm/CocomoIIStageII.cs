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
    public class CocomoIIStageII : BindableBase
    {
        #region variable

        private int _KLOC = 0;
        private double _AdjustedPM = 0;

        private List<string>[] _RateScale = new List<string>[5];
        private string[] _SelectedRateScale = null;
        private double[] selectedCostRateScale = new double[5];

        private List<string>[] _RateCostDriver = new List<string>[7];
        private string[] _SelectedRateCostDriver = null;
        private double[] selectedCostRateCostDriver = new double[7];

        int rating = 0;
        
        double[,] scaleTable = new double[5,5]
        {
                {6.20,4.96,3.74,2.48,1.24},
                {5.07,4.05,3.04,2.03,1.01},
                {7.07,5.65,4.24,2.83,1.41},
                {5.48,4.38,3.29,2.19,1.10},
                {7.80,6.24,4.68,3.12,1.56}
        };

        double[,] driverTable= new double[7, 7]
        {
            {0.73,0.81,0.98,1,1.30,1.74,2.38},
            {-1,-1,0.95,1,1.07,1.15,1.24},
            {-1,-1,0.87,1,1.29,1.81,2.61},
            {2.12,1.62,1.26,1,0.83,0.63,0.50},
            {1.59,1.33,1.12,1,0.87,0.71,0.62},
            {1.43,1.30,1.10,1,1,0.73,0.62},
            {-1,1.43,1.14,1,-1,1,-1}
        };
        #endregion


        #region command

        public ICommand CocomoIIStageIIButton { get; set; }

        #endregion


        #region constructor

        public CocomoIIStageII()
        {
            CocomoIIStageIIButton = new DelegateCommand(CocomoIIStageIIButtonA);
        }

        #endregion


        #region properties

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

        public double AdjustedPM
        {
            get
            {
                return _AdjustedPM;
            }
            set
            {
                _AdjustedPM = value;
                RaisePropertyChanged("AdjustedPM");
            }
        }


        public List<string>[] RateScale
        {
            get
            {
                _RateScale = new List<string>[5];

                for (int i = 0; i < 5; i++)
                        _RateScale[i] = new List<string> { "Very Low", "Low", "Nominal", "High", "Very High"};

                return _RateScale;
            }
        }

        public string[] SelectedRateScale
        {
            get
            {
                if (_SelectedRateScale == null)
                {
                    _SelectedRateScale = new string[5];
                    for (int i = 0; i < 5; i++)
                        _SelectedRateScale[i] = "Very Low";
                }

                return _SelectedRateScale;
            }
            set
            {
                _SelectedRateScale = value;
                RaisePropertyChanged("SelectedRateScale");
            }
        }


        public List<string>[] RateCostDriver
        {
            get
            {
                _RateCostDriver = new List<string>[7];

                for (int i = 0; i < 7; i++)
                {
                    if (i == 0 || i ==3 || i ==4 || i ==5)
                        _RateCostDriver[i] = new List<string> { "Extra Low", "Very Low", "Low", "Nominal", "High", "Very High", "Extra High" };
                    else if (i == 1)
                        _RateCostDriver[i] = new List<string> {"Low", "Nominal", "High", "Very High", "Extra High" };
                    else if (i == 2)
                        _RateCostDriver[i] = new List<string> { "Low", "Nominal", "High", "Very High", "Extra High" };
                    else if (i == 6)
                        _RateCostDriver[i] = new List<string> {"Very Low", "Low", "Nominal"};
                }
                    

                return _RateCostDriver;
            }
        }

        public string[] SelectedRateCostDriver
        {
            get
            {
                if (_SelectedRateCostDriver == null)
                {
                    _SelectedRateCostDriver = new string[7];
                    for (int i = 0; i < 7; i++)
                        _SelectedRateCostDriver[i] = "Extra Low";
                }

                return _SelectedRateCostDriver;
            }
            set
            {
                _SelectedRateCostDriver = value;
                RaisePropertyChanged("SelectedRateCostDriver");
            }
        }

        #endregion



        private void CocomoIIStageIIButtonA()
        {
            double NominalPM = 0;
            double F = 0;
            double EM = 1;
            double B = 0;

            setCostRateScale();
            setRateCostDriver();

            for (int i = 0; i < 5; i++)
                F += selectedCostRateScale[i];

            B = 0.91 + (0.01 * F);
            NominalPM = 2.5 * Math.Pow(KLOC, B);

            for (int i = 0; i < 7; i++)
                EM += selectedCostRateCostDriver[i];

            AdjustedPM = NominalPM * EM;
        }


        private void setCostRateScale()
        {
            for (int i = 0; i < 5; i++)
            {
                if (_SelectedRateScale[i] == "Very Low")
                    rating = 0;
                else if (_SelectedRateScale[i] == "Low")
                    rating = 1;
                else if (_SelectedRateScale[i] == "Nominal")
                    rating = 2;
                else if (_SelectedRateScale[i] == "High")
                    rating = 3;
                else if (_SelectedRateScale[i] == "Very High")
                    rating = 4;

                selectedCostRateScale[i] = scaleTable[i, rating];
            }
        }

        private  void setRateCostDriver()
        {
            for (int i = 0; i < 7; i++)
            {
                if (_SelectedRateCostDriver[i] == "Extra Low")
                    rating = 0;
                else if (_SelectedRateCostDriver[i] == "Very Low")
                    rating = 1;
                else if (_SelectedRateCostDriver[i] == "Low")
                    rating = 2;
                else if (_SelectedRateCostDriver[i] == "Nominal")
                    rating = 3;
                else if (_SelectedRateCostDriver[i] == "High")
                    rating = 4;
                else if (_SelectedRateCostDriver[i] == "Very High")
                    rating = 5;
                else if (_SelectedRateCostDriver[i] == "Extra High")
                    rating = 6;

                selectedCostRateCostDriver[i] = driverTable[i, rating];
            }
        }
    }
}
