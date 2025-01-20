using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AssetManagement.Models.db;

namespace AssetManagement.Repositories.IRepos.IHouseRepo
{
    public interface IHouseRepository : IRepository<House>
    {
        void Update(House house);
    }
}