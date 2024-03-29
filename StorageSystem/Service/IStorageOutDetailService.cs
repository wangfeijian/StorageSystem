using StorageSystem.Shared;
using StorageSystem.Shared.Dtos;

namespace StorageSystem.Service
{
    public interface IStorageOutDetailService : IBaseService<StorageOutDetailDto>
    {
        Task<ApiResponse<StorageOutDetailDto>> GetFirstOfDefaultAsync(string sn);
    }
}
