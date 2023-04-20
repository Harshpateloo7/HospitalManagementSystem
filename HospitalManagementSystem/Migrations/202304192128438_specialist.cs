namespace HospitalManagementSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class specialist : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Specialists",
                c => new
                    {
                        SpecialistId = c.Int(nullable: false, identity: true),
                        SpecialistName = c.String(),
                    })
                .PrimaryKey(t => t.SpecialistId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Specialists");
        }
    }
}
