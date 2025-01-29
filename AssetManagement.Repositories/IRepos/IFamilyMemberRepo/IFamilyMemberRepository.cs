using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AssetManagement.Models.db;

namespace AssetManagement.Repositories.IRepos.IFamilyMemberRepo
{
    public interface IFamilyMemberRepository : IRepository<FamilyMember>
    {
        void Update(FamilyMember familyMember);
        bool IsUniqueMember(string nidNumber);
    }
}