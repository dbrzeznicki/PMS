using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.Model
{
    public class MainTask
    {
        [Key]
        public int MainTaskID { get; set; }
        [ForeignKey("Project")]
        public int ProjectID { get; set; }
        
        public string Name { get; set; }
        public int Priority { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public bool Status { get; set; }
        public List<MainTask> PrecedingMainTasks { get; set; }

        public virtual ICollection<Subtask> Subtasks { get; set; }
        public virtual Project Project { get; set; }
    }
}
