namespace PMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class First : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserID = c.Int(nullable: false, identity: true),
                        UserRoleID = c.Int(nullable: false),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Login = c.String(),
                        Password = c.String(),
                        Salary = c.Double(nullable: false),
                        PhoneNumber = c.String(),
                        Email = c.String(),
                        AccountCreationDate = c.DateTime(nullable: false),
                        HiredDate = c.DateTime(nullable: false),
                        FiredDate = c.DateTime(),
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
                .ForeignKey("dbo.UserRoles", t => t.UserRoleID, cascadeDelete: true)
                .Index(t => t.UserRoleID);
            
            CreateTable(
                "dbo.UserRoles",
                c => new
                    {
                        UserRoleID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.UserRoleID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Users", "UserRoleID", "dbo.UserRoles");
            DropIndex("dbo.Users", new[] { "UserRoleID" });
            DropTable("dbo.UserRoles");
            DropTable("dbo.Users");
        }
    }
}
