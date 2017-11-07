namespace UpdateMe.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Answers : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Questions", "AnswersInternal", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Questions", "AnswersInternal");
        }
    }
}
