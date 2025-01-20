using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AssetManagement.Database.data;
using AssetManagement.Models.db;
using AssetManagement.Models.Request.Dto;
using AssetManagement.Models.Request.Generic;
using AssetManagement.Repositories.IRepos.IDistrictRepo;

namespace AssetManagement.Repositories.Repos.DistrictRepo
{
    public class DistrictRepository : Repository<District>, IDistrictRepository
    {
        private readonly AssetManagementDbContext _context;
        public DistrictRepository(AssetManagementDbContext context) : base(context)
        {
            _context = context;
        }
        public void Update(District request)
        {
            _context.Update(request);
        }
    }
}