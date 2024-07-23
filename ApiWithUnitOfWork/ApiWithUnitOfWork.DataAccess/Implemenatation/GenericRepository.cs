using ApiWithUnitOfWork.DataAccess.Context;
using ApiWithUnitOfWork.Domain.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ApiWithUnitOfWork.DataAccess.Implemenatation
{
    public class GenericRepository<T>: IGenericRepository<T> where T : class
    {
        public readonly ApiDbContext _context;

        public GenericRepository(ApiDbContext context)
        {
            _context = context;
        }

        public void Add(T entity)
        {
            _context.Set<T>().Add(entity);
        }

        public void AddRange(IEnumerable<T> entities)
        {
            _context.Set<T>().AddRange(entities);
        }

        public IEnumerable<T> Find(System.Linq.Expressions.Expression<Func<T, bool>> predicate)
        {
            return _context.Set<T>().Where(predicate);
        }

        public IEnumerable<T> GetAll(System.Linq.Expressions.Expression<Func<T, bool>> predicate = null)
        {
            if (predicate == null)
            {
                return _context.Set<T>().ToList();
            }
            else
            {
                return _context.Set<T>().Where(predicate).ToList();
            }
        }

        public T GetById(int id)
        {
            return _context.Set<T>().Find(id);
        }

        public T GetById(long id)
        {
            return _context.Set<T>().Find(id);
        }

        public void Remove(T entity)
        {
            _context.Set<T>().Remove(entity);
        }

        public void RemoveRange(IEnumerable<T> entiities)
        {
            _context.Set<T>().RemoveRange(entiities);
        }
        public T Authenticate(Expression<Func<T, bool>> predicate)
        {
            return _context.Set<T>().FirstOrDefault(predicate);
        }
    }
}

