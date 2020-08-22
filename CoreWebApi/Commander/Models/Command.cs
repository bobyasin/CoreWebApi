using System.ComponentModel.DataAnnotations;

namespace Commander.Models
{
    public class Command
    {
        [Key]// Id propertisy otomatik olarak Pk yapılır ama Key tanımı ile PK belirtilir
        public int Id { get; set; }

        [Required] // not null demek
        [MaxLength(250)]
        public string HowTo { get; set; }
        [Required]
         public string Line { get; set; }
        [Required]
        public string Platform { get; set; }
    }
}