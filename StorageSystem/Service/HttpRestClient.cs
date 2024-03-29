using Newtonsoft.Json;
using RestSharp;
using StorageSystem.Shared;

namespace StorageSystem.Service
{
    public class HttpRestClient
    {
        protected readonly RestClient client;
        private readonly string apiUrl;

        public HttpRestClient(string apiUrl)
        {
            client = new RestClient();
            this.apiUrl = apiUrl;
        }

        public async Task<ApiResponse> ExecuteAsync(BaseRequest baseRequest)
        {
            var request = new RestRequest(apiUrl + baseRequest.Route, baseRequest.Method);
            request.AddHeader("Content-Type", baseRequest.ContentType);

            if (baseRequest.Parameter != null)
                request.AddBody(JsonConvert.SerializeObject(baseRequest.Parameter));

            var response = await client.ExecuteAsync(request);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
                return JsonConvert.DeserializeObject<ApiResponse>(response.Content);

            else
                return new ApiResponse()
                {
                    Status = false,
                    Result = null,
                    Message = response.ErrorMessage
                };
        }

        public async Task<ApiResponse<T>> ExecuteAsync<T>(BaseRequest baseRequest)
        {
            var request = new RestRequest(apiUrl + baseRequest.Route, baseRequest.Method);
            request.AddHeader("Content-Type", baseRequest.ContentType);

            if (baseRequest.Parameter != null)
                request.AddBody(JsonConvert.SerializeObject(baseRequest.Parameter));
            var response = await client.ExecuteAsync(request);
            try
            {
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    return JsonConvert.DeserializeObject<ApiResponse<T>>(response.Content);

                else
                    return new ApiResponse<T>()
                    {
                        Status = false,
                        Message = response.ErrorMessage
                    };
            }
            catch (Exception)
            {
                var result = JsonConvert.DeserializeObject<ApiResponse>(response.Content);
                return new ApiResponse<T>()
                {
                    Status = false,
                    Message = result.Result.ToString()
                };

            };
        }
    }
}
