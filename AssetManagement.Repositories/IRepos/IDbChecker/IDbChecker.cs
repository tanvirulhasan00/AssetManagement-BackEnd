using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AssetManagement.Repositories.IRepos.IDbChecker
{
    public interface IDbChecker
    {
        Task<bool> IsDbConnectedAsync(string connectionString);
    }
}