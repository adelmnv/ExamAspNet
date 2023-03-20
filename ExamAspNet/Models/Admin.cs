using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ExamAspNet.Models
{
    public class Admin
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public virtual ICollection<Application> Applications { get; set; }
    }
}