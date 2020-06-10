using TestApp.Domain.Interfaces.Repositories;
using TestApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace TestApp.Dal.Repositories
{
    public class ProductRepository : IRepository<Product>
    {
        private readonly TestAppContext _context;

        public ProductRepository(TestAppContext context)
        {
            _context = context;
        }

        public IEnumerable<Product> GetMany(
            Expression<Func<Product, bool>> filter,
            Expression<Func<Product, object>> orderBy,
            bool orderAsc = true)
        {
            IQueryable<Product> query = _context.Products;
            query = query.Where(filter);
            query = orderAsc ? query.OrderBy(orderBy) : query.OrderByDescending(orderBy);

            return query.ToList();
        }

        public IEnumerable<Product> GetMany(Expression<Func<Product, bool>> filter)
        {
            IQueryable<Product> query = _context.Products;
            query = query.Where(filter);

            return query.ToList();
        }

        public IEnumerable<Product> GetMany()
        {
            return _context.Products.ToList();
        }

        public Product GetSingle(Expression<Func<Product, bool>> filter)
        {
            return _context.Products.SingleOrDefault(filter);
        }

        public Product GetSingle()
        {
            return _context.Products.SingleOrDefault();
        }

        public void Insert(Product product)
        {
            _context.Products.Add(product);
        }

        public void Update(Product updatedProduct)
        {
            _context.Products.Update(updatedProduct);
        }

        public void Delete(Product product)
        {
            _context.Products.Remove(product);
        }

        public void Delete(Guid id)
        {
            _context.Products.Remove(_context.Products.Single(c => c.Id == id));
        }

        public bool Exists(Expression<Func<Product, bool>> filter)
        {
            return _context.Products.Any(filter);
        }

        public int Count()
        {
            return _context.Products.Count();
        }
    }
}
