namespace HospitalManagementSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Career : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Careers",
                c => new
                {
                    CareerId = c.Int(nullable: false, identity: true),
                    CareerTitle = c.String(),
                    CareerDesc = c.String(),
                    CareerSalary = c.String(),
                    CareerLocation = c.String(),
                })
                .PrimaryKey(t => t.CareerId);
        }
        
        public override void Down()
        {
            DropTable("dbo.Careers");
        }
    }
}
