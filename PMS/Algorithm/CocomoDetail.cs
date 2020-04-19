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
    public class CocomoDetail : BindableBase
    {

        #region variables

        private double _Effort = 0;
        private double _Time = 0;
        private int _KLOC;
        private List<string>[] _Drivers = new List<string>[15];
        private string[] _SelectedDrivers = null;
        private List<string> _ProjectTypes;
        private string _SelectedProjectType = "Organic";

        private List<string> _SizesProjectDetail;
        private string _SelectedSizesProjectDetail = "Small";

        private int model = 0;
        private int rating;
        private int size = 0;

        private double[,] table = new double[3, 4]
        {
            {3.2,1.05,2.5,0.38},
            {3.0,1.12,2.5,0.35},
            {2.8,1.20,2.5,0.32}
        };

        private double[,] costDrivers = new double[15, 6]
        {
            {0.75,0.88,1,1.15,1.40,-1},
            {-1,0.94,1,1.08,1.16,-1},
            {0.70,0.85,1,15,1.30,1.65},
            {-1,-1,1,1.11,1.30,1.66},
            {-1,-1,1,1.06,1.21,1.56},
            {-1,0.87,1,1.15,1.30,-1},
            {-1,0.87,1,1.07,1.15,-1},
            {1.46,1.19,1,0.86,0.71,-1},
            {1.29,1.13,1.00,0.91,0.82,-1},
            {1.42,1.17,1,0.86,0.70,-1},
            {1.21,1.10,1,0.90,-1,-1},
            {1.14,1.07,1,0.95,-1,-1},
            {1.24,1.10,1.00,0.91,0.82,-1},
            {1.24,1.10,1,0.91,0.83,-1},
            {1.23,1.08,1,1.04,1.10,-1}
        };

        private double[] selectedCost = new double[15];

        private double[,] costDetail1 = new double[6, 5]
        {
            {0.06,0.16,0.26,0.42,0.16},
            {0.06,0.16,0.24,0.38,0.22},
            {0.07,0.17,0.25,0.33,0.25},
            {0.07,0.17,0.24,0.31,0.28},
            {0.08,0.18,0.25,0.26,0.31},
            {0.08,0.18,0.24,0.24,0.34}
          };

        private double[,] costDetail2 = new double[6, 5]
        {
            {0.10,0.19,0.24,0.39,0.18},
            {0.12,0.19,0.21,0.34,0.26},
            {0.20,0.26,0.21,0.27,0.26},
            {0.22,0.27,0.19,0.25,0.29},
            {0.36,0.36,0.18,0.18,0.28},
            {0.40,0.38,0.16,0.16,0.30}
        };

        private string[] name = new string[5]
        {
            "Planning and Requirements",
            "System Design",
            "Detail Design",
            "Module Code and Test",
            "Integration and Test"
        };

        private string _PhaseWiseDistributionOfEffort;
        private string _PhaseWiseDistributionOfEffortDevelopmentTime;

        #endregion


    #region command

    public ICommand CocomoIntermediateButton { get; set; }

        #endregion


        #region constructor

        public CocomoDetail()
        {
            CocomoIntermediateButton = new DelegateCommand(CocomoDetailA);
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

        public List<string>[] Drivers
        {
            get
            {
                _Drivers = new List<string>[15];

                for (int i = 0; i < 15; i++)
                {
                    if (i == 0 || i == 7 || i == 8 || i == 9 || i == 12 || i == 13 || i == 14)
                        _Drivers[i] = new List<string> { "Very Low", "Low", "Nominal", "High", "Very High" };
                    else if (i == 1 || i == 5 || i == 6)
                        _Drivers[i] = new List<string> { "Low", "Nominal", "High", "Very High" };
                    else if (i == 2)
                        _Drivers[i] = new List<string> { "Very Low", "Low", "Nominal", "High", "Very High", "Extra High" };
                    else if (i == 3 || i == 4)
                        _Drivers[i] = new List<string> { "Nominal", "High", "Very High", "Extra High" };
                    else
                        _Drivers[i] = new List<string> { "Very Low", "Low", "Nominal", "High" };
                }

                return _Drivers;
            }
        }

        public string[] SelectedDrivers
        {
            get
            {
                if (_SelectedDrivers == null)
                {
                    _SelectedDrivers = new string[15];
                    for (int i = 0; i < 15; i++)
                        _SelectedDrivers[i] = "Very Low";
                }

                return _SelectedDrivers;
            }
            set
            {
                _SelectedDrivers = value;
                RaisePropertyChanged("SelectedDrivers");
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

                //////////
                RaisePropertyChanged("SizesProjectDetail");
                return _SelectedProjectType;
            }
            set
            {
                _SelectedProjectType = value;
                RaisePropertyChanged("SelectedProjectType");
            }
        }

        public List<string> SizesProjectDetail
        {
            get
            {
                if(model==0)
                {
                    _SizesProjectDetail = new List<string> { "Small", "Medium" };
                    SelectedSizesProjectDetail = "Small";
                }   
                else if (model == 1)
                {
                    _SizesProjectDetail = new List<string> { "Medium", "Large" };
                    SelectedSizesProjectDetail = "Medium";
                }   
                else if (model == 2)
                {
                    _SizesProjectDetail = new List<string> { "Large", "Extra Large" };
                    SelectedSizesProjectDetail = "Large";
                }

                return _SizesProjectDetail;
            }
            set
            {
                _SizesProjectDetail = value;
                RaisePropertyChanged("SizesProjectDetail");
            }
        }

        public string SelectedSizesProjectDetail
        {
            get
            {
                setSize();

                return _SelectedSizesProjectDetail;
            }
            set
            {

                _SelectedSizesProjectDetail = value;
                RaisePropertyChanged("SelectedSizesProjectDetail");
            }
        }

        public string PhaseWiseDistributionOfEffort
        {
            get
            {
                return _PhaseWiseDistributionOfEffort;
            }
            set
            {

                _PhaseWiseDistributionOfEffort = value;
                RaisePropertyChanged("PhaseWiseDistributionOfEffort");
            }
        }

        public string PhaseWiseDistributionOfEffortDevelopmentTime
        {
            get
            {
                return _PhaseWiseDistributionOfEffortDevelopmentTime;
            }
            set
            {

                _PhaseWiseDistributionOfEffortDevelopmentTime = value;
                RaisePropertyChanged("PhaseWiseDistributionOfEffortDevelopmentTime");
            }
        }
        #endregion


        private void CocomoDetailA()
        {
            AlgorithmValidation AV = new AlgorithmValidation();
            bool correctForm = AV.CocomoBasicValidation(KLOC);
            if (correctForm)
            {
                double EAF = 1;
                setCostDrivers();

                for (int i = 0; i < 15; i++)
                    EAF += selectedCost[i];


                Effort = table[model, 0] * Math.Pow(_KLOC, table[model, 1]) * EAF;
                Time = table[model, 2] * Math.Pow(_Effort, table[model, 3]);

                PhaseWiseDistributionOfEffort = "";

                for (int i=0; i<5; i++)
                {
                    PhaseWiseDistributionOfEffort += name[i] + " phase = ";
                    double tmp = Math.Round(Effort * costDetail1[size, i], 2);
                    PhaseWiseDistributionOfEffort += tmp + "\n";
                }

                PhaseWiseDistributionOfEffortDevelopmentTime = "";

                for (int i = 0; i < 5; i++)
                {
                    PhaseWiseDistributionOfEffortDevelopmentTime += name[i] + " phase = ";
                    double tmp = Math.Round(Time * costDetail2[size, i], 2);
                    PhaseWiseDistributionOfEffortDevelopmentTime += tmp + "\n";
                }
            }
        }





        public void setCostDrivers()
        {
            for (int i = 0; i < 15; i++)
            {
                if (_SelectedDrivers[i] == "Very Low")
                    rating = 0;
                else if (_SelectedDrivers[i] == "Low")
                    rating = 1;
                else if (_SelectedDrivers[i] == "Nominal")
                    rating = 2;
                else if (_SelectedDrivers[i] == "High")
                    rating = 3;
                else if (_SelectedDrivers[i] == "Very High")
                    rating = 4;
                else if (_SelectedDrivers[i] == "Extra High")
                    rating = 5;

                selectedCost[i] = costDrivers[i, rating];
            }
        }

        public void setSize()
        {
            if (model == 0)
            {
                if (_SelectedSizesProjectDetail == "Small")
                    size = 0;
                if (_SelectedSizesProjectDetail == "Medium")
                    size = 1;
            }
            else if (model == 1)
            {
                if (_SelectedSizesProjectDetail == "Medium")
                    size = 2;
                if (_SelectedSizesProjectDetail == "Large")
                    size = 3;
            }
            else if (model == 2)
            {
                if (_SelectedSizesProjectDetail == "Large")
                    size = 4;
                if (_SelectedSizesProjectDetail == "Extra Large")
                    size = 5;
            }
        }
    }
}
