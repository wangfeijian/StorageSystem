using StorageSystem.Shared;

namespace StorageSystem.API.Services
{
    public interface IBaseService<T> where T : class
    {
        public Task<ApiResponse> GetAsync(int id);
        public Task<ApiResponse> AddAsync(T model);
        public Task<ApiResponse> UpdateAsync(T model);
        public Task<ApiResponse> DeleteAsync(int id);
    }
}
