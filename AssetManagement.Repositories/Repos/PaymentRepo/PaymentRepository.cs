using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AssetManagement.Database.data;
using AssetManagement.Models.db;
using AssetManagement.Repositories.IRepos.IPaymentRepo;

namespace AssetManagement.Repositories.Repos.PaymentRepo
{
    public class PaymentRepository : Repository<Payment>, IPaymentRepository
    {
        private readonly AssetManagementDbContext _context;
        public PaymentRepository(AssetManagementDbContext context) : base(context)
        {
            _context = context;
        }

        public void Update(Payment payment)
        {
            _context.Update(payment);
        }
    }
}