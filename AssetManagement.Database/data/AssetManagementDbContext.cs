using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AssetManagement.Models.db;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AssetManagement.Database.data
{
    public class AssetManagementDbContext(DbContextOptions<AssetManagementDbContext> options) : IdentityDbContext<ApplicationUser>(options)
    {
        public DbSet<ApplicationUser>? ApplicationUsers { get; set; }
        public DbSet<Renter>? Renters { get; set; }
        public DbSet<History>? Histories { get; set; }
        public DbSet<FamilyMember>? FamilyMembers { get; set; }
        public DbSet<Category>? Categories { get; set; }
        public DbSet<House>? Houses { get; set; }
        public DbSet<Flat>? Flats { get; set; }
        public DbSet<Area>? Areas { get; set; }
        public DbSet<Division>? Divisions { get; set; }
        public DbSet<District>? Districts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}