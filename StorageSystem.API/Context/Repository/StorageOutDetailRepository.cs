using StorageSystem.Api;
using StorageSystem.Shared.Context;

namespace StorageSystem.API.Context.Repository
{
    public class StorageOutDetailRepository : Repository<StorageOutDetail>, IRepository<StorageOutDetail>
    {
        public StorageOutDetailRepository(StorageDbContext dbContext) : base(dbContext)
        {

        }
    }
}
