namespace sGrades.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddGrade : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Grades",
                c => new
                    {
                        StudentId = c.String(nullable: false, maxLength: 9),
                        LecturerId = c.String(nullable: false, maxLength: 9),
                        AssignmentId = c.Int(nullable: false),
                        AssignmentGrade = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.StudentId, t.LecturerId, t.AssignmentId })
                .ForeignKey("dbo.Assignments", t => t.AssignmentId, cascadeDelete: true)
                .ForeignKey("dbo.Students", t => new { t.StudentId, t.LecturerId }, cascadeDelete: true)
                .Index(t => new { t.StudentId, t.LecturerId })
                .Index(t => t.AssignmentId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Grades", new[] { "StudentId", "LecturerId" }, "dbo.Students");
            DropForeignKey("dbo.Grades", "AssignmentId", "dbo.Assignments");
            DropIndex("dbo.Grades", new[] { "AssignmentId" });
            DropIndex("dbo.Grades", new[] { "StudentId", "LecturerId" });
            DropTable("dbo.Grades");
        }
    }
}
