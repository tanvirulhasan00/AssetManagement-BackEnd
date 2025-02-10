using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AssetManagement.Repositories.IRepos.IAuth;
using AssetManagement.Repositories.IRepos.IDivisionRepo;
using AssetManagement.Repositories.IRepos.IDistrictRepo;
using AssetManagement.Repositories.IRepos.ICategoryRepo;
using AssetManagement.Repositories.IRepos.IAreaRepo;
using AssetManagement.Repositories.IRepos.IHouseRepo;
using AssetManagement.Repositories.IRepos.IFlatRepo;
using AssetManagement.Repositories.IRepos.IRenterRepo;
using AssetManagement.Repositories.IRepos.IFamilyMemberRepo;
using AssetManagement.Repositories.IRepos.IUserRepo;
using AssetManagement.Repositories.IRepos.IImageUpload;
using AssetManagement.Repositories.IRepos.IHistoryRepo;
using AssetManagement.Repositories.IRepos.IAssignRepo;
using AssetManagement.Repositories.IRepos.IPaymentRepo;

namespace AssetManagement.Repositories.IRepos
{
    public interface IUnitOfWork
    {
        public IAuthRepository Auth { get; }
        public IDivisionRepository Division { get; }
        public IDistrictRepository District { get; }
        public ICategoryRepository Categories { get; }
        public IAreaRepository Areas { get; }
        public IHouseRepository Houses { get; }
        public IFlatRepository Flats { get; }
        public IRenterRepository Renters { get; }
        public IFamilyMemberRepository FamilyMembers { get; }
        public IUserRepository Users { get; }
        public IImageUploadRepository Image { get; }
        public IHistoryRepositoy Histories { get; }
        public IAssignRepository Assign { get; }
        public IPaymentRepository Payment { get; }
        Task<int> Save();
    }
}