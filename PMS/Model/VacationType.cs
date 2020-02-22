using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.Model
{
    public class VacationType
    {
        [Key]
        public int VacationTypeID { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Vacation> Vacations { get; set; }
    }
}
