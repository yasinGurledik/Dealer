using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Dealer.Domain.Interfaces;

public interface IRepository<T> where T: class
{
	Task<List<T>> GetFilterAndIncludeAsync(Expression<Func<T, bool>>? filter = null, params Expression<Func<T, object>>[] includeProperties);
	Task<List<T>> GetFilterAsync(Expression<Func<T, bool>>? filter = null);
	Task<List<T>> GetAllAsync();
	Task<T?> GetByIdAsync(int id);
	Task AddAsync(T entity);
	Task UpdateAsync(T entity);
	Task DeleteAsync(T entity);
}
