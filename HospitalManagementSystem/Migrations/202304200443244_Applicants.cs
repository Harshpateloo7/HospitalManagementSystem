namespace HospitalManagementSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Applicant : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Applicants",
                c => new
                    {
                        ApplicantId = c.Int(nullable: false, identity: true),
                        ApplicantName = c.String(),
                        ApplicantCoverLetter = c.String(),
                        ApplicantEmail = c.String(),
                        ApplicantPhone = c.String(),
                        ApplicantAddress = c.String(),
                    })
                .PrimaryKey(t => t.ApplicantId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Applicants");
        }
    }
}
