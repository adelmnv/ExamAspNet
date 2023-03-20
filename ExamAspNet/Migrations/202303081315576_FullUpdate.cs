namespace ExamAspNet.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FullUpdate : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.FitProgramDay", "FitProgramId", "dbo.FitPrograms");
            DropForeignKey("dbo.FitProgramDay", "DayId", "dbo.Days");
            DropForeignKey("dbo.UserFitProgram", "UserId", "dbo.Users");
            DropForeignKey("dbo.UserFitProgram", "FitProgramId", "dbo.FitPrograms");
            DropForeignKey("dbo.CoachFitProgram", "CoachId", "dbo.Coaches");
            DropForeignKey("dbo.CoachFitProgram", "FitProgramId", "dbo.FitPrograms");
            DropIndex("dbo.FitProgramDay", new[] { "FitProgramId" });
            DropIndex("dbo.FitProgramDay", new[] { "DayId" });
            DropIndex("dbo.UserFitProgram", new[] { "UserId" });
            DropIndex("dbo.UserFitProgram", new[] { "FitProgramId" });
            DropIndex("dbo.CoachFitProgram", new[] { "CoachId" });
            DropIndex("dbo.CoachFitProgram", new[] { "FitProgramId" });
            CreateTable(
                "dbo.Practices",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CoachId = c.Int(nullable: false),
                        FitProgramId = c.Int(nullable: false),
                        StartTime = c.String(),
                        EndTime = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Coaches", t => t.CoachId, cascadeDelete: true)
                .ForeignKey("dbo.FitPrograms", t => t.FitProgramId, cascadeDelete: true)
                .Index(t => t.CoachId)
                .Index(t => t.FitProgramId);
            
            CreateTable(
                "dbo.PracticeDay",
                c => new
                    {
                        PracticeId = c.Int(nullable: false),
                        DayId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.PracticeId, t.DayId })
                .ForeignKey("dbo.Practices", t => t.PracticeId, cascadeDelete: true)
                .ForeignKey("dbo.Days", t => t.DayId, cascadeDelete: true)
                .Index(t => t.PracticeId)
                .Index(t => t.DayId);
            
            CreateTable(
                "dbo.UserPractice",
                c => new
                    {
                        UserId = c.Int(nullable: false),
                        PracticeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserId, t.PracticeId })
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.Practices", t => t.PracticeId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.PracticeId);
            
            DropTable("dbo.FitProgramDay");
            DropTable("dbo.UserFitProgram");
            DropTable("dbo.CoachFitProgram");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.CoachFitProgram",
                c => new
                    {
                        CoachId = c.Int(nullable: false),
                        FitProgramId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.CoachId, t.FitProgramId });
            
            CreateTable(
                "dbo.UserFitProgram",
                c => new
                    {
                        UserId = c.Int(nullable: false),
                        FitProgramId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserId, t.FitProgramId });
            
            CreateTable(
                "dbo.FitProgramDay",
                c => new
                    {
                        FitProgramId = c.Int(nullable: false),
                        DayId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.FitProgramId, t.DayId });
            
            DropForeignKey("dbo.UserPractice", "PracticeId", "dbo.Practices");
            DropForeignKey("dbo.UserPractice", "UserId", "dbo.Users");
            DropForeignKey("dbo.Practices", "FitProgramId", "dbo.FitPrograms");
            DropForeignKey("dbo.PracticeDay", "DayId", "dbo.Days");
            DropForeignKey("dbo.PracticeDay", "PracticeId", "dbo.Practices");
            DropForeignKey("dbo.Practices", "CoachId", "dbo.Coaches");
            DropIndex("dbo.UserPractice", new[] { "PracticeId" });
            DropIndex("dbo.UserPractice", new[] { "UserId" });
            DropIndex("dbo.PracticeDay", new[] { "DayId" });
            DropIndex("dbo.PracticeDay", new[] { "PracticeId" });
            DropIndex("dbo.Practices", new[] { "FitProgramId" });
            DropIndex("dbo.Practices", new[] { "CoachId" });
            DropTable("dbo.UserPractice");
            DropTable("dbo.PracticeDay");
            DropTable("dbo.Practices");
            CreateIndex("dbo.CoachFitProgram", "FitProgramId");
            CreateIndex("dbo.CoachFitProgram", "CoachId");
            CreateIndex("dbo.UserFitProgram", "FitProgramId");
            CreateIndex("dbo.UserFitProgram", "UserId");
            CreateIndex("dbo.FitProgramDay", "DayId");
            CreateIndex("dbo.FitProgramDay", "FitProgramId");
            AddForeignKey("dbo.CoachFitProgram", "FitProgramId", "dbo.FitPrograms", "Id", cascadeDelete: true);
            AddForeignKey("dbo.CoachFitProgram", "CoachId", "dbo.Coaches", "Id", cascadeDelete: true);
            AddForeignKey("dbo.UserFitProgram", "FitProgramId", "dbo.FitPrograms", "Id", cascadeDelete: true);
            AddForeignKey("dbo.UserFitProgram", "UserId", "dbo.Users", "Id", cascadeDelete: true);
            AddForeignKey("dbo.FitProgramDay", "DayId", "dbo.Days", "Id", cascadeDelete: true);
            AddForeignKey("dbo.FitProgramDay", "FitProgramId", "dbo.FitPrograms", "Id", cascadeDelete: true);
        }
    }
}
