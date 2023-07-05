using System.ComponentModel.DataAnnotations;

namespace ApiGatewayService.Models
{
    public class Cart
    {
        [Key]
        public int Id { get; set; }


        [Required]
        public int UserId { get; set; }


        public DateTime CreatedAt { get; set; }
    }
}