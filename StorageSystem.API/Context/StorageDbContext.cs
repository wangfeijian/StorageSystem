using Microsoft.EntityFrameworkCore;
using StorageSystem.Shared.Context;

namespace StorageSystem.API.Context
{
    public class StorageDbContext : DbContext
    {
        public StorageDbContext(DbContextOptions<StorageDbContext> options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<StorageDetail> StorageDetails { get; set; }
        public DbSet<StorageStatus> StorageStatuses { get; set; }
        public DbSet<StorageOutDetail> StorageOutDetails { get; set; }
    }
}
