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
        public string Name { get; set; }
    }
}
