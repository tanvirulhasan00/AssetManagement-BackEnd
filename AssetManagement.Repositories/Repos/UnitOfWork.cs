using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AssetManagement.Database.data;
using AssetManagement.Models.db;
using AssetManagement.Repositories.IRepos;
using AssetManagement.Repositories.IRepos.IAuth;
using AssetManagement.Repositories.IRepos.IDivisionRepo;
using AssetManagement.Repositories.IRepos.IDistrictRepo;
using AssetManagement.Repositories.Repos.Auth;
using AssetManagement.Repositories.Repos.DivisionRepo;
using AssetManagement.Repositories.Repos.DistrictRepo;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using AssetManagement.Repositories.IRepos.ICategoryRepo;
using AssetManagement.Repositories.Repos.CategoryRepo;
using AssetManagement.Repositories.IRepos.IAreaRepo;
using AssetManagement.Repositories.Repos.AreaRepo;
using AssetManagement.Repositories.IRepos.IHouseRepo;
using AssetManagement.Repositories.Repos.HouseRepo;
using AssetManagement.Repositories.IRepos.IFlatRepo;
using AssetManagement.Repositories.Repos.FlatRepo;
using AssetManagement.Repositories.IRepos.IRenterRepo;
using AssetManagement.Repositories.Repos.RenterRepo;
using AssetManagement.Repositories.IRepos.IFamilyMemberRepo;
using AssetManagement.Repositories.Repos.FamilyMemberRepo;
using Microsoft.AspNetCore.Hosting;
using AssetManagement.Repositories.IRepos.IUserRepo;
using AssetManagement.Repositories.Repos.UserRepo;
using AssetManagement.Repositories.IRepos.IImageUpload;
using AssetManagement.Repositories.Repos.ImageUploadRepo;
using AssetManagement.Repositories.IRepos.IHistoryRepo;
using AssetManagement.Repositories.Repos.HistoryRepo;

namespace AssetManagement.Repositories.Repos
{
    public class UnitOfWork : IUnitOfWork
    {
        public IAuthRepository Auth { get; private set; }
        public IDivisionRepository Division { get; private set; }
        public IDistrictRepository District { get; private set; }
        public ICategoryRepository Categories { get; private set; }
        public IAreaRepository Areas { get; private set; }
        public IHouseRepository Houses { get; private set; }
        public IFlatRepository Flats { get; private set; }
        public IRenterRepository Renters { get; private set; }
        public IFamilyMemberRepository FamilyMembers { get; private set; }
        public IUserRepository Users { get; private set; }
        public IImageUploadRepository Image { get; private set; }
        public IHistoryRepositoy Histories { get; private set; }
        private readonly AssetManagementDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly string SecretKey;
        private readonly IWebHostEnvironment _env;

        public UnitOfWork(AssetManagementDbContext context, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration, IWebHostEnvironment env)
        {
            _context = context;
            SecretKey = configuration["TokenSetting:SecretKey"] ?? "";
            _env = env;
            _userManager = userManager;
            _roleManager = roleManager;
            Auth = new AuthRepository(_context, _userManager, _roleManager, SecretKey, _env);
            Division = new DivisionRepository(_context);
            District = new DistrictRepository(_context);
            Categories = new CategoryRepository(_context);
            Areas = new AreaRepository(_context);
            Houses = new HouseRepository(_context);
            Flats = new FlatRepository(_context);
            Renters = new RenterRepository(_context);
            FamilyMembers = new FamilyMemberRepository(_context);
            Users = new UserRepositoy(_context, _env);
            Image = new ImageUploadRepository(_env);
            Histories = new HistoryRepository(_context);
        }
        public async Task<int> Save()
        {
            return await _context.SaveChangesAsync();
        }
    }
}