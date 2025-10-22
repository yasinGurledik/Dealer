using Dealer.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Dealer.Infrastructure.Data
{
	public class Repository<T> : IRepository<T> where T : class
	{
		private readonly ApplicationDbContext _context;
		private readonly DbSet<T> _dbSet;

		public Repository(ApplicationDbContext context)
		{
			_context = context;
			_dbSet = context.Set<T>();
		}

		public async Task AddAsync(T entity)
		{
			await _dbSet.AddAsync(entity);
			await _context.SaveChangesAsync();
		}
		public async Task<List<T>> GetFilterAndIncludeAsync(Expression<Func<T, bool>>? filter = null, params Expression<Func<T, object>>[] includeProperties)
		{
			IQueryable<T> query = _dbSet;
			if (filter != null) query = query.Where(filter);
			foreach (var include in includeProperties) query = query.Include(include);
			return await query.ToListAsync();
		}
		public async Task<List<T>> GetFilterAsync(Expression<Func<T, bool>>? filter = null)
		{
			IQueryable<T> query = _dbSet;
			if (filter != null) query = query.Where(filter);
			return await query.ToListAsync();
		}
		public async Task<List<T>> GetAllAsync()
		{
			IQueryable<T> query = _dbSet;
			return await query.ToListAsync();
		}
		public async Task<T?> GetByIdAsync(int id) => await _dbSet.FindAsync(id);
		

		public async Task UpdateAsync(T entity)
		{
			_dbSet.Update(entity);
			await _context.SaveChangesAsync();
		}
		public async Task DeleteAsync(T entity)
		{
			_dbSet.Remove(entity);
			await _context.SaveChangesAsync();
		}
	}
}
