namespace HospitalManagementSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Blogfkdoctor : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Blogs",
                c => new
                    {
                        BlogId = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        CreatedDate = c.DateTime(nullable: false),
                        Content = c.String(),
                        DoctorId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.BlogId)
                .ForeignKey("dbo.Doctors", t => t.DoctorId, cascadeDelete: true)
                .Index(t => t.DoctorId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Blogs", "DoctorId", "dbo.Doctors");
            DropIndex("dbo.Blogs", new[] { "DoctorId" });
            DropTable("dbo.Blogs");
        }
    }
}
