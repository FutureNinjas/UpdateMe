namespace UpdateMe.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Quiz : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CurrentQuizStates",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CurrentUserResult = c.Int(nullable: false),
                        CurrentQuestion = c.Int(nullable: false),
                        AssignmentId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Assignments", t => t.AssignmentId, cascadeDelete: true)
                .Index(t => t.AssignmentId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CurrentQuizStates", "AssignmentId", "dbo.Assignments");
            DropIndex("dbo.CurrentQuizStates", new[] { "AssignmentId" });
            DropTable("dbo.CurrentQuizStates");
        }
    }
}
