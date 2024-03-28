using StorageSystem.Api;
using StorageSystem.Shared.Context;

namespace StorageSystem.API.Context.Repository
{
    public class StorageDetailRepository : Repository<StorageDetail>, IRepository<StorageDetail>
    {
        public StorageDetailRepository(StorageDbContext dbContext) : base(dbContext)
        {

        }
    }
}

