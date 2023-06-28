using System.ComponentModel.DataAnnotations;

namespace Microservices.CommandService.Models.DTO_s
{
    public class CommandCreateDTO
    {
        [Required]
        public string HowTo { get; set; }

        [Required]
        public string CommandLine { get; set; }
    }
}
