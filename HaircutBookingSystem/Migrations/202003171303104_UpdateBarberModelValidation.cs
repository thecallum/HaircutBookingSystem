namespace HaircutBookingSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateBarberModelValidation : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Barbers", "Name", c => c.String(nullable: false, maxLength: 255));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Barbers", "Name", c => c.String(nullable: false));
        }
    }
}
