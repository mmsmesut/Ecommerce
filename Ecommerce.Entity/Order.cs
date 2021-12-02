using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Ecommerce.Entity
{

    public class Order : BaseEntity
    {
        [Key]
        public int OrderId { get; set; }

        public int ProductId { get; set; }

        public int Count { get; set; }

        public Product Product { get; set; }

        public Guid UId { get; set; } 
    }
}
