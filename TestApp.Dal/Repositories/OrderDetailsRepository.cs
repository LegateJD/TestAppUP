using TestApp.Domain.Interfaces.Repositories;
using TestApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace TestApp.Dal.Repositories
{
    public class OrderDetailsRepository : IRepository<OrderDetails>
    {
        private readonly TestAppContext _context;

        public OrderDetailsRepository(TestAppContext context)
        {
            _context = context;
        }

        public IEnumerable<OrderDetails> GetMany(
            Expression<Func<OrderDetails, bool>> filter,
            Expression<Func<OrderDetails, object>> orderBy,
            bool orderAsc = true)
        {
            IQueryable<OrderDetails> query = _context.OrderDetailses;
            query = query.Where(filter).OrderBy(orderBy);
            query = orderAsc ? query.OrderBy(orderBy) : query.OrderByDescending(orderBy);

            return query.ToList();
        }

        public IEnumerable<OrderDetails> GetMany(Expression<Func<OrderDetails, bool>> filter)
        {
            IQueryable<OrderDetails> query = _context.OrderDetailses;
            query = query.Where(filter);

            return query.ToList();
        }

        public IEnumerable<OrderDetails> GetMany()
        {
            return _context.OrderDetailses.ToList();
        }

        public bool Exists(Expression<Func<OrderDetails, bool>> filter)
        {
            return _context.OrderDetailses.Any(filter);
        }

        public OrderDetails GetSingle(Expression<Func<OrderDetails, bool>> filter)
        {
            return _context.OrderDetailses.SingleOrDefault(filter);
        }

        public OrderDetails GetSingle()
        {
            return _context.OrderDetailses.SingleOrDefault();
        }

        public void Insert(OrderDetails entity)
        {
            _context.OrderDetailses.Add(entity);
        }

        public void Update(OrderDetails updatedEntity)
        {
            _context.OrderDetailses.Update(updatedEntity);
        }

        public void Delete(OrderDetails entity)
        {
            _context.OrderDetailses.Remove(entity);
        }

        public void Delete(Guid id)
        {
            _context.OrderDetailses.Remove(_context.OrderDetailses.Single(c => c.Id == id));
        }

        public int Count()
        {
            return _context.OrderDetailses.Count();
        }
    }
}
