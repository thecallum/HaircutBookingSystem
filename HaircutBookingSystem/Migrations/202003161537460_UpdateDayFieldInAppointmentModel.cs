namespace HaircutBookingSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateDayFieldInAppointmentModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Appointments", "Date", c => c.DateTime(nullable: false));
            DropColumn("dbo.Appointments", "Day");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Appointments", "Day", c => c.DateTime(nullable: false));
            DropColumn("dbo.Appointments", "Date");
        }
    }
}
