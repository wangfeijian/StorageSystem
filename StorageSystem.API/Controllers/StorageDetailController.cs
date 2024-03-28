using Microsoft.AspNetCore.Mvc;
using StorageSystem.API.Services;
using StorageSystem.Shared;
using StorageSystem.Shared.Dtos;
using StorageSystem.Shared.Parameters;

namespace StorageSystem.API.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class StorageDetailController : ControllerBase
    {
        private readonly IStorageDetailService service;

        public StorageDetailController(IStorageDetailService service)
        {
            this.service = service;
        }

        [HttpPost]
        public async Task<ApiResponse> Add([FromBody] StorageDetailDto storageDetailDto) => await service.AddAsync(storageDetailDto);

        [HttpDelete]
        public async Task<ApiResponse> Delete(int id) => await service.DeleteAsync(id);

        [HttpGet]
        public async Task<ApiResponse> GetAll([FromQuery] QueryParameter queryParameter) => await service.GetAllAsync(queryParameter);

        [HttpGet]
        public async Task<ApiResponse> Get(int id) => await service.GetAsync(id);
        [HttpGet]
        public async Task<ApiResponse> GetFromSn(string sn) => await service.GetAsync(sn);

        [HttpPost]
        public async Task<ApiResponse> Update([FromBody] StorageDetailDto storageDetailDto) => await service.UpdateAsync(storageDetailDto);
    }
}
