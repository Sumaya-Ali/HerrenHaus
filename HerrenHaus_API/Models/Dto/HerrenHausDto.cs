using System.ComponentModel.DataAnnotations;

namespace HerrenHaus_API.Models.Dto
{
    public class HerrenHausDto
    {
        public int ID { get; set; }

        [Required]
        [MaxLength(20)]
        public string Name { get; set; }
        public string Location { get; set; }
        public string price { get; set; }
    }
}
