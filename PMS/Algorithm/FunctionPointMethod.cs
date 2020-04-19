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
    public class FunctionPointMethod : BindableBase
    {

        #region variables

        private List<string> _LevelOfElementComplexity;
        private string _SelectedLevelOfElementComplexity = "Simple";
        private int[] _NumberOfElements;
        
        private int[,] levelOfElementComplexityTable = new int[5, 3]
        {
            {3,4,6},
            {4,5,7},
            {3,4,6},
            {7,10,15},
            {5,7,10},
            
        };

        int level = 0;
        private int totalWeight = 0;


        private List<int>[] _CorrectTable = new List<int>[15];
        private int[] _SelectedCorrectTable = null;

        double VAF = 0;
        int FP = 0;

        private List<string> _LanguageName;
        private string _SelectedLanguageName;

        private int[] languageWeight = new int[38]
        {
            28,51,119,14,97,50,54,61,47,20,32,71,209,
            43,36,34,46,53,47,62,29,23,40,57,37,35,24,
            64,37,26,77,70,38,59,75,21,52,42
        };

        private int _LOC = 0;
        #endregion


        #region command

        public ICommand FunctionPointButton { get; set; }

        #endregion


        #region constructor

        public FunctionPointMethod()
        {
            FunctionPointButton = new DelegateCommand(FunctionPoint);
        }

        #endregion

        #region properties

        public List<string> LevelOfElementComplexity
        {
            get
            {
                _LevelOfElementComplexity = new List<string> { "Simple", "Averagrage", "Complex" };

                return _LevelOfElementComplexity;
            }
        }

        public string SelectedLevelOfElementComplexity
        {
            get
            {
                if (_SelectedLevelOfElementComplexity == "Simple")
                    level = 0;
                else if (_SelectedLevelOfElementComplexity == "Averagrage")
                    level = 1;
                else if (_SelectedLevelOfElementComplexity == "Complex")
                    level = 2;

                return _SelectedLevelOfElementComplexity;
            }
            set
            {
                _SelectedLevelOfElementComplexity = value;
                RaisePropertyChanged("SelectedLevelOfElementComplexity");
            }
        }


        public int[] NumberOfElements
        {
            get
            {
                if (_NumberOfElements == null)
                {
                    _NumberOfElements = new int[5];
                    for (int i = 0; i < 5; i++)
                        _NumberOfElements[i] = 0;
                }

                return _NumberOfElements;
            }
            set
            {
                _NumberOfElements = value;
                RaisePropertyChanged("NumberOfElements");
            }
        }


        public List<int>[] CorrectTable
        {
            get
            {
                _CorrectTable = new List<int>[14];

                for (int i = 0; i < 14; i++)
                    _CorrectTable[i] = new List<int> {0,1,2,3,4,5};

                return _CorrectTable;
            }
        }

        public int[] SelectedCorrectTable
        {
            get
            {
                if (_SelectedCorrectTable == null)
                {
                    _SelectedCorrectTable = new int[14];
                    for (int i = 0; i < 14; i++)
                        _SelectedCorrectTable[i] = 0;
                }

                return _SelectedCorrectTable;
            }
            set
            {
                _SelectedCorrectTable = value;
                RaisePropertyChanged("SelectedCorrectTable");
            }
        }


        public List<string> LanguageName
        {
            get
            {
                _LanguageName = new List<string> {"ABAP (SAP)", "ASP", "Assembler", "Brio",
                                                    "C", "C++", "C#", "COBOL", "Cognos Impromptu Scripts",
                                                    "Cross System Products (CSP)", "Cool:Gen/IEF", "Datastage", "Excel",
                                                    "Focus", "FoxPro", "HTML", "J2EE", "Java", "JavaScript", "JCL",
                                                    "LINC II", "Lotus Notes", "Natural", ".NET", "Oracle", "PACBASE",
                                                    "Perl", "PL/I", "PL/SQL", "Powerbuilder", "REXX", "Sabretalk",
                                                    "SAS", "Siebel", "SLOGAN", "SQL", "VB.NET", "Visual Basic"
                };

                return _LanguageName;
            }
        }

        public string SelectedLanguageName
        {
            get
            {
                return _SelectedLanguageName;
            }
            set
            {
                _SelectedLanguageName = value;
                RaisePropertyChanged("SelectedLanguageName");
            }
        }


        public int LOC
        {
            get
            {
                return _LOC;
            }
            set
            {
                _LOC = value;
                RaisePropertyChanged("LOC");
            }
        }

        #endregion


        private void FunctionPoint()
        {
            //AlgorithmValidation AV = new AlgorithmValidation();
            //bool correctForm = AV.FunctionPointValidation(NumberOfElements);

            //if (correctForm)
            //{
                totalWeight = 0;

                for (int i = 0; i < 5; i++)
                {
                    totalWeight += NumberOfElements[i] * levelOfElementComplexityTable[i, level];
                }

                int tmp = 0;
                for (int i = 0; i < 14; i++)
                {
                    tmp += SelectedCorrectTable[i];
                }

                VAF = tmp * 0.01;

                FP = Convert.ToInt32(totalWeight * (0.65 + VAF));

                LOC = FP * languageWeight[findIndex()];
            //}
        }

        private int findIndex ()
        {
            int index = 0;
            for(int i=0; i<38;i++)
            {
                if (LanguageName[i] == SelectedLanguageName)
                    index = i;
            }

            return index;
        }
    }
}
