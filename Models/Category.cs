﻿using System.ComponentModel.DataAnnotations;

namespace ApiGatewayService.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public DateTime CreatedAt { get; set; }

    }
}