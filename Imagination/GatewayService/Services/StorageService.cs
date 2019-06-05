using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using StorageService.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace GatewayService.Services
{
    public class StorageService : IStorageService
    {
        private readonly HttpClient _httpClient;
        private readonly string _remoteServiceBaseUrl;

        public StorageService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = new HttpClient();
            _remoteServiceBaseUrl = $"{configuration["StorageUrl"]}";
        }

        public async Task<IEnumerable<Image>> GetImageFiles(IEnumerable<int> ids)
        {
            var request = new HttpRequestMessage(new HttpMethod("GET"),
                _remoteServiceBaseUrl + "/images");
            request.Content = new StringContent(JsonConvert.SerializeObject(ids),
                Encoding.UTF8, "application/json");

            try
            {
                var response = await _httpClient.SendAsync(request);
                var images = await response.Content.ReadAsAsync<IEnumerable<Image>>();
                return images;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public async Task<Image> AddImage(string path, string tags)
        {
            var request = new HttpRequestMessage(new HttpMethod("POST"),
                _remoteServiceBaseUrl + "/images");
            request.Content = new StringContent(JsonConvert.SerializeObject(new Image() { Path = path, Tags = tags }),
                Encoding.UTF8, "application/json");

            try
            {
                var response = await _httpClient.SendAsync(request);
                return await response.Content.ReadAsAsync<Image>();
            }
            catch (Exception e)
            {
                return null;
            }
        }
    }
}
