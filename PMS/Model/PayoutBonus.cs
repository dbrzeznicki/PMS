using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.Model
{
    public class PayoutBonus
    {
        [Key]
        public int PayoutBonusID { get; set; }

        [ForeignKey("User")]
        public int UserID { get; set; }

        public DateTime DateOfGrantiedBonuses { get; set; }
        public double Value { get; set; }

        public virtual User User { get; set; }
    }
}
