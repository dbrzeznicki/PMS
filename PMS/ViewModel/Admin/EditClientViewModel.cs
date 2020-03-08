using PMS.DAL;
using PMS.Model;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PMS
{
    public class EditClientViewModel : BindableBase
    {

        public readonly PMSContext dbContext = new PMSContext();
        
        
        #region variables

        private ObservableCollection<Client> _Clients { get; set; }
        private Client _mySelectedClient;

        private bool _IsEnabledEditButton = false;
        private string _FirstName;
        private string _LastName;
        private string _CompanyName;
        private string _NIP;
        private string _REGON;
        private string _Street;
        private string _HouseNumber;
        private string _ApartmentNumber;
        private string _Postcode;
        private string _City;

        #endregion

        #region command

        public ICommand EditClientButton { get; set; }

        #endregion

        #region constructor

        public EditClientViewModel()
        {
            EditClientButton = new DelegateCommand(EditClient);
        }

        #endregion


        #region properties

        public string FirstName
        {
            get
            {
                return _FirstName;
            }
            set
            {
                _FirstName = value;
                RaisePropertyChanged("FirstName");
            }
        }
        public string LastName
        {
            get
            {
                return _LastName;
            }
            set
            {
                _LastName = value;
                RaisePropertyChanged("LastName");
            }
        }
        public string CompanyName
        {
            get
            {
                return _CompanyName;
            }
            set
            {
                _CompanyName = value;
                RaisePropertyChanged("CompanyName");
            }
        }
        public string NIP
        {
            get
            {
                return _NIP;
            }
            set
            {
                _NIP = value;
                RaisePropertyChanged("NIP");
            }
        }
        public string REGON
        {
            get
            {
                return _REGON;
            }
            set
            {
                _REGON = value;
                RaisePropertyChanged("REGON");
            }
        }

        public string Street
        {
            get
            {
                return _Street;
            }
            set
            {
                _Street = value;
                RaisePropertyChanged("Street");
            }
        }
        public string HouseNumber
        {
            get
            {
                return _HouseNumber;
            }
            set
            {
                _HouseNumber = value;
                RaisePropertyChanged("HouseNumber");
            }
        }
        public string ApartmentNumber
        {
            get
            {
                return _ApartmentNumber;
            }
            set
            {
                _ApartmentNumber = value;
                RaisePropertyChanged("ApartmentNumber");
            }
        }
        public string Postcode
        {
            get
            {
                return _Postcode;
            }
            set
            {
                _Postcode = value;
                RaisePropertyChanged("Postcode");
            }
        }
        public string City
        {
            get
            {
                return _City;
            }
            set
            {
                _City = value;
                RaisePropertyChanged("City");
            }
        }
        public bool IsEnabledEditButton
        {
            get
            {
                return _IsEnabledEditButton;
            }
            set
            {
                _IsEnabledEditButton = value;
                RaisePropertyChanged("IsEnabledEditButton");
            }
        }

        public Client MySelectedClient
        {
            get { return _mySelectedClient; }
            set
            {
                _mySelectedClient = value;
                RaisePropertyChanged("MySelectedClient");

                IsEnabledEditButton = true;

                FirstName = _mySelectedClient.FirstName;
                LastName = _mySelectedClient.LastName;
                CompanyName = _mySelectedClient.CompanyName;
                NIP = _mySelectedClient.NIP;
                REGON = _mySelectedClient.REGON;

                Street = _mySelectedClient.Street;
                HouseNumber = _mySelectedClient.HouseNumber;
                ApartmentNumber = _mySelectedClient.ApartmentNumber;
                Postcode = _mySelectedClient.Postcode;
                City = _mySelectedClient.City;
            }
        }


        public ObservableCollection<Client> Clients
        {
            get
            {
                //PMSContext dbContext = new PMSContext();
                if (_Clients == null)
                {
                    _Clients = new ObservableCollection<Client>(dbContext.Client);
                }
                return _Clients;
            }
            set
            {
                _Clients = value;
                RaisePropertyChanged("Clients");
            }
        }
        #endregion


        private void EditClient()
        {

            MySelectedClient.FirstName = FirstName;
            MySelectedClient.LastName = LastName;

            MySelectedClient.CompanyName = CompanyName;

            MySelectedClient.NIP = NIP;
            MySelectedClient.REGON = REGON;
            MySelectedClient.Street = Street;
            MySelectedClient.HouseNumber = HouseNumber;
            MySelectedClient.ApartmentNumber = ApartmentNumber;
            MySelectedClient.Postcode = Postcode;
            MySelectedClient.City = City;

            dbContext.SaveChanges();

            Clients = new ObservableCollection<Client>(dbContext.Client);

            ErrorMessage er = new ErrorMessage("Client edit successfully!");
            er.ShowDialog();
        }
    }
}
