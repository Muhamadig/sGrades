namespace sGrades.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCourseEnroll : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CourseEnrolls",
                c => new
                    {
                        StudentId = c.String(nullable: false, maxLength: 9),
                        LecturerId = c.String(nullable: false, maxLength: 9),
                        Id = c.Int(nullable: false, identity: true),
                        CourseId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Courses", t => t.CourseId, cascadeDelete: true)
                .ForeignKey("dbo.Students", t => new { t.StudentId, t.LecturerId }, cascadeDelete: true)
                .Index(t => new { t.CourseId, t.StudentId, t.LecturerId }, unique: true, name: "IX_CourseEnroll");
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CourseEnrolls", new[] { "StudentId", "LecturerId" }, "dbo.Students");
            DropForeignKey("dbo.CourseEnrolls", "CourseId", "dbo.Courses");
            DropIndex("dbo.CourseEnrolls", "IX_CourseEnroll");
            DropTable("dbo.CourseEnrolls");
        }
    }
}
