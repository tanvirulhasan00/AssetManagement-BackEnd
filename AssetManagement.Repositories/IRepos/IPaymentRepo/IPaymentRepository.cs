using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AssetManagement.Models.db;

namespace AssetManagement.Repositories.IRepos.IPaymentRepo
{
    public interface IPaymentRepository : IRepository<Payment>
    {
        void Update(Payment payment);
    }
}