using System.ComponentModel.DataAnnotations;

namespace ApiGatewayService.Models
{
    public class ProductImage
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int ProductId { get; set; }

        [Required]
        public string ImageUrl { get; set; }

        public DateTime CreatedAt { get; set; }

    }
}