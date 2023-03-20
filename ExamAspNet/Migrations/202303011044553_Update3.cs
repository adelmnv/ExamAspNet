namespace ExamAspNet.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update3 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Days",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DayOfWeek = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.FitProgramDay",
                c => new
                    {
                        FitProgramId = c.Int(nullable: false),
                        DayId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.FitProgramId, t.DayId })
                .ForeignKey("dbo.FitPrograms", t => t.FitProgramId, cascadeDelete: true)
                .ForeignKey("dbo.Days", t => t.DayId, cascadeDelete: true)
                .Index(t => t.FitProgramId)
                .Index(t => t.DayId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.FitProgramDay", "DayId", "dbo.Days");
            DropForeignKey("dbo.FitProgramDay", "FitProgramId", "dbo.FitPrograms");
            DropIndex("dbo.FitProgramDay", new[] { "DayId" });
            DropIndex("dbo.FitProgramDay", new[] { "FitProgramId" });
            DropTable("dbo.FitProgramDay");
            DropTable("dbo.Days");
        }
    }
}
