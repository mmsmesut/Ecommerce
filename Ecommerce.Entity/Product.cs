using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ecommerce.Entity
{

    public class Product : BaseEntity
    {
        [Key]
        public int ProductId { get; set; }

        public int CategoryId { get; set; }

        public string ProductName { get; set; }

        public decimal Price { get; set; }
        public string Title { get; set; }

        public string Url { get; set; }

        public Category Categori { get; set; }

    }

}
