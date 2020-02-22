namespace PMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class editDatabase : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Articles",
                c => new
                    {
                        ArticleID = c.Int(nullable: false, identity: true),
                        Description = c.String(),
                        Url = c.String(),
                        DateAdded = c.DateTime(nullable: false),
                        WhoAdded_UserID = c.Int(),
                    })
                .PrimaryKey(t => t.ArticleID)
                .ForeignKey("dbo.Users", t => t.WhoAdded_UserID)
                .Index(t => t.WhoAdded_UserID);
            
            CreateTable(
                "dbo.Subtasks",
                c => new
                    {
                        SubtaskID = c.Int(nullable: false, identity: true),
                        SubtaskStatusID = c.Int(nullable: false),
                        MainTaskID = c.Int(nullable: false),
                        UserID = c.Int(nullable: false),
                        Name = c.String(),
                        Description = c.String(),
                        StartTime = c.DateTime(nullable: false),
                        EndTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.SubtaskID)
                .ForeignKey("dbo.MainTasks", t => t.MainTaskID, cascadeDelete: true)
                .ForeignKey("dbo.SubtaskStatus", t => t.SubtaskStatusID, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserID, cascadeDelete: true)
                .Index(t => t.MainTaskID)
                .Index(t => t.SubtaskStatusID)
                .Index(t => t.UserID);
            
            CreateTable(
                "dbo.MainTasks",
                c => new
                    {
                        MainTaskID = c.Int(nullable: false, identity: true),
                        ProjectID = c.Int(nullable: false),
                        Name = c.String(),
                        Priority = c.Int(nullable: false),
                        StartTime = c.DateTime(nullable: false),
                        EndTime = c.DateTime(nullable: false),
                        Status = c.Boolean(nullable: false),
                        MainTask_MainTaskID = c.Int(),
                    })
                .PrimaryKey(t => t.MainTaskID)
                .ForeignKey("dbo.MainTasks", t => t.MainTask_MainTaskID)
                .ForeignKey("dbo.Projects", t => t.ProjectID, cascadeDelete: true)
                .Index(t => t.MainTask_MainTaskID)
                .Index(t => t.ProjectID);
            
            CreateTable(
                "dbo.Projects",
                c => new
                    {
                        ProjectID = c.Int(nullable: false, identity: true),
                        ClientID = c.Int(nullable: false),
                        ProjectStatusID = c.Int(nullable: false),
                        TeamID = c.Int(nullable: false),
                        Name = c.String(),
                        StartTime = c.DateTime(nullable: false),
                        EndTime = c.DateTime(nullable: false),
                        Cost = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.ProjectID)
                .ForeignKey("dbo.Clients", t => t.ClientID, cascadeDelete: true)
                .ForeignKey("dbo.ProjectStatus", t => t.ProjectStatusID, cascadeDelete: true)
                .ForeignKey("dbo.Teams", t => t.TeamID, cascadeDelete: true)
                .Index(t => t.ClientID)
                .Index(t => t.ProjectStatusID)
                .Index(t => t.TeamID);
            
            CreateTable(
                "dbo.Clients",
                c => new
                    {
                        ClientID = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                        CompanyName = c.String(),
                        NIP = c.String(),
                        REGON = c.String(),
                        Street = c.String(),
                        HouseNumber = c.String(),
                        ApartmentNumber = c.String(),
                        Postcode = c.String(),
                        City = c.String(),
                    })
                .PrimaryKey(t => t.ClientID);
            
            CreateTable(
                "dbo.ProjectStatus",
                c => new
                    {
                        ProjectStatusID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.ProjectStatusID);
            
            CreateTable(
                "dbo.Teams",
                c => new
                    {
                        TeamID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        NumberOfPeople = c.Int(nullable: false),
                        NumberOfProjects = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.TeamID);
            
            CreateTable(
                "dbo.SubtaskStatus",
                c => new
                    {
                        SubtaskStatusID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.SubtaskStatusID);
            
            CreateTable(
                "dbo.Vacations",
                c => new
                    {
                        VacationID = c.Int(nullable: false, identity: true),
                        UserID = c.Int(nullable: false),
                        VacationTypeID = c.Int(nullable: false),
                        StartVacation = c.DateTime(nullable: false),
                        EndVacation = c.DateTime(nullable: false),
                        NumberOfDays = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.VacationID)
                .ForeignKey("dbo.Users", t => t.UserID, cascadeDelete: true)
                .ForeignKey("dbo.VacationTypes", t => t.VacationTypeID, cascadeDelete: true)
                .Index(t => t.UserID)
                .Index(t => t.VacationTypeID);
            
            CreateTable(
                "dbo.VacationTypes",
                c => new
                    {
                        VacationTypeID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.VacationTypeID);
            
            CreateTable(
                "dbo.RecentActivities",
                c => new
                    {
                        RecentActivityID = c.Int(nullable: false, identity: true),
                        Description = c.String(),
                        DateAdded = c.DateTime(nullable: false),
                        ProjectName = c.String(),
                        SubtaskName = c.String(),
                        WhoAdded_UserID = c.Int(),
                    })
                .PrimaryKey(t => t.RecentActivityID)
                .ForeignKey("dbo.Users", t => t.WhoAdded_UserID)
                .Index(t => t.WhoAdded_UserID);
            
            AddColumn("dbo.Users", "TeamID", c => c.Int());
            CreateIndex("dbo.Users", "TeamID");
            AddForeignKey("dbo.Users", "TeamID", "dbo.Teams", "TeamID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.RecentActivities", "WhoAdded_UserID", "dbo.Users");
            DropForeignKey("dbo.Articles", "WhoAdded_UserID", "dbo.Users");
            DropForeignKey("dbo.Vacations", "VacationTypeID", "dbo.VacationTypes");
            DropForeignKey("dbo.Vacations", "UserID", "dbo.Users");
            DropForeignKey("dbo.Users", "TeamID", "dbo.Teams");
            DropForeignKey("dbo.Subtasks", "UserID", "dbo.Users");
            DropForeignKey("dbo.Subtasks", "SubtaskStatusID", "dbo.SubtaskStatus");
            DropForeignKey("dbo.Subtasks", "MainTaskID", "dbo.MainTasks");
            DropForeignKey("dbo.MainTasks", "ProjectID", "dbo.Projects");
            DropForeignKey("dbo.Projects", "TeamID", "dbo.Teams");
            DropForeignKey("dbo.Projects", "ProjectStatusID", "dbo.ProjectStatus");
            DropForeignKey("dbo.Projects", "ClientID", "dbo.Clients");
            DropForeignKey("dbo.MainTasks", "MainTask_MainTaskID", "dbo.MainTasks");
            DropIndex("dbo.RecentActivities", new[] { "WhoAdded_UserID" });
            DropIndex("dbo.Articles", new[] { "WhoAdded_UserID" });
            DropIndex("dbo.Vacations", new[] { "VacationTypeID" });
            DropIndex("dbo.Vacations", new[] { "UserID" });
            DropIndex("dbo.Users", new[] { "TeamID" });
            DropIndex("dbo.Subtasks", new[] { "UserID" });
            DropIndex("dbo.Subtasks", new[] { "SubtaskStatusID" });
            DropIndex("dbo.Subtasks", new[] { "MainTaskID" });
            DropIndex("dbo.MainTasks", new[] { "ProjectID" });
            DropIndex("dbo.Projects", new[] { "TeamID" });
            DropIndex("dbo.Projects", new[] { "ProjectStatusID" });
            DropIndex("dbo.Projects", new[] { "ClientID" });
            DropIndex("dbo.MainTasks", new[] { "MainTask_MainTaskID" });
            DropColumn("dbo.Users", "TeamID");
            DropTable("dbo.RecentActivities");
            DropTable("dbo.VacationTypes");
            DropTable("dbo.Vacations");
            DropTable("dbo.SubtaskStatus");
            DropTable("dbo.Teams");
            DropTable("dbo.ProjectStatus");
            DropTable("dbo.Clients");
            DropTable("dbo.Projects");
            DropTable("dbo.MainTasks");
            DropTable("dbo.Subtasks");
            DropTable("dbo.Articles");
        }
    }
}
