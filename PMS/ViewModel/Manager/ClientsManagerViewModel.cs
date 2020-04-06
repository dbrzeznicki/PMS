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
    public class ClientsManagerViewModel : BindableBase
    {

        #region variable

        public readonly PMSContext dbContext = new PMSContext();

        private ObservableCollection<Client> _FilteredClients;
        private string _SelectedCompanyName;
        private string _SelectedNIP;


        private string _FirstName = "";
        private string _LastName = "";
        private string _CompanyName = "";
        private string _NIP = "";
        private string _REGON = "";
        private string _Street;
        private string _HouseNumber;
        private string _ApartmentNumber;
        private string _Postcode;
        private string _City;


        private ObservableCollection<Client> _Clients { get; set; }
        private Client _mySelectedClient;

        private bool _IsEnabledEditButton = false;
        private string _EditFirstName = "";
        private string _EditLastName = "";
        private string _EditCompanyName = "";
        private string _EditNIP = "";
        private string _EditREGON = "";
        private string _EditStreet;
        private string _EditHouseNumber;
        private string _EditApartmentNumber;
        private string _EditPostcode;
        private string _EditCity;


        #endregion


        #region command

        public ICommand AddClientButton { get; set; }
        public ICommand EditClientButton { get; set; }

        #endregion


        #region constructors

        public ClientsManagerViewModel()
        {
            AddClientButton = new DelegateCommand(AddClient);
            EditClientButton = new DelegateCommand(EditClient);
        }

        #endregion


        #region properties
        public ObservableCollection<Client> FilteredClients
        {
            get
            {
                if (_FilteredClients == null)
                {
                    _FilteredClients = new ObservableCollection<Client>(dbContext.Client);
                }
                return _FilteredClients;
            }
            set
            {
                _FilteredClients = value;
                RaisePropertyChanged("FilteredClients");
            }
        }

        public string SelectedCompanyName
        {
            get
            {
                return _SelectedCompanyName;
            }
            set
            {
                _SelectedCompanyName = value;
                RaisePropertyChanged("SelectedCompanyName");
                FilterUser();
            }
        }

        public string SelectedNIP
        {
            get
            {
                return _SelectedNIP;
            }
            set
            {
                _SelectedNIP = value;
                RaisePropertyChanged("SelectedNIP");
                FilterUser();
            }
        }



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


        public string EditFirstName
        {
            get
            {
                return _EditFirstName;
            }
            set
            {
                _EditFirstName = value;
                RaisePropertyChanged("EditFirstName");
            }
        }
        public string EditLastName
        {
            get
            {
                return _EditLastName;
            }
            set
            {
                _EditLastName = value;
                RaisePropertyChanged("EditLastName");
            }
        }
        public string EditCompanyName
        {
            get
            {
                return _EditCompanyName;
            }
            set
            {
                _EditCompanyName = value;
                RaisePropertyChanged("EditCompanyName");
            }
        }
        public string EditNIP
        {
            get
            {
                return _EditNIP;
            }
            set
            {
                _EditNIP = value;
                RaisePropertyChanged("EditNIP");
            }
        }
        public string EditREGON
        {
            get
            {
                return _EditREGON;
            }
            set
            {
                _EditREGON = value;
                RaisePropertyChanged("EditREGON");
            }
        }

        public string EditStreet
        {
            get
            {
                return _EditStreet;
            }
            set
            {
                _EditStreet = value;
                RaisePropertyChanged("EditStreet");
            }
        }
        public string EditHouseNumber
        {
            get
            {
                return _EditHouseNumber;
            }
            set
            {
                _EditHouseNumber = value;
                RaisePropertyChanged("EditHouseNumber");
            }
        }
        public string EditApartmentNumber
        {
            get
            {
                return _EditApartmentNumber;
            }
            set
            {
                _EditApartmentNumber = value;
                RaisePropertyChanged("EditApartmentNumber");
            }
        }
        public string EditPostcode
        {
            get
            {
                return _EditPostcode;
            }
            set
            {
                _EditPostcode = value;
                RaisePropertyChanged("EditPostcode");
            }
        }
        public string EditCity
        {
            get
            {
                return _EditCity;
            }
            set
            {
                _EditCity = value;
                RaisePropertyChanged("EditCity");
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

                if(_mySelectedClient != null)
                {
                    EditFirstName = _mySelectedClient.FirstName;
                    EditLastName = _mySelectedClient.LastName;
                    EditCompanyName = _mySelectedClient.CompanyName;
                    EditNIP = _mySelectedClient.NIP;
                    EditREGON = _mySelectedClient.REGON;

                    EditStreet = _mySelectedClient.Street;
                    EditHouseNumber = _mySelectedClient.HouseNumber;
                    EditApartmentNumber = _mySelectedClient.ApartmentNumber;
                    EditPostcode = _mySelectedClient.Postcode;
                    EditCity = _mySelectedClient.City;
                }

            }
        }


        public ObservableCollection<Client> Clients
        {
            get
            {
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


        #region methods

        //private void RemoveUser(object ID)
        //{
        //    int val = Convert.ToInt32(ID);
        //    User user = dbContext.User.Find(val);
        //    dbContext.User.Remove(user);
        //    dbContext.SaveChanges();
        //    _Users.Remove(user);
        //    _FilteredUsers.Remove(user);

        //}

        public void FilterUser()
        {

            if (SelectedNIP == null)
                SelectedNIP = "";
            if (_SelectedCompanyName == null)
                _SelectedCompanyName = "";

            FilteredClients = new ObservableCollection<Client>(dbContext.Client.Where(c => c.NIP.Contains(SelectedNIP)
            && c.CompanyName.Contains(SelectedCompanyName)));
        }



        private void AddClient()
        {
            ManagerValidation VM = new ManagerValidation();
            bool correctForm = VM.ClientValidation(FirstName, LastName, CompanyName, NIP, REGON);

            if (correctForm)
            {
                Client client;

                client = AddClientObject();

                dbContext.Client.Add(client);
                dbContext.SaveChanges();


                _FilteredClients = new ObservableCollection<Client>(dbContext.Client);
                _Clients = new ObservableCollection<Client>(dbContext.Client);

                RaisePropertyChanged("FilteredClients");
                RaisePropertyChanged("Clients");

                clear();

                ErrorMessage er = new ErrorMessage("Client created successfully!");
                er.ShowDialog();
            }

        }

        private Client AddClientObject()
        {
            Client client;

            client = new Client()
            {
                FirstName = _FirstName,
                LastName = _LastName,
                CompanyName = _CompanyName,
                NIP = _NIP,
                REGON = _REGON,
                Street = _Street,
                HouseNumber = _HouseNumber,
                ApartmentNumber = _ApartmentNumber,
                City = _City,
                Postcode = _Postcode
            };

            return client;
        }



        private void EditClient()
        {

            ManagerValidation VM = new ManagerValidation();
            bool correctForm = VM.ClientValidation(EditFirstName, EditLastName, EditCompanyName, EditNIP, EditREGON);

            if (correctForm)
            {
                MySelectedClient.FirstName = EditFirstName;
                MySelectedClient.LastName = EditLastName;

                MySelectedClient.CompanyName = EditCompanyName;

                MySelectedClient.NIP = EditNIP;
                MySelectedClient.REGON = EditREGON;
                MySelectedClient.Street = EditStreet;
                MySelectedClient.HouseNumber = EditHouseNumber;
                MySelectedClient.ApartmentNumber = EditApartmentNumber;
                MySelectedClient.Postcode = EditPostcode;
                MySelectedClient.City = EditCity;

                dbContext.SaveChanges();


                _FilteredClients = new ObservableCollection<Client>(dbContext.Client);
                _Clients = new ObservableCollection<Client>(dbContext.Client);

                RaisePropertyChanged("FilteredClients");
                RaisePropertyChanged("Clients");

                clear();

                ErrorMessage er = new ErrorMessage("Client edit successfully!");
                er.ShowDialog();
            }
        }


        private void clear()
        {
            _FirstName = "";
            _LastName = "";
            _CompanyName = "";
            _NIP = "";
            _REGON = "";
            _Street = "";
            _HouseNumber = "";
            _ApartmentNumber = "";
            _Postcode = "";
            _City = "";

            _EditFirstName = "";
            _EditLastName = "";
            _EditCompanyName = "";
            _EditNIP = "";
            _EditREGON = "";
            _EditStreet = "";
            _EditHouseNumber = "";
            _EditApartmentNumber = "";
            _EditPostcode = "";
            _EditCity = "";

            _mySelectedClient = null;
            _IsEnabledEditButton = false;


            RaisePropertyChanged("FirstName");
            RaisePropertyChanged("LastName");
            RaisePropertyChanged("CompanyName");
            RaisePropertyChanged("NIP");
            RaisePropertyChanged("REGON");
            RaisePropertyChanged("Street");
            RaisePropertyChanged("HouseNumber");
            RaisePropertyChanged("ApartmentNumber");
            RaisePropertyChanged("Postcode");
            RaisePropertyChanged("City");

            RaisePropertyChanged("EditFirstName");
            RaisePropertyChanged("EditLastName");
            RaisePropertyChanged("EditCompanyName");
            RaisePropertyChanged("EditNIP");
            RaisePropertyChanged("EditREGON");
            RaisePropertyChanged("EditStreet");
            RaisePropertyChanged("EditHouseNumber");
            RaisePropertyChanged("EditApartmentNumber");
            RaisePropertyChanged("EditPostcode");
            RaisePropertyChanged("EditCity");

            RaisePropertyChanged("MySelectedClient");
            RaisePropertyChanged("IsEnabledEditButton");
        }


        #endregion
    }
}
