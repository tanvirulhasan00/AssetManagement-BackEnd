using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AssetManagement.Database.data;
using AssetManagement.Models.db;
using AssetManagement.Models.Request.Dto;
using AssetManagement.Models.Request.Generic;
using AssetManagement.Repositories.IRepos.IDivisionRepo;
using AssetManagement.Repositories.IRepos.IMonthlyPaymentStatusRepo;

namespace AssetManagement.Repositories.Repos.MonthlyPaymentStatusRepo
{
    public class MonthlyPaymentStatusRepository : Repository<MonthlyPaymentStatus>, IMonthlyPaymentStatusRepository
    {
        private readonly AssetManagementDbContext _context;
        public MonthlyPaymentStatusRepository(AssetManagementDbContext context) : base(context)
        {
            _context = context;
        }
        public void Update(MonthlyPaymentStatus request)
        {
            _context.Update(request);
        }
    }
}