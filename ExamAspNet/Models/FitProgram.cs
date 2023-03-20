using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Threading;

namespace ExamAspNet.Models
{
    public class FitProgram
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public double Duration { get; set; }
        public virtual ICollection<Practice> Practices { get; set; }
        public FitProgram() { Practices = new List<Practice>(); }

        
    }

}