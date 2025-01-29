using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AssetManagement.Database.data;
using AssetManagement.Models.Request.Generic;
using AssetManagement.Models.Response.Api;
using AssetManagement.Repositories.IRepos;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace AssetManagement.Repositories.Repos
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly AssetManagementDbContext _context;
        private readonly DbSet<T> _dbSet;


        public Repository(AssetManagementDbContext context)
        {
            _context = context;
            this._dbSet = _context.Set<T>();

        }
        public async Task<List<T>> GetAllAsync(GenericRequest<T> request)
        {
            IQueryable<T> query = request.NoTracking == true ? _dbSet.AsNoTracking() : _dbSet;
            if (request.Expression != null)
            {
                query = query.Where(request.Expression);
            }
            if (request.IncludeProperties != null)
            {
                foreach (var propery in request.IncludeProperties.Split([','], StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(propery);
                }
                ;
            }
            return await query.ToListAsync(request.CancellationToken);
        }

        public async Task<T> GetAsync(GenericRequest<T> request)
        {
            IQueryable<T> query = request.NoTracking == true ? _dbSet.AsNoTracking() : _dbSet;
            if (request.Expression != null)
            {
                query = query.Where(request.Expression);
            }
            if (request.IncludeProperties != null)
            {
                foreach (var property in request.IncludeProperties.Split([','], StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(property);
                }
                ;
            }
            var result = await query.FirstOrDefaultAsync(request.CancellationToken) ?? throw new KeyNotFoundException($"Data not found with the specified criteria.");
            return result;
        }

        public async Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public void Remove(T entity)
        {
            _dbSet.Remove(entity);
        }
    }
}