namespace HospitalManagementSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class appointments : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Appointments",
                c => new
                    {
                        AppointmentId = c.Int(nullable: false, identity: true),
                        AppointmentDateAndTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.AppointmentId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Appointments");
        }
    }
}
