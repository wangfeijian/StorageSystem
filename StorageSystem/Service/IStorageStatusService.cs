using StorageSystem.Shared;
using StorageSystem.Shared.Dtos;

namespace StorageSystem.Service
{
    public interface IStorageStatusService : IBaseService<StorageStatusDto>
    {
        Task<ApiResponse<StorageStatusDto>> GetFirstOfDefaultAsync(string storageName);
        public Task<ApiResponse<List<StorageStatusDto>>> GetGruopAsync(string groupName);
    }
}
