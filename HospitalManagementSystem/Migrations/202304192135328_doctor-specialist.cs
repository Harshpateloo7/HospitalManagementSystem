namespace HospitalManagementSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class doctorspecialist : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Specialists", "DoctorId", c => c.Int(nullable: false));
            CreateIndex("dbo.Specialists", "DoctorId");
            AddForeignKey("dbo.Specialists", "DoctorId", "dbo.Doctors", "DoctorId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Specialists", "DoctorId", "dbo.Doctors");
            DropIndex("dbo.Specialists", new[] { "DoctorId" });
            DropColumn("dbo.Specialists", "DoctorId");
        }
    }
}
