using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AssetManagement.Database.data;
using AssetManagement.Models.db;
using AssetManagement.Repositories.IRepos.IHistoryRepo;


namespace AssetManagement.Repositories.Repos.HistoryRepo
{
    public class HistoryRepository : Repository<History>, IHistoryRepositoy
    {
        private readonly AssetManagementDbContext _context;
        public HistoryRepository(AssetManagementDbContext context) : base(context)
        {
            _context = context;
        }

    }
}