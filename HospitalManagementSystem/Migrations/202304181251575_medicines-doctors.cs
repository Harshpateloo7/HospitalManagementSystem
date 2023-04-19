namespace HospitalManagementSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class medicinesdoctors : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Medicines",
                c => new
                    {
                        MedicineId = c.Int(nullable: false, identity: true),
                        MedicineName = c.String(),
                        DosePerDay = c.String(),
                    })
                .PrimaryKey(t => t.MedicineId);
            
            CreateTable(
                "dbo.MedicineDoctors",
                c => new
                    {
                        Medicine_MedicineId = c.Int(nullable: false),
                        Doctor_DoctorId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Medicine_MedicineId, t.Doctor_DoctorId })
                .ForeignKey("dbo.Medicines", t => t.Medicine_MedicineId, cascadeDelete: true)
                .ForeignKey("dbo.Doctors", t => t.Doctor_DoctorId, cascadeDelete: true)
                .Index(t => t.Medicine_MedicineId)
                .Index(t => t.Doctor_DoctorId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.MedicineDoctors", "Doctor_DoctorId", "dbo.Doctors");
            DropForeignKey("dbo.MedicineDoctors", "Medicine_MedicineId", "dbo.Medicines");
            DropIndex("dbo.MedicineDoctors", new[] { "Doctor_DoctorId" });
            DropIndex("dbo.MedicineDoctors", new[] { "Medicine_MedicineId" });
            DropTable("dbo.MedicineDoctors");
            DropTable("dbo.Medicines");
        }
    }
}
