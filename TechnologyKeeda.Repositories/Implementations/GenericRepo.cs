using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TechnologyKeeda.Repositories.Interfaces;

namespace TechnologyKeeda.Repositories.Implementations
{
    //Attach
    //Detached
    //Modified





    public class GenericRepo<T> : IGenericRepo<T> where T : class
    {
        private ApplicationDbContext _context;
        private DbSet<T> _dbset;

        public GenericRepo(ApplicationDbContext context)
        {
            _context = context;
            _dbset =  _context.Set<T>(); // _context.countries
        }

        public async void DeleteRange(List<T> entities)
        {
            _dbset.RemoveRange(entities);
            await _context.SaveChangesAsync();  
        }

        //_context.skills.Include().tolist();
        public async Task Edit(T entity)
        {
           _dbset.Attach(entity);
            _context.Entry(entity).State =  EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<T>> GetAll(Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null,
            bool disabledTracking = true)
        {
            IQueryable<T> query = _dbset;
            if(disabledTracking)
            {
                query = query.AsNoTracking();
            }
            if(filter!=null)
            {
                query =  query.Where(filter);
            }
            if(include!=null)
            {
                query = include(query);
            }
            if(orderBy!=null)
            {
                return await orderBy(query).ToListAsync();
            }
            else
            {
                return await query.ToListAsync();
            }
        }

        public async Task<T> GetById(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null, bool disabledTracking = true)
        {
            IQueryable<T> query = _dbset;
            if (disabledTracking)
            {
                query = query.AsNoTracking();
            }
            if (filter != null)
            {
                query = query.Where(filter);
            }
            if (include != null)
            {
                query = include(query);
            }
            if (orderBy != null)
            {
                return await orderBy(query).FirstOrDefaultAsync();
            }
            else
            {
                return await query.FirstOrDefaultAsync();
            }
        }

        public async Task RemoveData(T entity)
        {
            _dbset.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task Save(T entity)
        {
            //_context.countries.Add(Country)
            await _dbset.AddAsync(entity);
            await _context.SaveChangesAsync();
        }
    }
}
