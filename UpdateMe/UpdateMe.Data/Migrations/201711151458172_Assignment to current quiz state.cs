namespace UpdateMe.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Assignmenttocurrentquizstate : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.CurrentQuizStates", "AssignmentId", "dbo.Assignments");
            DropPrimaryKey("dbo.CurrentQuizStates");
            AddPrimaryKey("dbo.CurrentQuizStates", "AssignmentId");
            AddForeignKey("dbo.CurrentQuizStates", "AssignmentId", "dbo.Assignments", "Id");
            DropColumn("dbo.CurrentQuizStates", "Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.CurrentQuizStates", "Id", c => c.Int(nullable: false, identity: true));
            DropForeignKey("dbo.CurrentQuizStates", "AssignmentId", "dbo.Assignments");
            DropPrimaryKey("dbo.CurrentQuizStates");
            AddPrimaryKey("dbo.CurrentQuizStates", "Id");
            AddForeignKey("dbo.CurrentQuizStates", "AssignmentId", "dbo.Assignments", "Id", cascadeDelete: true);
        }
    }
}
