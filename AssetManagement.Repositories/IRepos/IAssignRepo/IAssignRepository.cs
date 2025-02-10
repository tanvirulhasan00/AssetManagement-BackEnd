using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AssetManagement.Models.db;

namespace AssetManagement.Repositories.IRepos.IAssignRepo
{
    public interface IAssignRepository : IRepository<Assign>
    {
        void Update(Assign assign);
    }
}