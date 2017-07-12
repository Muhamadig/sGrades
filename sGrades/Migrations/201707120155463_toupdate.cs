namespace sGrades.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class toupdate : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.StudentEnrolls", "StudentId", "dbo.Students");
            DropIndex("dbo.StudentEnrolls", new[] { "StudentId" });
            AlterColumn("dbo.StudentEnrolls", "StudentId", c => c.String(maxLength: 9));
            CreateIndex("dbo.StudentEnrolls", "StudentId");
            AddForeignKey("dbo.StudentEnrolls", "StudentId", "dbo.Students", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.StudentEnrolls", "StudentId", "dbo.Students");
            DropIndex("dbo.StudentEnrolls", new[] { "StudentId" });
            AlterColumn("dbo.StudentEnrolls", "StudentId", c => c.String(nullable: false, maxLength: 9));
            CreateIndex("dbo.StudentEnrolls", "StudentId");
            AddForeignKey("dbo.StudentEnrolls", "StudentId", "dbo.Students", "Id", cascadeDelete: true);
        }
    }
}
