using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ExamAspNet.Models
{
    public class User
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "This is required field")]
        [StringLength(20, MinimumLength = 2, ErrorMessage = "Min = 2, Max=20 simbols for field")]
        public string Name { get; set; }
        [Required(ErrorMessage = "This is required field")]
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[a-z]{2,4}", ErrorMessage = "Incorrect email")]
        public string Email { get; set; }
        [Required(ErrorMessage = "This is required field")]
        [RegularExpression(@"^[\+]?[(]?[0-9]{3}[)]?[-\s\.]?[0-9]{3}[-\s\.]?[0-9]{4,6}$", ErrorMessage = "Incorrect phone number")]
        public string Phone { get; set; }
        public CardType CardType { get; set; }
        public int ClassesNum { get; set; }
        [RegularExpression(@"^https?://\S+(?:jpg|jpeg|png)$", ErrorMessage = "Incorrect image URL address")]
        public string ImageUrl { get; set; }
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Min = 2, Max=50 simbols for field")]
        public string Status { get; set; }
        public virtual ICollection<Practice> Practices { get; set; }
        public User() { Practices = new List<Practice>();}
    }

    public enum CardType
    {
        Gold,
        Silver,
        Bronze
    }
}