using System.ComponentModel.DataAnnotations;

namespace DAL.Entities
{
    public class Cars
    {
        [Key]
        public int ID { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
