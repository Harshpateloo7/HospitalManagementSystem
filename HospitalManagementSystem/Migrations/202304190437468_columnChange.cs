namespace HospitalManagementSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class columnChange : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Parkings", "ParkingPosition", c => c.String());
            DropColumn("dbo.Parkings", "ParkingPostion");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Parkings", "ParkingPostion", c => c.String());
            DropColumn("dbo.Parkings", "ParkingPosition");
        }
    }
}
