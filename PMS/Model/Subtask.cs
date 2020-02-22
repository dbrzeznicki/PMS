using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.Model
{
    public class Subtask
    {
        [Key]
        public int SubtaskID { get; set; }

        [ForeignKey("SubtaskStatus")]
        public int SubtaskStatusID { get; set; }
        [ForeignKey("MainTask")]
        public int MainTaskID { get; set; }
        [ForeignKey("User")]
        public int UserID { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }

        //tu dodac potem czasy ile dany użytkownik spędził przy wykonywaniu danego zadania???

        public virtual User User { get; set; }
        public virtual SubtaskStatus SubtaskStatus { get; set; }
        public virtual MainTask MainTask { get; set; }
    }
}
