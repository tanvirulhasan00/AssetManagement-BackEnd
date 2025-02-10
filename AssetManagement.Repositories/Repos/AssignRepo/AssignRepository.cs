using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AssetManagement.Database.data;
using AssetManagement.Models.db;
using AssetManagement.Repositories.IRepos.IAssignRepo;

namespace AssetManagement.Repositories.Repos.AssignRepo
{
    public class AssignRepository : Repository<Assign>, IAssignRepository
    {
        private readonly AssetManagementDbContext _context;
        public AssignRepository(AssetManagementDbContext context) : base(context)
        {
            _context = context;
        }

        public void Update(Assign assign)
        {
            _context.Update(assign);
        }
    }
}