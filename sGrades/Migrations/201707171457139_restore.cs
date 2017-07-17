namespace sGrades.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class restore : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Assignments", "Course_Id", "dbo.Courses");
            DropForeignKey("dbo.StudentCourses", new[] { "Student_Id", "Student_LecturerId" }, "dbo.Students");
            DropForeignKey("dbo.StudentCourses", "Course_Id", "dbo.Courses");
            DropForeignKey("dbo.Grades", new[] { "Student_Id", "Student_LecturerId" }, "dbo.Students");
            DropIndex("dbo.Assignments", new[] { "Course_Id" });
            DropIndex("dbo.Grades", new[] { "Student_Id", "Student_LecturerId" });
            DropIndex("dbo.StudentCourses", new[] { "Student_Id", "Student_LecturerId" });
            DropIndex("dbo.StudentCourses", new[] { "Course_Id" });
            DropTable("dbo.Assignments");
            DropTable("dbo.Grades");
            DropTable("dbo.StudentCourses");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.StudentCourses",
                c => new
                    {
                        Student_Id = c.String(nullable: false, maxLength: 9),
                        Student_LecturerId = c.String(nullable: false, maxLength: 9),
                        Course_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Student_Id, t.Student_LecturerId, t.Course_Id });
            
            CreateTable(
                "dbo.Grades",
                c => new
                    {
                        AssignmentId = c.Int(nullable: false, identity: true),
                        AssigmentGrade = c.Int(),
                        Student_Id = c.String(maxLength: 9),
                        Student_LecturerId = c.String(maxLength: 9),
                    })
                .PrimaryKey(t => t.AssignmentId);
            
            CreateTable(
                "dbo.Assignments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Type = c.String(nullable: false),
                        Name = c.String(nullable: false),
                        Course_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateIndex("dbo.StudentCourses", "Course_Id");
            CreateIndex("dbo.StudentCourses", new[] { "Student_Id", "Student_LecturerId" });
            CreateIndex("dbo.Grades", new[] { "Student_Id", "Student_LecturerId" });
            CreateIndex("dbo.Assignments", "Course_Id");
            AddForeignKey("dbo.Grades", new[] { "Student_Id", "Student_LecturerId" }, "dbo.Students", new[] { "Id", "LecturerId" });
            AddForeignKey("dbo.StudentCourses", "Course_Id", "dbo.Courses", "Id", cascadeDelete: true);
            AddForeignKey("dbo.StudentCourses", new[] { "Student_Id", "Student_LecturerId" }, "dbo.Students", new[] { "Id", "LecturerId" }, cascadeDelete: true);
            AddForeignKey("dbo.Assignments", "Course_Id", "dbo.Courses", "Id");
        }
    }
}
