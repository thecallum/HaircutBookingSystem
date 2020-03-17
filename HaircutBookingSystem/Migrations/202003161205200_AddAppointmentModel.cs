namespace HaircutBookingSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddAppointmentModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Appointments",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Day = c.DateTime(nullable: false),
                        Slot = c.Int(nullable: false),
                        barber_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Barbers", t => t.barber_ID, cascadeDelete: true)
                .Index(t => t.barber_ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Appointments", "barber_ID", "dbo.Barbers");
            DropIndex("dbo.Appointments", new[] { "barber_ID" });
            DropTable("dbo.Appointments");
        }
    }
}
