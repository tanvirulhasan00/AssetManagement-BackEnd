using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AssetManagement.Models.db;
using AssetManagement.Models.Request.Dto;

namespace AssetManagement.Repositories.IRepos.IDistrictRepo
{
    public interface IDistrictRepository : IRepository<District>
    {
        void Update(District request);
    }
}