namespace sGrades.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CoursesId : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Courses", "Id", c => c.Int(nullable: false, identity: true));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Courses", "Id");
        }
    }
}
