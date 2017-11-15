namespace UpdateMe.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Questionsproperties : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Questions", "CorrectAnswer", c => c.String());
            AddColumn("dbo.Questions", "IsAnsweredCorrectly", c => c.Boolean(nullable: false));
            DropColumn("dbo.Questions", "CorrectAnswerIndex");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Questions", "CorrectAnswerIndex", c => c.Int(nullable: false));
            DropColumn("dbo.Questions", "IsAnsweredCorrectly");
            DropColumn("dbo.Questions", "CorrectAnswer");
        }
    }
}
