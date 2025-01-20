using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AssetManagement.Database.data;
using AssetManagement.Models.db;
using AssetManagement.Repositories.IRepos.IRenterRepo;

namespace AssetManagement.Repositories.Repos.RenterRepo
{
    public class RenterRepository : Repository<Renter>, IRenterRepository
    {
        private readonly AssetManagementDbContext _context;
        public RenterRepository(AssetManagementDbContext context) : base(context)
        {
            _context = context;
        }

        public void Update(Renter renter)
        {
            _context.Update(renter);
        }
    }
}