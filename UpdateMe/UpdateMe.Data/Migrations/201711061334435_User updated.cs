namespace UpdateMe.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Userupdated : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.AspNetUsers", "FirstName");
            DropColumn("dbo.AspNetUsers", "LastName");
            DropColumn("dbo.AspNetUsers", "Position");
            DropColumn("dbo.AspNetUsers", "Department");
            DropColumn("dbo.AspNetUsers", "IsAdmin");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "IsAdmin", c => c.Boolean(nullable: false));
            AddColumn("dbo.AspNetUsers", "Department", c => c.String(maxLength: 20));
            AddColumn("dbo.AspNetUsers", "Position", c => c.String(maxLength: 20));
            AddColumn("dbo.AspNetUsers", "LastName", c => c.String(nullable: false, maxLength: 20));
            AddColumn("dbo.AspNetUsers", "FirstName", c => c.String(nullable: false, maxLength: 20));
        }
    }
}
