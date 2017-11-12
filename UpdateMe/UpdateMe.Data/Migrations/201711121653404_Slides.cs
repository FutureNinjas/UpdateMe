namespace UpdateMe.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Slides : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Slides",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Base64Img = c.String(),
                        CourseId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Courses", t => t.CourseId, cascadeDelete: true)
                .Index(t => t.CourseId);
            
            DropColumn("dbo.Courses", "Slides");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Courses", "Slides", c => c.String());
            DropForeignKey("dbo.Slides", "CourseId", "dbo.Courses");
            DropIndex("dbo.Slides", new[] { "CourseId" });
            DropTable("dbo.Slides");
        }
    }
}
