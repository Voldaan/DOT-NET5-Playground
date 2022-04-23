using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MVC_Frontend_and_REST_API.Models.ViewModels
{
    public class RegisterModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [MaxLength(50, ErrorMessage = "A maximum of {1} characters allowed")]
        public string UserName { get; set; }
        [Required]
        [MaxLength(200, ErrorMessage = "A maximum of {1} characters allowed")]
        public string Password { get; set; }
    }
}
