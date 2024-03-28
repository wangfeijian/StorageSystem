using StorageSystem.Shared;
using StorageSystem.Shared.Parameters;

namespace StorageSystem.Service
{
    public interface IBaseService<TEntity> where TEntity : class
    {
        Task<ApiResponse<TEntity>> AddAsync(TEntity entity);

        Task<ApiResponse<TEntity>> UpdateAsync(TEntity entity);

        Task<ApiResponse> DeleteAsync(int id);

        Task<ApiResponse<TEntity>> GetFirstOfDefaultAsync(int id);

        Task<ApiResponse<PagedList<TEntity>>> GetAllAsync(QueryParameter parameter);
    }
}
