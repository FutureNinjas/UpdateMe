namespace UpdateMe.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NullableAssignmentdates : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Assignments", "AssignmentDate", c => c.DateTime());
            AlterColumn("dbo.Assignments", "DueDate", c => c.DateTime());
            AlterColumn("dbo.Assignments", "CompletionDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Assignments", "CompletionDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Assignments", "DueDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Assignments", "AssignmentDate", c => c.DateTime(nullable: false));
        }
    }
}
