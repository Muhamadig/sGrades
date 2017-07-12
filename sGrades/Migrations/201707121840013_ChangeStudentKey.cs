namespace sGrades.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeStudentKey : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.StudentEnrolls", "StudentId", "dbo.Students");
            DropIndex("dbo.StudentEnrolls", new[] { "StudentId" });
            DropPrimaryKey("dbo.Students");
            AddColumn("dbo.Students", "LecturerId", c => c.String(nullable: false, maxLength: 128));
            AddColumn("dbo.StudentEnrolls", "Student_Id", c => c.String(maxLength: 9));
            AddColumn("dbo.StudentEnrolls", "Student_LecturerId", c => c.String(maxLength: 128));
            AlterColumn("dbo.StudentEnrolls", "StudentId", c => c.String());
            AddPrimaryKey("dbo.Students", new[] { "Id", "LecturerId" });
            CreateIndex("dbo.StudentEnrolls", new[] { "Student_Id", "Student_LecturerId" });
            AddForeignKey("dbo.StudentEnrolls", new[] { "Student_Id", "Student_LecturerId" }, "dbo.Students", new[] { "Id", "LecturerId" });
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.StudentEnrolls", new[] { "Student_Id", "Student_LecturerId" }, "dbo.Students");
            DropIndex("dbo.StudentEnrolls", new[] { "Student_Id", "Student_LecturerId" });
            DropPrimaryKey("dbo.Students");
            AlterColumn("dbo.StudentEnrolls", "StudentId", c => c.String(maxLength: 9));
            DropColumn("dbo.StudentEnrolls", "Student_LecturerId");
            DropColumn("dbo.StudentEnrolls", "Student_Id");
            DropColumn("dbo.Students", "LecturerId");
            AddPrimaryKey("dbo.Students", "Id");
            CreateIndex("dbo.StudentEnrolls", "StudentId");
            AddForeignKey("dbo.StudentEnrolls", "StudentId", "dbo.Students", "Id");
        }
    }
}
