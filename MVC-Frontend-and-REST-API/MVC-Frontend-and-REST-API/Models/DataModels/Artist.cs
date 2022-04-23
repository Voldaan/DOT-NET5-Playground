using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MVC_Frontend_and_REST_API.Models.DataModels
{
    public class Artist
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        [MaxLength(50, ErrorMessage = "A maximum of {1} characters allowed")]      //This is called DataAnnotations
        public string Name { get; set; }
        [Required]
        [MaxLength(1000, ErrorMessage = "A maximum of {1} characters allowed")]
        public string Description { get; set; }
    }
}
