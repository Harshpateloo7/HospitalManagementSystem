namespace HospitalManagementSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class newchanges : DbMigration
    {
        public override void Up()
        {
            /*CreateTable(
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
            
            CreateTable(
              /*  "dbo.Specialists",
                c => new
                    {
                        SpecialistId = c.Int(nullable: false, identity: true),
                        SpecialistName = c.String(),
                        DoctorId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.SpecialistId)
                .ForeignKey("dbo.Doctors", t => t.DoctorId, cascadeDelete: true)
                .Index(t => t.DoctorId);*/
            
        }
        
        public override void Down()
        {
           /* DropForeignKey("dbo.Specialists", "DoctorId", "dbo.Doctors");
            DropForeignKey("dbo.Blogs", "DoctorId", "dbo.Doctors");
            DropIndex("dbo.Specialists", new[] { "DoctorId" });
            DropIndex("dbo.Blogs", new[] { "DoctorId" });
            DropTable("dbo.Specialists");
            DropTable("dbo.Blogs");*/
        }
    }
}
