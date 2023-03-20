using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ExamAspNet.Models
{
    public class Day
    {
        public int Id { get; set; }
        public DayOfWeek DayOfWeek { get; set; }
        public virtual ICollection<Practice> Practices { get; set; }
        public Day() { Practices = new List<Practice>(); }
    }
}