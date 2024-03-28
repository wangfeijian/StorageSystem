using StorageSystem.Shared;
using StorageSystem.Shared.Dtos;

namespace StorageSystem.Service
{
    public class StorageDetailService : BaseService<StorageDetailDto>, IStorageDetailService
    {
        private readonly HttpRestClient client;

        public StorageDetailService(HttpRestClient client) : base(client, "StorageDetail")
        {
            this.client = client;
        }

        public async Task<ApiResponse<StorageDetailDto>> GetFirstOfDefaultAsync(string sn)
        {
            BaseRequest request = new BaseRequest();
            request.Method = RestSharp.Method.Get;
            request.Route = $"api/StorageDetail/GetFromSn?sn={sn}";
            return await client.ExecuteAsync<StorageDetailDto>(request);
        }
    }
}
