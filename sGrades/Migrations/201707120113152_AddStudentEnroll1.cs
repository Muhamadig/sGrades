namespace sGrades.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddStudentEnroll1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.StudentEnrolls",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StudentId = c.String(nullable: false, maxLength: 9),
                        Lecturer = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Students", t => t.StudentId, cascadeDelete: true)
                .Index(t => t.StudentId);
            
            CreateTable(
                "dbo.Students",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 9),
                        FName = c.String(nullable: false),
                        LName = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.StudentEnrolls", "StudentId", "dbo.Students");
            DropIndex("dbo.StudentEnrolls", new[] { "StudentId" });
            DropTable("dbo.Students");
            DropTable("dbo.StudentEnrolls");
        }
    }
}
