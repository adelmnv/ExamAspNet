using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ExamAspNet.Models
{
    public class Application
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public int PracticeId { get; set; }
        public string PracticeName { get; set; }
        public bool? isAccepted { get; set; }
    }
}