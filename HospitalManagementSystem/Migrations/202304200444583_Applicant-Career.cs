namespace HospitalManagementSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ApplicantCareer : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Applicants", "CareerId", c => c.Int(nullable: false));
            CreateIndex("dbo.Applicants", "CareerId");
            AddForeignKey("dbo.Applicants", "CareerId", "dbo.Careers", "CareerId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Applicants", "CareerId", "dbo.Careers");
            DropIndex("dbo.Applicants", new[] { "CareerId" });
            DropColumn("dbo.Applicants", "CareerId");
        }
    }
}
