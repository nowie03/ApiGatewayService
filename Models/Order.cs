﻿using System.ComponentModel.DataAnnotations;

namespace ApiGatewayService.Models
{
    public class Order
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int ProdudtId { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        public int UserId { get; set; }
    }
}