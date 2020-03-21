﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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

        [ForeignKey("Team")]
        public int TeamID { get; set; }

        public virtual Team Team { get; set; }
    }
}
