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
        Task<int> Save();
    }
}