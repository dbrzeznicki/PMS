using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.Model
{
    public class SubtaskStatus
    {
        [Key]
        public int SubtaskStatusID { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Subtask> Subtasks { get; set; }
    }
}
