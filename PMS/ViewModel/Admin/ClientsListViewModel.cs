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
    public class ClientsListViewModel : BindableBase
    {

        //Filtered
        private ObservableCollection<Client> _FilteredClients { get; set; }
        private string _SelectedCompanyName { get; set; }
        private string _SelectedNIP { get; set; }

        public ClientsListViewModel()
        {
        }

        public ObservableCollection<Client> FilteredClients
        {
            get
            {
                PMSContext dbContext = new PMSContext();
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

            PMSContext dbContext = new PMSContext();

            if (SelectedNIP == null)
                SelectedNIP = "";
            if (_SelectedCompanyName == null)
                _SelectedCompanyName = "";

            FilteredClients = new ObservableCollection<Client>(dbContext.Client.Where(c => c.NIP.Contains(SelectedNIP)
            && c.CompanyName.Contains(SelectedCompanyName)));
        }
    }
}
