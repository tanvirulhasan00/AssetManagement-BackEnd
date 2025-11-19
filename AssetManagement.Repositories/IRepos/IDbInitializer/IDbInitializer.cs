using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AssetManagement.Repositories.IRepos.IDbInitializer
{
    public interface IDbInitializer
    {
        Task InitializeAsync();
    }
}