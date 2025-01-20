using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AssetManagement.Database.data;
using AssetManagement.Models.db;
using AssetManagement.Repositories.IRepos.IFamilyMemberRepo;

namespace AssetManagement.Repositories.Repos.FamilyMemberRepo
{
    public class FamilyMemberRepository : Repository<FamilyMember>, IFamilyMemberRepository
    {
        private readonly AssetManagementDbContext _context;
        public FamilyMemberRepository(AssetManagementDbContext context) : base(context)
        {
            _context = context;
        }

        public void Update(FamilyMember familyMember)
        {
            _context.Update(familyMember);
        }
    }
}