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


        public DbSet<UserRole> UserRole { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<ProjectStatus> ProjectStatus { get; set; }
        public DbSet<VacationType> VacationType { get; set; }
        public DbSet<Client> Client { get; set; }
        public DbSet<Team> Team { get; set; }
        public DbSet<Project> Project { get; set; }
        public DbSet<MainTask> MainTask { get; set; }
        public DbSet<SubtaskStatus> SubtaskStatus { get; set; }
        public DbSet<Subtask> Subtask { get; set; }
        public DbSet<Vacation> Vacation { get; set; }
        public DbSet<Article> Article { get; set; }
        public DbSet<RecentActivity> RecentActivity { get; set; }
        public DbSet<PayoutBonus> PayoutBonus { get; set; }
        public DbSet<Resources> Resources { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MainTask>()
                .HasMany(p => p.PrecedingMainTasks)
                .WithMany()
                .Map(m =>
                {
                    m.MapLeftKey("MainTaskID");
                    m.MapRightKey("RelatedID");
                    m.ToTable("maintask_related");
                });
        }
    }
}