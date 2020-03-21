namespace HaircutBookingSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class CreateAdminRole : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO [dbo].[AspNetRoles] ([Id], [Name], [Discriminator]) VALUES (N'213ff961-d650-480b-b2c3-8c02ee0835f3', N'Admin', N'ApplicationRole')");
            Sql("INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'57472fbb-bcb1-4d10-af24-0f764cbc99fe', N'admin@barbershop.com', 0, N'ADj0RYrUzogse91rb4o9exPcjS/qyNTlQg6yS71p43+7CCKQG0r7EjVnyzXqfDs9hg==', N'4599e966-e5b2-48fd-a927-d03282d617e5', NULL, 0, 0, NULL, 1, 0, N'admin@barbershop.com')");
            Sql("INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'57472fbb-bcb1-4d10-af24-0f764cbc99fe', N'213ff961-d650-480b-b2c3-8c02ee0835f3')");

        }

        public override void Down()
        {
        }
    }
}
