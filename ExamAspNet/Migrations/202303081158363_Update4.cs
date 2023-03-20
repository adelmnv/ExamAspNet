namespace ExamAspNet.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update4 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Coaches", "Password", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Coaches", "Password");
        }
    }
}
