using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AssetManagement.Models.db;

namespace AssetManagement.Repositories.IRepos.IAreaRepo
{
    public interface IAreaRepository : IRepository<Area>
    {
        void Update(Area area);
    }
}