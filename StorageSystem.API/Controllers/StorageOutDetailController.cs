using Microsoft.AspNetCore.Mvc;
using StorageSystem.API.Services;
using StorageSystem.Shared;
using StorageSystem.Shared.Dtos;
using StorageSystem.Shared.Parameters;

namespace StorageSystem.API.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class StorageOutDetailController : ControllerBase
    {
        private readonly IStorageOutDetailService service;

        public StorageOutDetailController(IStorageOutDetailService service)
        {
            this.service = service;
        }

        [HttpPost]
        public async Task<ApiResponse> Add([FromBody] StorageOutDetailDto model) => await service.AddAsync(model);

        [HttpDelete]
        public async Task<ApiResponse> Delete(int sn) => await service.DeleteAsync(sn);

        [HttpGet]
        public async Task<ApiResponse> GetAll([FromQuery] QueryParameter parameter) => await service.GetAllAsync(parameter);

        [HttpGet]
        public async Task<ApiResponse> Get(int sn) => await service.GetAsync(sn);

        [HttpGet]
        public async Task<ApiResponse> GetFromSn(string sn) => await service.GetAsync(sn);

        [HttpPost]
        public async Task<ApiResponse> Update([FromBody] StorageOutDetailDto model) => await service.UpdateAsync(model);
    }
}
