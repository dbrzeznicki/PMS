namespace PMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class recentActivity : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.RecentActivities", "WhoAdded_UserID", "dbo.Users");
            DropIndex("dbo.RecentActivities", new[] { "WhoAdded_UserID" });
            AddColumn("dbo.RecentActivities", "TeamID", c => c.Int(nullable: false));
            CreateIndex("dbo.RecentActivities", "TeamID");
            AddForeignKey("dbo.RecentActivities", "TeamID", "dbo.Teams", "TeamID", cascadeDelete: true);
            DropColumn("dbo.RecentActivities", "ProjectName");
            DropColumn("dbo.RecentActivities", "SubtaskName");
            DropColumn("dbo.RecentActivities", "WhoAdded_UserID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.RecentActivities", "WhoAdded_UserID", c => c.Int());
            AddColumn("dbo.RecentActivities", "SubtaskName", c => c.String());
            AddColumn("dbo.RecentActivities", "ProjectName", c => c.String());
            DropForeignKey("dbo.RecentActivities", "TeamID", "dbo.Teams");
            DropIndex("dbo.RecentActivities", new[] { "TeamID" });
            DropColumn("dbo.RecentActivities", "TeamID");
            CreateIndex("dbo.RecentActivities", "WhoAdded_UserID");
            AddForeignKey("dbo.RecentActivities", "WhoAdded_UserID", "dbo.Users", "UserID");
        }
    }
}
