using StorageSystem.Api;
using StorageSystem.Shared.Context;

namespace StorageSystem.API.Context.Repository
{
    public class StorageStatusRepository : Repository<StorageStatus>, IRepository<StorageStatus>
    {
        public StorageStatusRepository(StorageDbContext dbContext) : base(dbContext)
        {

        }
    }
}