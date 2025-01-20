using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AssetManagement.Models.db;

namespace AssetManagement.Repositories.IRepos.ICategoryRepo
{
    public interface ICategoryRepository : IRepository<Category>
    {
        void Update(Category category);
    }
}