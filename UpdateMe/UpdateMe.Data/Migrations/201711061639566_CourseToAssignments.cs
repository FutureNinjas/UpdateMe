namespace UpdateMe.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CourseToAssignments : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Assignments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AssignmentDate = c.DateTime(nullable: false),
                        DueDate = c.DateTime(nullable: false),
                        CompletionDate = c.DateTime(nullable: false),
                        AssignmentStatus = c.Int(nullable: false),
                        IsMandatory = c.Boolean(nullable: false),
                        CourseId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Courses", t => t.CourseId, cascadeDelete: true)
                .Index(t => t.CourseId);
            
            CreateTable(
                "dbo.Courses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        PassScore = c.Int(nullable: false),
                        DateCreated = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Assignments", "CourseId", "dbo.Courses");
            DropIndex("dbo.Assignments", new[] { "CourseId" });
            DropTable("dbo.Courses");
            DropTable("dbo.Assignments");
        }
    }
}
