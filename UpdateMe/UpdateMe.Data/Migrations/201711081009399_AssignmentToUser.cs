namespace UpdateMe.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AssignmentToUser : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Assignments", name: "ApplicationUser_Id", newName: "ApplicationUserId");
            RenameIndex(table: "dbo.Assignments", name: "IX_ApplicationUser_Id", newName: "IX_ApplicationUserId");
            AlterColumn("dbo.Courses", "Description", c => c.String(nullable: false, maxLength: 300));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Courses", "Description", c => c.String(maxLength: 300));
            RenameIndex(table: "dbo.Assignments", name: "IX_ApplicationUserId", newName: "IX_ApplicationUser_Id");
            RenameColumn(table: "dbo.Assignments", name: "ApplicationUserId", newName: "ApplicationUser_Id");
        }
    }
}
