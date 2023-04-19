namespace HospitalManagementSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class doctorparking : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Parkings",
                c => new
                    {
                        ParkingId = c.Int(nullable: false, identity: true),
                        ParkingPostion = c.String(),
                        ParkingWing = c.String(),
                    })
                .PrimaryKey(t => t.ParkingId);
            
            AddColumn("dbo.Doctors", "ParkingId", c => c.Int(nullable: false));
            CreateIndex("dbo.Doctors", "ParkingId");
            AddForeignKey("dbo.Doctors", "ParkingId", "dbo.Parkings", "ParkingId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Doctors", "ParkingId", "dbo.Parkings");
            DropIndex("dbo.Doctors", new[] { "ParkingId" });
            DropColumn("dbo.Doctors", "ParkingId");
            DropTable("dbo.Parkings");
        }
    }
}
