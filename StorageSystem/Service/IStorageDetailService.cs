using StorageSystem.Shared;
using StorageSystem.Shared.Dtos;

namespace StorageSystem.Service
{
    public interface IStorageDetailService : IBaseService<StorageDetailDto>
    {
        Task<ApiResponse<StorageDetailDto>> GetFirstOfDefaultAsync(string sn);
    }
}
