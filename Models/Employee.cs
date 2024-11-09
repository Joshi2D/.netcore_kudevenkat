using System.ComponentModel.DataAnnotations;

namespace Kudvenkat_.NET_Core.Models
{
    public class Employee
    {
       
        public int Id { get; set; }

        [Required, MaxLength(20)]
        public string Name { get; set; }

        [Required, MaxLength(30)]
        [Display(Name = "Office Email")]
        [RegularExpression(@"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9]+\.[a-zA-Z0-9-.]+$", ErrorMessage ="Invalid Email format")]
        public string Email { get; set; }

        [Required]
        public Dept? Department { get; set; } //---> underlying datatype of Dept enum is integer, so passing a value = "" is passing a string, which itself is an error, so we have to make
                                             //department nullable to activate required attribute.

        public string Photopath { get; set; }
    }
}
