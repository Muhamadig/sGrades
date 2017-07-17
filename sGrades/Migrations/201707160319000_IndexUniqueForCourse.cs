namespace sGrades.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class IndexUniqueForCourse : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.Courses");
            AlterColumn("dbo.Courses", "Name", c => c.String(nullable: false, maxLength: 255, unicode: false));
            AlterColumn("dbo.Courses", "Semester", c => c.String(nullable: false, maxLength: 10, unicode: false));
            AlterColumn("dbo.Courses", "LecturerId", c => c.String(nullable: false, maxLength: 9, unicode: false));
            AddPrimaryKey("dbo.Courses", "Id");
            CreateIndex("dbo.Courses", new[] { "Name", "Year", "Semester", "LecturerId" }, unique: true, name: "IX_Course");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Courses", "IX_Course");
            DropPrimaryKey("dbo.Courses");
            AlterColumn("dbo.Courses", "LecturerId", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.Courses", "Semester", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.Courses", "Name", c => c.String(nullable: false, maxLength: 128));
            AddPrimaryKey("dbo.Courses", new[] { "Name", "Year", "Semester", "LecturerId" });
        }
    }
}
