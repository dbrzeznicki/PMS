using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.Model
{
    public class Team
    {
        [Key]
        public int TeamID { get; set; }

        public string Name { get; set; }
        public int NumberOfPeople { get; set; }
        public int NumberOfProjects { get; set; }

        public virtual ICollection<User> Users { get; set; }

        public virtual ICollection<RecentActivity> RecentActivities { get; set; }


        public virtual ICollection<Project> Projects { get; set; }
    }
}
