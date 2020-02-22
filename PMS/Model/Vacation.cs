using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.Model
{
    public class Vacation
    {
        [Key]
        public int VacationID { get; set; }

        [ForeignKey("User")]
        public int UserID { get; set; }
        [ForeignKey("VacationType")]
        public int VacationTypeID { get; set; }

        public DateTime StartVacation { get; set; }
        public DateTime EndVacation { get; set; }
        public int NumberOfDays { get; set; }

        public virtual User User { get; set; }
        public virtual VacationType VacationType { get; set; }
    }
}
