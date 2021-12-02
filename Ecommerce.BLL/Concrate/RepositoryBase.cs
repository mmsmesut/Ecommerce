using Ecommerce.BLL.Abstract;
using Ecommerce.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Ecommerce.BLL.Concrate
{
    //Genel Base Yapısı 
    public class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        public DbSet<T> Table { get; set; }
 
        private readonly EcommerceDbContext dbContext;

        //Dependency Injection
        public RepositoryBase(EcommerceDbContext dbContext)
        {
            this.dbContext = dbContext;
            this.Table = dbContext.Set<T>();
        }

        public bool Add(T entity)
        {
            Table.Add(entity);
            return Save();
        }


        public bool Update(T entity)
        {
            Table.Update(entity);
            return Save();
        }


        public bool Delete(T entity)
        {
            Table.Remove(entity);
            return Save();
        }

        public IQueryable<T> All()
        {
            return Table;
        }

        public IQueryable<T> Where(Expression<Func<T, bool>> where)
        {
            return Table.Where(where);
        }


        public IQueryable<T> OrderBy<TKey>(Expression<Func<T, TKey>> orderBy, bool isDesc)
        {
            if (isDesc)
            {
                return Table.OrderByDescending(orderBy);
            }
            return Table.OrderBy(orderBy);
        }


        private bool Save()
        {
            try
            {
                dbContext.SaveChanges();
                return true;

            }
            catch (Exception)
            {
                //Log Exceptions for Db 
                return false;
            }
        }
    }
}
