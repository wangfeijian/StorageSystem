using Microsoft.AspNetCore.Mvc;
using StorageSystem.API.Services;
using StorageSystem.Shared;
using StorageSystem.Shared.Dtos;
using StorageSystem.Shared.Parameters;

namespace StorageSystem.API.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class StorageStatusController : ControllerBase
    {
        private readonly IStorageStatusService service;

        public StorageStatusController(IStorageStatusService service)
        {
            this.service = service;
        }

        [HttpGet]
        public async Task<ApiResponse> GetFromStr(string storageName) => await service.GetAsync(storageName);
        [HttpGet]
        public async Task<ApiResponse> Get(int id) => await service.GetAsync(id);

        [HttpGet]
        public async Task<ApiResponse> GetGroup(string storageName) => await service.GetGruopAsync(storageName);

        [HttpGet]
        public async Task<ApiResponse> GetAll([FromQuery] QueryParameter parameter) => await service.GetAllAsync(parameter);

        [HttpPost]
        public async Task<ApiResponse> Add([FromBody] StorageStatusDto storageStatusDto) => await service.AddAsync(storageStatusDto);

        [HttpPost]
        public async Task<ApiResponse> Update([FromBody] StorageStatusDto storageStatusDto) => await service.UpdateAsync(storageStatusDto);

        [HttpDelete]
        public async Task<ApiResponse> Delete(int id) => await service.DeleteAsync(id);
    }
}
