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
    public class CocomoIIStageI : BindableBase
    {
        #region variables

        private int[] _Screen = new int[4];
        private int[] _Report = new int[4];

        private int[,] CPLXTable = new int[3, 3]
        {
            {0,0,1},
            {0,1,2},
            {1,2,2}
        };

        private int[,] WeightTable = new int[3, 3]
        {
            {1,2,3},
            {2,5,8},
            {-1,-1,10}
        };

        double _Effort = 0;
        int _Reuse = 0;

        private List<string> _DevelopersExperienceAndCapability;
        private string _SelectedDevelopersExperienceAndCapability = "Very Low";

        int column = 0;
        int reportRow = 0;
        int screenRow = 0;
        int reportCPLX = 0;
        int screenCPLX = 0;
        int rating = 0;
        int OP = 0;
        double NOP;
        int[] PROD = new int[5] { 4, 7, 13, 25, 50 };

        #endregion



        #region command

        public ICommand CocomoIIStageIButton { get; set; }

        #endregion


        #region constructor

        public CocomoIIStageI()
        {
            CocomoIIStageIButton = new DelegateCommand(CocomoIIStageIButtonA);
        }

        #endregion


        #region properties

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

        public int Reuse
        {
            get
            {
                return _Reuse;
            }
            set
            {
                _Reuse = value;
                RaisePropertyChanged("Reuse");
            }
        }

        public List<string> DevelopersExperienceAndCapability
        {
            get
            {
                _DevelopersExperienceAndCapability = new List<string> { "Very Low", "Low", "Nominal", "High", "Very High" };
                SelectedDevelopersExperienceAndCapability = "Very Low";

                return _DevelopersExperienceAndCapability;
            }
            set
            {
                _DevelopersExperienceAndCapability = value;
                RaisePropertyChanged("DevelopersExperienceAndCapability");
            }
        }

        public string SelectedDevelopersExperienceAndCapability
        {
            get
            {
                return _SelectedDevelopersExperienceAndCapability;
            }
            set
            {
                _SelectedDevelopersExperienceAndCapability = value;
                RaisePropertyChanged("SelectedDevelopersExperienceAndCapability");
            }
        }


        public int[] Report
        {
            get
            {
                return _Report;
            }
            set
            {
                _Report = value;
                RaisePropertyChanged("Report");
            }
        }

        public int[] Screen
        {
            get
            {
                return _Screen;
            }
            set
            {
                _Screen = value;
                RaisePropertyChanged("Screen");
            }
        }
        #endregion


        private void CocomoIIStageIButtonA()
        {
            setScreenRow();
            setReportRow();
            setColumn();
            setRating();

            screenCPLX = CPLXTable[screenRow, column];
            reportCPLX = CPLXTable[reportRow, column];

            OP = (_Screen[0] * WeightTable[0,screenCPLX]) + (_Report[0] * WeightTable[1,reportCPLX]);

            NOP = (OP * (100 - _Reuse)) / 100.0;

            Effort = NOP/PROD[rating];
            //set variable

        }

        private void setScreenRow()
        {
            if (_Screen[1] < 3)
                screenRow = 0;
            else if (_Screen[1] >= 3 && _Screen[1] <= 7)
                screenRow = 1;
            else if (_Screen[1] >= 8)
                screenRow = 2;
        }

        private void setReportRow()
        {
            if (_Report[1] == 0 || _Report[1] == 1)
                reportRow = 0;
            else if (_Report[1] == 2 || _Report[1] == 3)
                reportRow = 1;
            else if (_Report[1] >= 4)
                reportRow = 2;
        }

        private void setColumn()
        {
            if (_Screen[2] < 2 && _Screen[3] < 3)
                column = 0;
            else if ((_Screen[2] >= 2 && _Screen[2] <= 3) && (_Screen[3] >= 3 && _Screen[3] <= 5))
                column = 1;
            else if (_Screen[2] > 3 && _Screen[3] > 5)
                column = 2;
        }

        private void setRating()
        {
            if (SelectedDevelopersExperienceAndCapability == "Very Low")
                rating = 0;
            else if (SelectedDevelopersExperienceAndCapability == "Low")
                rating = 1;
            else if (SelectedDevelopersExperienceAndCapability == "Nominal")
                rating = 2;
            else if (SelectedDevelopersExperienceAndCapability == "High")
                rating = 3;
            else if (SelectedDevelopersExperienceAndCapability == "Very High")
                rating = 4;
        }

        /*private void setVariable()
        {
            Screen = new int[] { 0, 0, 0, 0, 0 };
            Report = new int[] { 0, 0, 0, 0, 0 };

        }*/
    }
}
