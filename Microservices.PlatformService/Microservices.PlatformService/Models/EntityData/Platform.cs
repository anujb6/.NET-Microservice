using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace Microservices.PlatformService.Models.EntityData
{
    public partial class Platform
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Publisher { get; set; }
        
        [Required]
        public string Cost { get; set; }
    }
}
