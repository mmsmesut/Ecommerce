using System;
using System.Collections.Generic;
using System.Text;

namespace Ecommerce.Entity
{
    public class BaseEntity
    {
        public DateTime CreaDate { get; set; } = DateTime.Now;

        public bool Deleted { get; set; }
    }
}
