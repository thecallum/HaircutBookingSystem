namespace HaircutBookingSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddBarberIDToAppointmentModel : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Appointments", new[] { "barber_ID" });
            AddColumn("dbo.Appointments", "BarberID", c => c.Byte(nullable: false));
            CreateIndex("dbo.Appointments", "Barber_ID");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Appointments", new[] { "Barber_ID" });
            DropColumn("dbo.Appointments", "BarberID");
            CreateIndex("dbo.Appointments", "barber_ID");
        }
    }
}
