namespace ExamAspNet.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update5 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Applications", "isAccepted", c => c.Boolean());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Applications", "isAccepted", c => c.Boolean(nullable: false));
        }
    }
}
