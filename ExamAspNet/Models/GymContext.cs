using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;


namespace ExamAspNet.Models
{
    public class GymContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Coach> Coaches { get; set; }
        public DbSet<FitProgram> FitPrograms { get; set; }
        public DbSet<Day>Days { get; set; }
        public DbSet<Practice> Practices { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Application> Applications { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Coach>().HasMany(c => c.FitPrograms).WithMany(a => a.Coaches).Map(t => t.MapLeftKey("CoachId").MapRightKey("FitProgramId").ToTable("CoachFitProgram"));
            modelBuilder.Entity<User>().HasMany(c => c.Practices).WithMany(a => a.Users).Map(t => t.MapLeftKey("UserId").MapRightKey("PracticeId").ToTable("UserPractice"));
            modelBuilder.Entity<Practice>().HasMany(c => c.DayOfWeeks).WithMany(a => a.Practices).Map(t => t.MapLeftKey("PracticeId").MapRightKey("DayId").ToTable("PracticeDay"));
        }
    }

    
}