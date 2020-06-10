using TestApp.Domain.Interfaces.Repositories;
using TestApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace TestApp.Dal.Repositories
{
    public class OrderRepository : IRepository<Order>
    {
        private readonly TestAppContext _context;

        public OrderRepository(TestAppContext context)
        {
            _context = context;
        }

        public IEnumerable<Order> GetMany(
            Expression<Func<Order, bool>> filter,
            Expression<Func<Order, object>> orderBy,
            bool orderAsc = true)
        {
            IQueryable<Order> query = _context.Orders;
            query = query.Where(filter);
            query = orderAsc ? query.OrderBy(orderBy) : query.OrderByDescending(orderBy);

            return query.ToList();
        }

        public IEnumerable<Order> GetMany(Expression<Func<Order, bool>> filter)
        {
            IQueryable<Order> query = _context.Orders;
            query = query.Where(filter);

            return query.ToList();
        }

        public IEnumerable<Order> GetMany()
        {
            return _context.Orders.ToList();
        }

        public bool Exists(Expression<Func<Order, bool>> filter)
        {
            return _context.Orders.Any(filter);
        }

        public Order GetSingle(Expression<Func<Order, bool>> filter)
        {
            return _context.Orders.SingleOrDefault(filter);
        }

        public Order GetSingle()
        {
            return _context.Orders.SingleOrDefault();
        }

        public void Insert(Order entity)
        {
            _context.Orders.Add(entity);
        }

        public void Update(Order updatedEntity)
        {
            _context.Orders.Add(updatedEntity);
        }

        public void Delete(Order entity)
        {
            _context.Orders.Remove(entity);
        }

        public void Delete(Guid id)
        {
            _context.Orders.Remove(_context.Orders.Single(c => c.Id == id));
        }

        public int Count()
        {
            return _context.Orders.Count();
        }
    }
}
