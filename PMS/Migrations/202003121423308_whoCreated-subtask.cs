namespace PMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class whoCreatedsubtask : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Subtasks", "WhoCreated", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Subtasks", "WhoCreated");
        }
    }
}
