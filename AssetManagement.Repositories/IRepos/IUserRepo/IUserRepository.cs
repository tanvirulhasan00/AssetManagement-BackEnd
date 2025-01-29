using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AssetManagement.Models.db;

namespace AssetManagement.Repositories.IRepos.IUserRepo
{
    public interface IUserRepository : IRepository<ApplicationUser>
    {
        void Update(ApplicationUser user);
    }
}