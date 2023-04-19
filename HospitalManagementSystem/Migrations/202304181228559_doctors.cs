namespace HospitalManagementSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class doctors : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Doctors",
                c => new
                    {
                        DoctorId = c.Int(nullable: false, identity: true),
                        DoctorName = c.String(),
                        DoctorDescription = c.String(),
                    })
                .PrimaryKey(t => t.DoctorId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Doctors");
        }
    }
}
