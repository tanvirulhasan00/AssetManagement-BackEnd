using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AssetManagement.Database.data;
using AssetManagement.Models.db;
using AssetManagement.Repositories.IRepos.ICategoryRepo;

namespace AssetManagement.Repositories.Repos.CategoryRepo
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        private readonly AssetManagementDbContext _context;
        public CategoryRepository(AssetManagementDbContext context) : base(context)
        {
            _context = context;
        }
        public void Update(Category category)
        {
            _context.Update(category);
        }
    }
}