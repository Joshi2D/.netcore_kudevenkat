using Kudvenkat_.NET_Core.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.Metadata;
using System.ComponentModel.DataAnnotations;

namespace Kudvenkat_.NET_Core.ViewModels
{
    public class EmployeeCreateViewModel
    {

        [Required, MaxLength(20, ErrorMessage ="No more than 20 Characters")]
        public string Name { get; set; }

        [Required, MaxLength(30)]
        [Display(Name = "Office Email")]
        [RegularExpression(@"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9]+\.[a-zA-Z0-9-.]+$", ErrorMessage = "Invalid Email format")]
        public string Email { get; set; }

        [Required]
        public Dept? Department { get; set; } //---> underlying datatype of Dept enum is integer, so passing a value = "" is passing a string, which itself is an error, so we have to make
                                              //department nullable to activate required attribute.

        public IFormFile Photo { get; set; }
    }
}
