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
 
    public class CategoryRepository : RepositoryBase<Category> , ICategoryRepository
    {
        private readonly EcommerceDbContext dbContext;

        public CategoryRepository(EcommerceDbContext dbContext) : base(dbContext)
        {

        }


    }
}
