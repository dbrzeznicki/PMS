namespace PMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class userarticle_Key : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Articles", "WhoAdded_UserID", "dbo.Users");
            DropForeignKey("dbo.Subtasks", "UserID", "dbo.Users");
            DropIndex("dbo.Articles", new[] { "WhoAdded_UserID" });
            DropIndex("dbo.Subtasks", new[] { "UserID" });
            AddColumn("dbo.Articles", "UserID", c => c.Int(nullable: false));
            AddColumn("dbo.Subtasks", "User_UserID", c => c.Int());
            AddColumn("dbo.Subtasks", "User_UserID1", c => c.Int());
            CreateIndex("dbo.Subtasks", "User_UserID");
            CreateIndex("dbo.Articles", "UserID");
            CreateIndex("dbo.Subtasks", "User_UserID1");
            AddForeignKey("dbo.Subtasks", "User_UserID", "dbo.Users", "UserID");
            AddForeignKey("dbo.Articles", "UserID", "dbo.Users", "UserID", cascadeDelete: true);
            AddForeignKey("dbo.Subtasks", "User_UserID1", "dbo.Users", "UserID");
            DropColumn("dbo.Articles", "WhoAdded_UserID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Articles", "WhoAdded_UserID", c => c.Int());
            DropForeignKey("dbo.Subtasks", "User_UserID1", "dbo.Users");
            DropForeignKey("dbo.Articles", "UserID", "dbo.Users");
            DropForeignKey("dbo.Subtasks", "User_UserID", "dbo.Users");
            DropIndex("dbo.Subtasks", new[] { "User_UserID1" });
            DropIndex("dbo.Articles", new[] { "UserID" });
            DropIndex("dbo.Subtasks", new[] { "User_UserID" });
            DropColumn("dbo.Subtasks", "User_UserID1");
            DropColumn("dbo.Subtasks", "User_UserID");
            DropColumn("dbo.Articles", "UserID");
            CreateIndex("dbo.Subtasks", "UserID");
            CreateIndex("dbo.Articles", "WhoAdded_UserID");
            AddForeignKey("dbo.Subtasks", "UserID", "dbo.Users", "UserID", cascadeDelete: true);
            AddForeignKey("dbo.Articles", "WhoAdded_UserID", "dbo.Users", "UserID");
        }
    }
}
