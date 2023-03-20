namespace ExamAspNet.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingApplication : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Applications", "UserName", c => c.String());
            AddColumn("dbo.Applications", "PracticeName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Applications", "PracticeName");
            DropColumn("dbo.Applications", "UserName");
        }
    }
}
