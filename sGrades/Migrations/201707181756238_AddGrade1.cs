namespace sGrades.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddGrade1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Grades", "AssignmentGrade", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Grades", "AssignmentGrade", c => c.Int(nullable: false));
        }
    }
}
