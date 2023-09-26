using System.ComponentModel.DataAnnotations;

namespace HerrenHaus_API.Models.Dto
{
    /// <summary>
    /// This class represent HerrenHaus object model
    /// </summary>
    public class HerrenHausDto
    {
        /// <summary>
        /// Id Field
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// Name Field
        /// </summary>
        [Required]
        [MaxLength(20)]
        public string Name { get; set; }
        /// <summary>
        /// Location Field
        /// </summary>
        public string Location { get; set; }
        /// <summary>
        /// Price Field
        /// </summary>
        public string price { get; set; }
    }
}
