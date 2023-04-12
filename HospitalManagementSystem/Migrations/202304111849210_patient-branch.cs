namespace HospitalManagementSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class patientbranch : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Patients", "BranchId", c => c.Int(nullable: false));
            CreateIndex("dbo.Patients", "BranchId");
            AddForeignKey("dbo.Patients", "BranchId", "dbo.Branches", "BranchId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Patients", "BranchId", "dbo.Branches");
            DropIndex("dbo.Patients", new[] { "BranchId" });
            DropColumn("dbo.Patients", "BranchId");
        }
    }
}
