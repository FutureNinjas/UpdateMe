namespace UpdateMe.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Userfields : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "FirstName", c => c.String(nullable: false, maxLength: 20));
            AddColumn("dbo.AspNetUsers", "LastName", c => c.String(nullable: false, maxLength: 20));
            AddColumn("dbo.AspNetUsers", "Position", c => c.String(maxLength: 20));
            AddColumn("dbo.AspNetUsers", "Department", c => c.String(maxLength: 20));
            AddColumn("dbo.AspNetUsers", "IsAdmin", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "IsAdmin");
            DropColumn("dbo.AspNetUsers", "Department");
            DropColumn("dbo.AspNetUsers", "Position");
            DropColumn("dbo.AspNetUsers", "LastName");
            DropColumn("dbo.AspNetUsers", "FirstName");
        }
    }
}
