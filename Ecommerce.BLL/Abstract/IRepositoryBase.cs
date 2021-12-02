using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Ecommerce.BLL.Abstract
{
    //General Metod imzaları 
    public interface IRepositoryBase<T> where T : class
    {
        DbSet<T> Table { get; }

        bool Add(T Entity);

        bool Update(T entity);

        bool Delete(T entity);

        IQueryable<T> All();

        IQueryable<T> Where(Expression<Func<T, bool>> where);

        IQueryable<T> OrderBy<TKey>(Expression<Func<T, TKey>> orderBy, bool isDesc);

    }
}
