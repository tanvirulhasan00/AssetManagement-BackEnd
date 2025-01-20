using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AssetManagement.Models.db;

namespace AssetManagement.Repositories.IRepos.IRenterRepo
{
    public interface IRenterRepository : IRepository<Renter>
    {
        void Update(Renter renter);
    }
}