using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AssetManagement.Database.data;
using AssetManagement.Models.db;
using AssetManagement.Repositories.IRepos.IHouseRepo;

namespace AssetManagement.Repositories.Repos.HouseRepo
{
    public class HouseRepository : Repository<House>, IHouseRepository
    {
        private readonly AssetManagementDbContext _context;
        public HouseRepository(AssetManagementDbContext context) : base(context)
        {
            _context = context;
        }

        public void Update(House house)
        {
            _context.Update(house);
        }
    }
}