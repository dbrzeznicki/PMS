using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.Model
{
    public class ProjectStatus
    {
        [Key]
        public int ProjectStatusID { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Project> Projects { get; set; }
    }
}
