using Mooni.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Mooni.Domain.Repositories
{
    public interface IBaseRepository<T> where T: Entity
    {
        Task Create(T entity);
        void Update(T entity,Guid id);
        Task<List<T>> ReadAll(Expression<Func<T,bool>> query);
        Task Delete(Guid id);
    }
}
