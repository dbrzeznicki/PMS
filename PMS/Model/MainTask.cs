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
        public DateTime EarlyStart { get; set; }
        public DateTime EarlyFinish { get; set; }
        public DateTime LateStart { get; set; }
        public DateTime LateFinish { get; set; }
        public int Effort { get; set; }
        public bool Status { get; set; }

        public virtual ICollection<MainTask> PrecedingMainTasks { get; set; }

        public virtual ICollection<Subtask> Subtasks { get; set; }
        public virtual Project Project { get; set; }



        public string StringMainTask
        {
            get
            {
                if (PrecedingMainTasks != null)
                {
                    List<string> tmp = new List<string>(PrecedingMainTasks.Select(x => x.Name));
                    return string.Join(",", tmp);
                } 
                else
                {
                    return "";
                }
                
            }
        }

        public string StringEarlyStart
        {
            get
            {
                    return EarlyStart.ToString("dd/MM/yyyy");
            }
        }

        public string StringEarlyFinish
        {
            get
            {
                return EarlyFinish.ToString("dd/MM/yyyy");
            }
        }

        public string StringLateStart
        {
            get
            {
                return LateStart.ToString("dd/MM/yyyy");
            }
        }

        public string StringLateFinish
        {
            get
            {
                return LateFinish.ToString("dd/MM/yyyy");
            }
        }

    }
}
