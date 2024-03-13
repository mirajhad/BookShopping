using BlazorBookShop.Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace BlazorBookShop.Client.Helpers
{
    public class HttpService : IHttpService
    {
        private readonly HttpClient _httpClient;
        private JsonSerializerOptions JsonSerializerOptions = new JsonSerializerOptions()
        {
            PropertyNameCaseInsensitive = true
        };
        public HttpService(HttpClient httpClient)
        {
           _httpClient = httpClient;
        }

        public async Task<ServerResponseHelper<object>> Delete(string url)
        {
            var responseFromServer = await _httpClient.DeleteAsync(url);
            return new ServerResponseHelper<object>(responseFromServer.IsSuccessStatusCode, null, responseFromServer);
        }

        public async Task<ServerResponseHelper<T>> Get<T>(string url)
        {
            var responseFromServer =  await _httpClient.GetAsync(url);
            if(responseFromServer.IsSuccessStatusCode)
            {
                var response = await DeserializeObject<T>(responseFromServer, JsonSerializerOptions);
                 return new ServerResponseHelper<T>(true, response, responseFromServer);

           }
            else
            {
                return new ServerResponseHelper<T>(false, default, responseFromServer);
            }
            
        }
        private async Task<T> DeserializeObject<T>(HttpResponseMessage responseMessage, JsonSerializerOptions options)
        {
            var stringReponse = await responseMessage.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<T>(stringReponse, options);
        }
        public async Task<ServerResponseHelper<object>> Post<T>(string url, T Data)
        {
            var serializedData =  JsonSerializer.Serialize(Data);
            var stringContent =  new StringContent(serializedData,Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(url, stringContent);
            return new ServerResponseHelper<object>(response.IsSuccessStatusCode, null, response);
        }

        public async Task<ServerResponseHelper<object>> Put<T>(string url, T Data)
        {
            var serializeContent = JsonSerializer.Serialize(Data);
            var stringContent = new StringContent(serializeContent, Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync(url, stringContent);
            return new ServerResponseHelper<object>(response.IsSuccessStatusCode, null, response);
        }

        public async Task<Pagination<T>> GetPagination<T>(string url, PageInfoDTO pageInfo)
        {
            string combinedUrl = string.Empty;
            if(url.Contains("?"))
            {
                combinedUrl = $"{url}&pagenumber={pageInfo.PageNumber}&pagesize={pageInfo.PageSize}";
            }
            else
            {
                combinedUrl = $"{url}?pagenumber={pageInfo.PageNumber}&pagesize={pageInfo.PageSize}";
            }
            var response = await Get<T>(combinedUrl);
            int totalPages = int.Parse(response.ResponseMessage.Headers.GetValues("totalPages").FirstOrDefault());
            var pagination = new Pagination<T>
            {
                Response = response.ServerResponse,
                TotalPages = totalPages,

            };
            return pagination;

        }

        public async Task<ServerResponseHelper<TResponse>> PostData<T, TResponse>(string url, T Data)
        {
            var serializedData = JsonSerializer.Serialize(Data);
            var stringContent = new StringContent(serializedData, Encoding.UTF8, "application/json");
            var responseFromServer = await _httpClient.PostAsync(url, stringContent);
            if (responseFromServer.IsSuccessStatusCode)
            {
                var response = await DeserializeObject<TResponse>(responseFromServer, JsonSerializerOptions);
                return new ServerResponseHelper<TResponse>(true, response, responseFromServer);

            }
            else
            {
                return new ServerResponseHelper<TResponse>(false, default, responseFromServer);
            }
        }
    }
}
