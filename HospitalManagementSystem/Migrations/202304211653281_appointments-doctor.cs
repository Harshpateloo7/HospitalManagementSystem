namespace HospitalManagementSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class appointmentsdoctor : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DoctorAppointments",
                c => new
                    {
                        Doctor_DoctorId = c.Int(nullable: false),
                        Appointments_AppointmentId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Doctor_DoctorId, t.Appointments_AppointmentId })
                .ForeignKey("dbo.Doctors", t => t.Doctor_DoctorId, cascadeDelete: true)
                .ForeignKey("dbo.Appointments", t => t.Appointments_AppointmentId, cascadeDelete: true)
                .Index(t => t.Doctor_DoctorId)
                .Index(t => t.Appointments_AppointmentId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.DoctorAppointments", "Appointments_AppointmentId", "dbo.Appointments");
            DropForeignKey("dbo.DoctorAppointments", "Doctor_DoctorId", "dbo.Doctors");
            DropIndex("dbo.DoctorAppointments", new[] { "Appointments_AppointmentId" });
            DropIndex("dbo.DoctorAppointments", new[] { "Doctor_DoctorId" });
            DropTable("dbo.DoctorAppointments");
        }
    }
}
