using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AssetManagement.Database.data;
using AssetManagement.Models.db;
using AssetManagement.Repositories.IRepos.IAreaRepo;

namespace AssetManagement.Repositories.Repos.AreaRepo
{
    public class AreaRepository : Repository<Area>, IAreaRepository
    {
        private readonly AssetManagementDbContext _context;
        public AreaRepository(AssetManagementDbContext context) : base(context)
        {
            _context = context;
        }
        public void Update(Area area)
        {
            _context.Update(area);
        }
    }
}