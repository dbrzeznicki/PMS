using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.Model
{
    public class Article
    {
        [Key]
        public int ArticleID { get; set; }

        public string Description { get; set; }
        public string Url { get; set; }
        public DateTime DateAdded { get; set; }
        public User WhoAdded { get; set; }

    }
}
