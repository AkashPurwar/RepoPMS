using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProductManagement.Models
{
    public class ProductData
    {
        [Key]
        public int ProductId { get; set; }
        [Required]
        public string ProductName { get; set; }
        public int CategoryId { get; set; }
        [Required]
        public float Price { get; set; }
        public float Currency { get; set; }
        [Required]
        public int Units { get; set; }
    }
}