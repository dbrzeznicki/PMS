using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.Model
{
    public class Project
    {
        [Key]
        public int ProjectID { get; set; }
        
        [ForeignKey("Client")]
        public int ClientID { get; set; }
        [ForeignKey("ProjectStatus")]
        public int ProjectStatusID { get; set; }
        [ForeignKey("Team")]
        public int TeamID { get; set; }


        public string Name { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public double Cost { get; set; }


        public virtual ICollection<MainTask> MainTasks { get; set; }
        public virtual Client Client { get; set; }
        public virtual ProjectStatus ProjectStatus { get; set; }
        public virtual Team Team { get; set; }
        public virtual ICollection<Resources> Resources { get; set; }
    }
}
