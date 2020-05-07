namespace PMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class editMaintask : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Articles",
                c => new
                    {
                        ArticleID = c.Int(nullable: false, identity: true),
                        UserID = c.Int(nullable: false),
                        Description = c.String(),
                        Url = c.String(),
                        DateAdded = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ArticleID)
                .ForeignKey("dbo.Users", t => t.UserID, cascadeDelete: true)
                .Index(t => t.UserID);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserID = c.Int(nullable: false, identity: true),
                        UserRoleID = c.Int(nullable: false),
                        TeamID = c.Int(),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Login = c.String(),
                        Password = c.String(),
                        Salary = c.Double(nullable: false),
                        PhoneNumber = c.String(),
                        Email = c.String(),
                        AccountCreationDate = c.DateTime(nullable: false),
                        HiredDate = c.DateTime(nullable: false),
                        FiredDate = c.DateTime(nullable: false),
                        ResidenceStreet = c.String(),
                        ResidenceHouseNumber = c.String(),
                        ResidenceApartmentNumber = c.String(),
                        ResidencePostcode = c.String(),
                        ResidenceCity = c.String(),
                        CorrespondenceStreet = c.String(),
                        CorrespondenceHouseNumber = c.String(),
                        CorrespondenceApartmentNumber = c.String(),
                        CorrespondencePostcode = c.String(),
                        CorrespondenceCity = c.String(),
                    })
                .PrimaryKey(t => t.UserID)
                .ForeignKey("dbo.Teams", t => t.TeamID)
                .ForeignKey("dbo.UserRoles", t => t.UserRoleID, cascadeDelete: true)
                .Index(t => t.UserRoleID)
                .Index(t => t.TeamID);
            
            CreateTable(
                "dbo.PayoutBonus",
                c => new
                    {
                        PayoutBonusID = c.Int(nullable: false, identity: true),
                        UserID = c.Int(nullable: false),
                        DateOfGrantiedBonuses = c.DateTime(nullable: false),
                        Value = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.PayoutBonusID)
                .ForeignKey("dbo.Users", t => t.UserID, cascadeDelete: true)
                .Index(t => t.UserID);
            
            CreateTable(
                "dbo.Subtasks",
                c => new
                    {
                        SubtaskID = c.Int(nullable: false, identity: true),
                        SubtaskStatusID = c.Int(nullable: false),
                        MainTaskID = c.Int(),
                        UserID = c.Int(nullable: false),
                        WhoCreated = c.Int(nullable: false),
                        Name = c.String(),
                        Description = c.String(),
                        Priority = c.String(),
                        StartTime = c.DateTime(nullable: false),
                        EndTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.SubtaskID)
                .ForeignKey("dbo.MainTasks", t => t.MainTaskID)
                .ForeignKey("dbo.SubtaskStatus", t => t.SubtaskStatusID, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserID, cascadeDelete: true)
                .Index(t => t.SubtaskStatusID)
                .Index(t => t.MainTaskID)
                .Index(t => t.UserID);
            
            CreateTable(
                "dbo.MainTasks",
                c => new
                    {
                        MainTaskID = c.Int(nullable: false, identity: true),
                        ProjectID = c.Int(nullable: false),
                        Name = c.String(),
                        EarlyStart = c.DateTime(nullable: false),
                        EarlyFinish = c.DateTime(nullable: false),
                        LateStart = c.DateTime(nullable: false),
                        LateFinish = c.DateTime(nullable: false),
                        Effort = c.Int(nullable: false),
                        Status = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.MainTaskID)
                .ForeignKey("dbo.Projects", t => t.ProjectID, cascadeDelete: true)
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
                "dbo.RecentActivities",
                c => new
                    {
                        RecentActivityID = c.Int(nullable: false, identity: true),
                        Description = c.String(),
                        DateAdded = c.DateTime(nullable: false),
                        TeamID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.RecentActivityID)
                .ForeignKey("dbo.Teams", t => t.TeamID, cascadeDelete: true)
                .Index(t => t.TeamID);
            
            CreateTable(
                "dbo.SubtaskStatus",
                c => new
                    {
                        SubtaskStatusID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.SubtaskStatusID);
            
            CreateTable(
                "dbo.UserRoles",
                c => new
                    {
                        UserRoleID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.UserRoleID);
            
            CreateTable(
                "dbo.Vacations",
                c => new
                    {
                        VacationID = c.Int(nullable: false, identity: true),
                        UserID = c.Int(nullable: false),
                        VacationTypeID = c.Int(nullable: false),
                        StartVacation = c.DateTime(nullable: false),
                        EndVacation = c.DateTime(nullable: false),
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
                "dbo.maintask_related",
                c => new
                    {
                        MainTaskID = c.Int(nullable: false),
                        RelatedID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.MainTaskID, t.RelatedID })
                .ForeignKey("dbo.MainTasks", t => t.MainTaskID)
                .ForeignKey("dbo.MainTasks", t => t.RelatedID)
                .Index(t => t.MainTaskID)
                .Index(t => t.RelatedID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Articles", "UserID", "dbo.Users");
            DropForeignKey("dbo.Vacations", "VacationTypeID", "dbo.VacationTypes");
            DropForeignKey("dbo.Vacations", "UserID", "dbo.Users");
            DropForeignKey("dbo.Users", "UserRoleID", "dbo.UserRoles");
            DropForeignKey("dbo.Users", "TeamID", "dbo.Teams");
            DropForeignKey("dbo.Subtasks", "UserID", "dbo.Users");
            DropForeignKey("dbo.Subtasks", "SubtaskStatusID", "dbo.SubtaskStatus");
            DropForeignKey("dbo.Subtasks", "MainTaskID", "dbo.MainTasks");
            DropForeignKey("dbo.MainTasks", "ProjectID", "dbo.Projects");
            DropForeignKey("dbo.Projects", "TeamID", "dbo.Teams");
            DropForeignKey("dbo.RecentActivities", "TeamID", "dbo.Teams");
            DropForeignKey("dbo.Resources", "ProjectID", "dbo.Projects");
            DropForeignKey("dbo.Projects", "ProjectStatusID", "dbo.ProjectStatus");
            DropForeignKey("dbo.Projects", "ClientID", "dbo.Clients");
            DropForeignKey("dbo.maintask_related", "RelatedID", "dbo.MainTasks");
            DropForeignKey("dbo.maintask_related", "MainTaskID", "dbo.MainTasks");
            DropForeignKey("dbo.PayoutBonus", "UserID", "dbo.Users");
            DropIndex("dbo.maintask_related", new[] { "RelatedID" });
            DropIndex("dbo.maintask_related", new[] { "MainTaskID" });
            DropIndex("dbo.Vacations", new[] { "VacationTypeID" });
            DropIndex("dbo.Vacations", new[] { "UserID" });
            DropIndex("dbo.RecentActivities", new[] { "TeamID" });
            DropIndex("dbo.Resources", new[] { "ProjectID" });
            DropIndex("dbo.Projects", new[] { "TeamID" });
            DropIndex("dbo.Projects", new[] { "ProjectStatusID" });
            DropIndex("dbo.Projects", new[] { "ClientID" });
            DropIndex("dbo.MainTasks", new[] { "ProjectID" });
            DropIndex("dbo.Subtasks", new[] { "UserID" });
            DropIndex("dbo.Subtasks", new[] { "MainTaskID" });
            DropIndex("dbo.Subtasks", new[] { "SubtaskStatusID" });
            DropIndex("dbo.PayoutBonus", new[] { "UserID" });
            DropIndex("dbo.Users", new[] { "TeamID" });
            DropIndex("dbo.Users", new[] { "UserRoleID" });
            DropIndex("dbo.Articles", new[] { "UserID" });
            DropTable("dbo.maintask_related");
            DropTable("dbo.VacationTypes");
            DropTable("dbo.Vacations");
            DropTable("dbo.UserRoles");
            DropTable("dbo.SubtaskStatus");
            DropTable("dbo.RecentActivities");
            DropTable("dbo.Teams");
            DropTable("dbo.Resources");
            DropTable("dbo.ProjectStatus");
            DropTable("dbo.Clients");
            DropTable("dbo.Projects");
            DropTable("dbo.MainTasks");
            DropTable("dbo.Subtasks");
            DropTable("dbo.PayoutBonus");
            DropTable("dbo.Users");
            DropTable("dbo.Articles");
        }
    }
}
