namespace sGrades.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeStudentKey1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.StudentEnrolls", new[] { "Student_Id", "Student_LecturerId" }, "dbo.Students");
            DropIndex("dbo.StudentEnrolls", new[] { "Student_Id", "Student_LecturerId" });
            DropPrimaryKey("dbo.Students");
            AlterColumn("dbo.Students", "LecturerId", c => c.String(nullable: false, maxLength: 9));
            AlterColumn("dbo.StudentEnrolls", "Student_LecturerId", c => c.String(maxLength: 9));
            AddPrimaryKey("dbo.Students", new[] { "Id", "LecturerId" });
            CreateIndex("dbo.StudentEnrolls", new[] { "Student_Id", "Student_LecturerId" });
            AddForeignKey("dbo.StudentEnrolls", new[] { "Student_Id", "Student_LecturerId" }, "dbo.Students", new[] { "Id", "LecturerId" });
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.StudentEnrolls", new[] { "Student_Id", "Student_LecturerId" }, "dbo.Students");
            DropIndex("dbo.StudentEnrolls", new[] { "Student_Id", "Student_LecturerId" });
            DropPrimaryKey("dbo.Students");
            AlterColumn("dbo.StudentEnrolls", "Student_LecturerId", c => c.String(maxLength: 128));
            AlterColumn("dbo.Students", "LecturerId", c => c.String(nullable: false, maxLength: 128));
            AddPrimaryKey("dbo.Students", new[] { "Id", "LecturerId" });
            CreateIndex("dbo.StudentEnrolls", new[] { "Student_Id", "Student_LecturerId" });
            AddForeignKey("dbo.StudentEnrolls", new[] { "Student_Id", "Student_LecturerId" }, "dbo.Students", new[] { "Id", "LecturerId" });
        }
    }
}
