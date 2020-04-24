using System;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace PMS.Model
{
    public class Resources
    {
        [Key]
        public int ResourcesID { get; set; }

        [ForeignKey("Project")]
        public int ProjectID { get; set; }

        public string Name { get; set; }
        public double Cost { get; set; }

        public virtual Project Project { get; set; }
    }
}
