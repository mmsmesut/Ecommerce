using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Ecommerce.Entity
{
    public class Category : BaseEntity
    {
        [Key]
        public int CategoryId { get; set; }

        public string CategoryName { get; set; }

    }
}
