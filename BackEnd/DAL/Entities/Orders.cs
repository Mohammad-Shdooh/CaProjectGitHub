using System.ComponentModel.DataAnnotations;

namespace DAL.Entities
{
    public class Orders
    {
        [Key]
        public int ID { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        public string MobileNumber { get; set; }
        public string Comments { get; set; }
        [Required]
        public DateTime From { get; set; }
        [Required]
        public DateTime To { get; set; }
        [Required]
        public int CarID { get; set; }
        public Cars Car { get; set; }
    }
}
