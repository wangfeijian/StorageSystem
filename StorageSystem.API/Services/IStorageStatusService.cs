using StorageSystem.Shared;
using StorageSystem.Shared.Dtos;
using StorageSystem.Shared.Parameters;

namespace StorageSystem.API.Services
{
    public interface IStorageStatusService : IBaseService<StorageStatusDto>
    {
        public Task<ApiResponse> GetAllAsync(QueryParameter query);
        public Task<ApiResponse> GetAsync(string storageName);
        public Task<ApiResponse> GetGruopAsync(string groupName);
    }
}
