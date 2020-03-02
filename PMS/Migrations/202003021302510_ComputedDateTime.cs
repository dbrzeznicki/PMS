namespace PMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ComputedDateTime : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Users", "FiredDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Users", "FiredDate", c => c.DateTime());
        }
    }
}
