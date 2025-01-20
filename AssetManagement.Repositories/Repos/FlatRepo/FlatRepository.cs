using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AssetManagement.Database.data;
using AssetManagement.Models.db;
using AssetManagement.Repositories.IRepos.IFlatRepo;

namespace AssetManagement.Repositories.Repos.FlatRepo
{
    public class FlatRepository : Repository<Flat>, IFlatRepository
    {
        private readonly AssetManagementDbContext _context;
        public FlatRepository(AssetManagementDbContext context) : base(context)
        {
            _context = context;
        }

        public void Update(Flat flat)
        {
            _context.Update(flat);
        }
    }
}