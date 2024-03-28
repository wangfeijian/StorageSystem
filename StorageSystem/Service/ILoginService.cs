using StorageSystem.Shared;
using StorageSystem.Shared.Dtos;

namespace StorageSystem.Service
{
    public interface ILoginService
    {
        Task<ApiResponse> Login(UserDto user);

        Task<ApiResponse> Resgiter(UserDto user);
    }
}
