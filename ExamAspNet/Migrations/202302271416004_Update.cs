namespace ExamAspNet.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.CardFitProgram", "CardId", "dbo.Cards");
            DropForeignKey("dbo.CardFitProgram", "FitProgramId", "dbo.FitPrograms");
            DropForeignKey("dbo.Users", "CardId", "dbo.Cards");
            DropIndex("dbo.Users", new[] { "CardId" });
            DropIndex("dbo.CardFitProgram", new[] { "CardId" });
            DropIndex("dbo.CardFitProgram", new[] { "FitProgramId" });
            CreateTable(
                "dbo.UserFitProgram",
                c => new
                    {
                        UserId = c.Int(nullable: false),
                        FitProgramId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserId, t.FitProgramId })
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.FitPrograms", t => t.FitProgramId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.FitProgramId);
            
            AddColumn("dbo.Users", "CardType", c => c.Int(nullable: false));
            AddColumn("dbo.Users", "ClassesNum", c => c.Int(nullable: false));
            DropColumn("dbo.Users", "CardId");
            DropTable("dbo.Cards");
            DropTable("dbo.CardFitProgram");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.CardFitProgram",
                c => new
                    {
                        CardId = c.Int(nullable: false),
                        FitProgramId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.CardId, t.FitProgramId });
            
            CreateTable(
                "dbo.Cards",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CardType = c.Int(nullable: false),
                        ClassesNum = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Users", "CardId", c => c.Int());
            DropForeignKey("dbo.UserFitProgram", "FitProgramId", "dbo.FitPrograms");
            DropForeignKey("dbo.UserFitProgram", "UserId", "dbo.Users");
            DropIndex("dbo.UserFitProgram", new[] { "FitProgramId" });
            DropIndex("dbo.UserFitProgram", new[] { "UserId" });
            DropColumn("dbo.Users", "ClassesNum");
            DropColumn("dbo.Users", "CardType");
            DropTable("dbo.UserFitProgram");
            CreateIndex("dbo.CardFitProgram", "FitProgramId");
            CreateIndex("dbo.CardFitProgram", "CardId");
            CreateIndex("dbo.Users", "CardId");
            AddForeignKey("dbo.Users", "CardId", "dbo.Cards", "Id");
            AddForeignKey("dbo.CardFitProgram", "FitProgramId", "dbo.FitPrograms", "Id", cascadeDelete: true);
            AddForeignKey("dbo.CardFitProgram", "CardId", "dbo.Cards", "Id", cascadeDelete: true);
        }
    }
}
