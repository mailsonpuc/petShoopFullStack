using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace petBack.Models
{
    public class Product
    {
        public Guid ProductId { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public string? ImagemUrl { get; set; }

    }
}