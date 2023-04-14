using Microsoft.EntityFrameworkCore;
using Mooni.Domain.Entities;
using Mooni.Domain.Repositories;
using Mooni.Infrastructure.Context;
using System.Linq.Expressions;

namespace Mooni.Infrastructure.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : Entity
    {
        private readonly MooniContext _context;

        public BaseRepository(MooniContext context)
        {
            _context = context;
        }

        public async Task Create(T entity)
        {
            try
            {
                await _context.AddAsync(entity);
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public async Task Delete(Guid id)
        {
            try
            {
                var entity = await _context.Set<T>().FindAsync(id);
                if (entity is not null)
                {
                    _context.Set<T>().Remove(entity);
                    await _context.SaveChangesAsync();
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public async Task<List<T>> ReadAll(Expression<Func<T, bool>> query)
        {
            try
            {
                return await _context.Set<T>().ToListAsync();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void Update(T entity, Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
