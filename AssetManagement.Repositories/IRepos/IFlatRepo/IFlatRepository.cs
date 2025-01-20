using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AssetManagement.Models.db;

namespace AssetManagement.Repositories.IRepos.IFlatRepo
{
    public interface IFlatRepository : IRepository<Flat>
    {
        void Update(Flat flat);
    }
}