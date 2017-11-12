namespace UpdateMe.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SlidesAdded : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Courses", "Slides", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Courses", "Slides");
        }
    }
}
