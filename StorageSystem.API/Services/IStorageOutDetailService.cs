using StorageSystem.Shared;
using StorageSystem.Shared.Dtos;
using StorageSystem.Shared.Parameters;

namespace StorageSystem.API.Services
{
    public interface IStorageOutDetailService : IBaseService<StorageOutDetailDto>
    {
        public Task<ApiResponse> GetAllAsync(QueryParameter query);

        public Task<ApiResponse> GetAsync(string sn);
    }
}
