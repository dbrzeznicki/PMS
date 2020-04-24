namespace PMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addResourcesToProject : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Resources",
                c => new
                    {
                        ResourcesID = c.Int(nullable: false, identity: true),
                        ProjectID = c.Int(nullable: false),
                        Name = c.String(),
                        Cost = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.ResourcesID)
                .ForeignKey("dbo.Projects", t => t.ProjectID, cascadeDelete: true)
                .Index(t => t.ProjectID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Resources", "ProjectID", "dbo.Projects");
            DropIndex("dbo.Resources", new[] { "ProjectID" });
            DropTable("dbo.Resources");
        }
    }
}
