using StorageSystem.Shared;
using StorageSystem.Shared.Dtos;

namespace StorageSystem.Service
{
    public class StorageOutDetailService : BaseService<StorageOutDetailDto>, IStorageOutDetailService
    {
        private readonly HttpRestClient client;

        public StorageOutDetailService(HttpRestClient client) : base(client, "StorageOutDetail")
        {
            this.client = client;
        }

        public async Task<ApiResponse<StorageOutDetailDto>> GetFirstOfDefaultAsync(string sn)
        {
            BaseRequest request = new BaseRequest();
            request.Method = RestSharp.Method.Get;
            request.Route = $"api/StorageOutDetail/GetFromSn?sn={sn}";
            return await client.ExecuteAsync<StorageOutDetailDto>(request);
        }
    }
}
