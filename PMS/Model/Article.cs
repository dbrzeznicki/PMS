using PMS.DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.Model
{
    public class Article
    {
        [Key]
        public int ArticleID { get; set; }

        [ForeignKey("User")]
        public int UserID { get; set; }


        public string Description { get; set; }
        public string Url { get; set; }
        public DateTime DateAdded { get; set; }

        public virtual User User { get; set; }

    }
}
