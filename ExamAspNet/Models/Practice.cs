using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ExamAspNet.Models
{
    public class Practice
    {
        public int Id { get; set; }
        public Coach Coach { get; set; }
        public int CoachId { get; set; }
        public FitProgram FitProgram { get; set; }
        public int FitProgramId { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public virtual ICollection<Day> DayOfWeeks { get; set; }
        public virtual ICollection<User> Users { get; set; }
    }
}