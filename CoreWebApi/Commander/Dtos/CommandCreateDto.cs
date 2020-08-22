using System.ComponentModel.DataAnnotations;
namespace Commander.Dtos
{
    public class CommandCreateDto
    {
        [Required] // not null demek
        [MaxLength(250)]
        public string HowTo { get; set; }
         [Required]
        public string Line { get; set; }
         [Required]
        public string Platform { get; set; }
    }
}