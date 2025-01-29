using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AssetManagement.Database.data;
using AssetManagement.Models.db;
using AssetManagement.Repositories.IRepos.IUserRepo;
using Microsoft.AspNetCore.Hosting;

namespace AssetManagement.Repositories.Repos.UserRepo
{
    public class UserRepositoy : Repository<ApplicationUser>, IUserRepository
    {
        private readonly AssetManagementDbContext _context;
        private readonly IWebHostEnvironment _env;
        public UserRepositoy(AssetManagementDbContext context, IWebHostEnvironment env) : base(context)
        {
            _context = context;
            _env = env;
        }

        public void Update(ApplicationUser user)
        {
            _context.Update(user);
        }
    }
}