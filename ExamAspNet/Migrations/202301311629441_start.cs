namespace ExamAspNet.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class start : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Coaches",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Sport = c.String(),
                        Age = c.Int(nullable: false),
                        Img = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.FitPrograms",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Description = c.String(),
                        Duration = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Email = c.String(),
                        Phone = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
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
            
            CreateTable(
                "dbo.CoachFitProgram",
                c => new
                    {
                        CoachId = c.Int(nullable: false),
                        FitProgramId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.CoachId, t.FitProgramId })
                .ForeignKey("dbo.Coaches", t => t.CoachId, cascadeDelete: true)
                .ForeignKey("dbo.FitPrograms", t => t.FitProgramId, cascadeDelete: true)
                .Index(t => t.CoachId)
                .Index(t => t.FitProgramId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CoachFitProgram", "FitProgramId", "dbo.FitPrograms");
            DropForeignKey("dbo.CoachFitProgram", "CoachId", "dbo.Coaches");
            DropForeignKey("dbo.UserFitProgram", "FitProgramId", "dbo.FitPrograms");
            DropForeignKey("dbo.UserFitProgram", "UserId", "dbo.Users");
            DropIndex("dbo.CoachFitProgram", new[] { "FitProgramId" });
            DropIndex("dbo.CoachFitProgram", new[] { "CoachId" });
            DropIndex("dbo.UserFitProgram", new[] { "FitProgramId" });
            DropIndex("dbo.UserFitProgram", new[] { "UserId" });
            DropTable("dbo.CoachFitProgram");
            DropTable("dbo.UserFitProgram");
            DropTable("dbo.Users");
            DropTable("dbo.FitPrograms");
            DropTable("dbo.Coaches");
        }
    }
}
