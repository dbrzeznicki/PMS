namespace PMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class liczbaDniZUrlopuUsunieta : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Vacations", "NumberOfDays");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Vacations", "NumberOfDays", c => c.Int(nullable: false));
        }
    }
}
