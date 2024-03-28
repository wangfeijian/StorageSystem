using StorageSystem.Shared;
using StorageSystem.Shared.Dtos;

namespace StorageSystem.API.Services
{
    public interface IUserService
    {
        public Task<ApiResponse> LoginAsync(string account, string password);
        public Task<ApiResponse> RegisterAsync(UserDto userDto);
    }
}
