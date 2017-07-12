namespace sGrades.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FixLecurerId : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.StudentEnrolls", "LecturerId", c => c.String(nullable: false));
            DropColumn("dbo.StudentEnrolls", "Lecturer");
        }
        
        public override void Down()
        {
            AddColumn("dbo.StudentEnrolls", "Lecturer", c => c.String(nullable: false));
            DropColumn("dbo.StudentEnrolls", "LecturerId");
        }
    }
}
