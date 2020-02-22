using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.Model
{
    public class Client
    {
        [Key]
        public int ClientID { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string CompanyName { get; set; }
        public string NIP { get; set; }
        public string REGON { get; set; }
        public string Street { get; set; }
        public string HouseNumber { get; set; }
        public string ApartmentNumber { get; set; }
        public string Postcode { get; set; }
        public string City { get; set; }

        public virtual ICollection<Project> Projects { get; set; }
    }
}
