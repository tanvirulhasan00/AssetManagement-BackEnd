using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AssetManagement.Models.Request.Generic;
using Microsoft.AspNetCore.Http;

namespace AssetManagement.Repositories.IRepos
{
    public interface IRepository<T> where T : class
    {
        Task<List<T>> GetAllAsync(GenericRequest<T> request);
        Task<T> GetAsync(GenericRequest<T> request);
        Task AddAsync(T entity);
        void Remove(T entity);
        void RemoveRange(List<T> entity);
    }
}