using PMS.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.DAL
{
    public class PMSContext : DbContext
    {
        public PMSContext() : base("name = PMSContext")
        {

        }

        static PMSContext()
        {
            Database.SetInitializer<PMSContext>(new PMSInitializer());
        }

        //tutaj dodajmy wszystkie tabele jakie mamy w bazie/modelu
        public DbSet<User> User { get; set; }
        public DbSet<UserRole> UserRole { get; set; }
    }
}
