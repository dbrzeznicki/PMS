using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.Model
{
    public class User
    {
        [Key]
        public int UserID { get; set; }


        [ForeignKey("UserRole")]
        public int UserRoleID { get; set; }
        [ForeignKey("Team")]
        public int ?TeamID { get; set; }


        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public double Salary { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public DateTime AccountCreationDate { get; set; }
        public DateTime HiredDate { get; set; } // data zatrudnienia
        //[DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        //[Column(TypeName = "datetime2")]
        public DateTime FiredDate { get; set; } //data zwolnienia

        public string ResidenceStreet { get; set; }
        public string ResidenceHouseNumber { get; set; }
        public string ResidenceApartmentNumber { get; set; }
        public string ResidencePostcode { get; set; }
        public string ResidenceCity { get; set; }

        //dane korespondencyjne
        public string CorrespondenceStreet { get; set; }
        public string CorrespondenceHouseNumber { get; set; }
        public string CorrespondenceApartmentNumber { get; set; }
        public string CorrespondencePostcode { get; set; }
        public string CorrespondenceCity { get; set; }


        public virtual UserRole UserRole { get; set; }
        public virtual Team Team { get; set; }
        public virtual ICollection<Vacation> Vacations { get; set; }
        public virtual ICollection<Subtask> Subtasks { get; set; }
        public virtual ICollection<Subtask> Articles { get; set; }

        public string FullResidenceAdress
        {
            get
            {
                if (ResidenceApartmentNumber == null || ResidenceApartmentNumber.Equals(""))
                    return $"{ResidencePostcode} {ResidenceCity} {ResidenceStreet} {ResidenceHouseNumber}";
                else
                    return $"{ResidencePostcode} {ResidenceCity} {ResidenceStreet} {ResidenceHouseNumber}/{ResidenceApartmentNumber}";
            }
        }

        public string FullCorrespondenceAdress
        {
            get
            {
                if (CorrespondenceApartmentNumber == null || CorrespondenceApartmentNumber.Equals(""))
                    return $"{CorrespondencePostcode} {CorrespondenceCity} {CorrespondenceStreet} {CorrespondenceHouseNumber}";
                else
                    return $"{CorrespondencePostcode} {CorrespondenceCity} {CorrespondenceStreet} {CorrespondenceHouseNumber}/{CorrespondenceApartmentNumber}";
            }
        }

        public string SearchEditUser
        {
            get
            {
                    return $"{FirstName} {LastName} {PhoneNumber}";
            }
        }

        public string FullName
        {
            get
            {
                return $"{FirstName} {LastName}";
            }
        }

    }
}
