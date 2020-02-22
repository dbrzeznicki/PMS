using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.Model
{
    public class RecentActivity
    {
        [Key]
        public int RecentActivityID { get; set; }
        
        public string Description { get; set; }
        public DateTime DateAdded { get; set; }
        public string ProjectName { get; set; }
        public string SubtaskName { get; set; }
        public User WhoAdded { get; set; }
    }
}
