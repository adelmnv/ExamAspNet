using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ExamAspNet.Models
{
    public class Coach
    {
        public int Id { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Sport { get; set; }
        public int Age { get; set; }
        public string Img { get; set; }
        public virtual ICollection<Practice> Practices { get; set; }
        public Coach() { Practices = new List<Practice>(); }
    }
}