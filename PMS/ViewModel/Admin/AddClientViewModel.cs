using PMS.DAL;
using PMS.Model;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PMS
{
    public class AddClientViewModel : BindableBase
    {

        #region variable

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

        //Projects?????????;
        #endregion

        #region command

        public ICommand AddClientButton { get; set; }

        #endregion


        #region constructor

        public AddClientViewModel()
        {
            AddClientButton = new DelegateCommand(AddClient);
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
        #endregion


        private void AddClient()
        {
            PMSContext dbContext = new PMSContext();
            Client client;

            client = AddClientObject();

            dbContext.Client.Add(client);
            dbContext.SaveChanges();

            ErrorMessage er = new ErrorMessage("Client created successfully!");
            er.ShowDialog();
        }

        private Client AddClientObject()
        {
            PMSContext dbContext = new PMSContext();
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
    }
}
