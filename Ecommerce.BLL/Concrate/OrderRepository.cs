using Ecommerce.BLL.Abstract;
using Ecommerce.Data;
using Ecommerce.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.BLL.Concrate
{
 
    public class OrderRepository : RepositoryBase<Order> , IOrderRepository
    {
        private readonly EcommerceDbContext dbContext;

        public OrderRepository(EcommerceDbContext dbContext) : base(dbContext)
        {

        }


    }
}
