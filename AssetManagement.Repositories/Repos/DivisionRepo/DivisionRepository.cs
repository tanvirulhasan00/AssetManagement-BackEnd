using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AssetManagement.Database.data;
using AssetManagement.Models.db;
using AssetManagement.Models.Request.Dto;
using AssetManagement.Models.Request.Generic;
using AssetManagement.Repositories.IRepos.IDivisionRepo;

namespace AssetManagement.Repositories.Repos.DivisionRepo
{
    public class DivisionRepository : Repository<Division>, IDivisionRepository
    {
        private readonly AssetManagementDbContext _context;
        public DivisionRepository(AssetManagementDbContext context) : base(context)
        {
            _context = context;
        }
        public void Update(Division request)
        {
            _context.Update(request);
        }
    }
}