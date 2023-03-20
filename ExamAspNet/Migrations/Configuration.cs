namespace ExamAspNet.Migrations
{
    using ExamAspNet.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<ExamAspNet.Models.GymContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(ExamAspNet.Models.GymContext context)
        {

            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.

            if (!context.Coaches.Any())
            {
                context.Coaches.Add(new Coach { Id = 1, Name = "Viktor Kim", Sport = "Body Builder", Age = 27, Password = "vikTOR2035" });
                context.Coaches.Add(new Coach { Id = 2, Name = "Olesya Sinitsina", Sport = "Runner", Age = 25, Password = "Sin32Oles" });
                context.Coaches.Add(new Coach { Id = 3, Name = "Mira Antonova", Sport = "Karate", Age = 31, Password = "miraKarAnt21" });
                context.Coaches.Add(new Coach { Id = 4, Name = "Sergey Ivanov", Sport = "Box", Age = 24, Password = "SerBox64" });
                context.Coaches.Add(new Coach { Id = 5, Name = "Peter Vasiliev", Sport = "Body Builder", Age = 27, Password ="QwerPeter00" });
                context.Coaches.Add(new Coach { Id = 6, Name = "Alexandra Tkacheva", Sport = "Runner", Age = 25, Password = "ruAlexaTkach3" });
                context.Coaches.Add(new Coach { Id = 7, Name = "Betty Cooper", Sport = "Gymnastic", Age = 27, Password = "CopBet55" });
                context.SaveChanges();
            }
            if (!context.FitPrograms.Any())
            {
                context.FitPrograms.Add(new FitProgram { Id = 1, Title = "Pilates", Description = "Pilates is a form of exercise which concentrates on strengthening the body with an emphasis on core strength. This helps to improve general fitness and overall well-being. Similar to Yoga, Pilates concentrates on posture, balance and flexibility.", Duration = 1.5 });
                context.FitPrograms.Add(new FitProgram { Id = 2, Title = "Yoga", Description = "Yoga is a practice that connects the body, breath, and mind. It uses physical postures, breathing exercises, and meditation to improve overall health. Yoga was developed as a spiritual practice thousands of years ago. Today, most Westerners do yoga for exercise or to reduce stress.", Duration = 2 });
                context.FitPrograms.Add(new FitProgram { Id = 3, Title = "Grit Cardio", Description = "Cardio is a high-intensity interval training (HIIT) workout that improves cardiovascular fitness, increases speed and maximizes calorie burn. This workout uses a variety of body weight exercises and provides the challenge and intensity you need to get results fast.", Duration = 1.45 });
                context.FitPrograms.Add(new FitProgram { Id = 4, Title = "Body Balance", Description = "Body Balance is a unique workout which incorporates Yoga, Tai Chi and Pilates to help improve flexibility, build strength and leave you feeling relaxed and calm. The class will help you learn how to control your breathing and focus your mind.", Duration = 2 });
                context.FitPrograms.Add(new FitProgram { Id = 5, Title = "Body Combat", Description = "Body Combat is a high-energy martial arts-inspired workout that is totally non-contact. Punch and kick your way to fitness and burn up to 740 calories* in a class. No experience needed. Learn moves from Karate, Taekwondo, Boxing, Muay Thai, Capoeira and Kung Fu. Release stress, have a blast and feel like a champ.", Duration = 1 });
                context.FitPrograms.Add(new FitProgram { Id = 6, Title = "Body Pump", Description = "Body Pump is a fast-paced, barbell-based workout that's specifically designed to help you get lean, toned and fit. It uses a combination of motivating music, fantastic instructors and scientifically proven moves to help you achieve these targets more quickly than you would working out on your own.", Duration = 1.5 });
                context.SaveChanges();
            }
            if(!context.Days.Any())
            {
                context.Days.Add(new Day { Id = 1, DayOfWeek = DayOfWeek.Monday });
                context.Days.Add(new Day { Id = 2, DayOfWeek= DayOfWeek.Tuesday });
                context.Days.Add(new Day { Id = 3, DayOfWeek= DayOfWeek.Wednesday });
                context.Days.Add(new Day { Id = 4, DayOfWeek= DayOfWeek.Thursday });
                context.Days.Add(new Day { Id = 5, DayOfWeek= DayOfWeek.Friday });
                context.Days.Add(new Day { Id = 6, DayOfWeek= DayOfWeek.Saturday });
                context.Days.Add(new Day { Id = 7, DayOfWeek= DayOfWeek.Sunday });
                context.SaveChanges();
            }
            if(!context.Admins.Any())
            {
                context.Admins.Add(new Admin { Id = 1, Name = "Manager", Password = "admin1" });
            }
        }

    }
}
