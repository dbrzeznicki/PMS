namespace PMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class payoutBonusEdit : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PayoutBonus", "DateOfGrantiedBonuses", c => c.DateTime(nullable: false));
            DropColumn("dbo.PayoutBonus", "Month");
        }
        
        public override void Down()
        {
            AddColumn("dbo.PayoutBonus", "Month", c => c.String());
            DropColumn("dbo.PayoutBonus", "DateOfGrantiedBonuses");
        }
    }
}
