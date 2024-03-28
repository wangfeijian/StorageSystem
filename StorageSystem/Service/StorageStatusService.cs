using StorageSystem.Shared;
using StorageSystem.Shared.Dtos;

namespace StorageSystem.Service
{
    public class StorageStatusService : BaseService<StorageStatusDto>, IStorageStatusService
    {
        private readonly HttpRestClient client;

        public StorageStatusService(HttpRestClient client) : base(client, "StorageStatus")
        {
            this.client = client;
        }

        public async Task<ApiResponse<StorageStatusDto>> GetFirstOfDefaultAsync(string storageName)
        {
            BaseRequest request = new BaseRequest();
            request.Method = RestSharp.Method.Get;
            request.Route = $"api/StorageStatus/GetFromStr?storageName={storageName}";
            return await client.ExecuteAsync<StorageStatusDto>(request);
        }

        public async Task<ApiResponse<List<StorageStatusDto>>> GetGruopAsync(string groupName)
        {
            BaseRequest request = new BaseRequest();
            request.Method = RestSharp.Method.Get;
            request.Route = $"api/StorageStatus/GetGroup?storageName={groupName}";
            return await client.ExecuteAsync<List<StorageStatusDto>>(request);
        }

    }
}
