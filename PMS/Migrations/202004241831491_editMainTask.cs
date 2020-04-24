namespace PMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class editMainTask : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.MainTasks", "EarlyStart", c => c.DateTime(nullable: false));
            AddColumn("dbo.MainTasks", "EarlyFinish", c => c.DateTime(nullable: false));
            AddColumn("dbo.MainTasks", "LateStart", c => c.DateTime(nullable: false));
            AddColumn("dbo.MainTasks", "LateFinish", c => c.DateTime(nullable: false));
            AddColumn("dbo.MainTasks", "Effort", c => c.Int(nullable: false));
            DropColumn("dbo.MainTasks", "Priority");
            DropColumn("dbo.MainTasks", "StartTime");
            DropColumn("dbo.MainTasks", "EndTime");
        }
        
        public override void Down()
        {
            AddColumn("dbo.MainTasks", "EndTime", c => c.DateTime(nullable: false));
            AddColumn("dbo.MainTasks", "StartTime", c => c.DateTime(nullable: false));
            AddColumn("dbo.MainTasks", "Priority", c => c.Int(nullable: false));
            DropColumn("dbo.MainTasks", "Effort");
            DropColumn("dbo.MainTasks", "LateFinish");
            DropColumn("dbo.MainTasks", "LateStart");
            DropColumn("dbo.MainTasks", "EarlyFinish");
            DropColumn("dbo.MainTasks", "EarlyStart");
        }
    }
}
