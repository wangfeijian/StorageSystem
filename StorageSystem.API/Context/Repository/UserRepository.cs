using StorageSystem.Api;
using StorageSystem.Shared.Context;

namespace StorageSystem.API.Context.Repository
{
    public class UserRepository : Repository<User>, IRepository<User>
    {
        public UserRepository(StorageDbContext dbContext) : base(dbContext)
        {

        }
    }
}
