using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using UserService.Data.Models;

namespace GatewayService.Services
{
    public class UserService : IUserService
    {
        private readonly HttpClient _httpClient;
        private readonly string _remoteServiceBaseUrl;

        public UserService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = new HttpClient();
            _remoteServiceBaseUrl = $"{configuration["UserUrl"]}";
        }

        public async Task<IEnumerable<UserBoard>> GetUserBoardsList(int id)
        {
            var request = new HttpRequestMessage(new HttpMethod("GET"),
                _remoteServiceBaseUrl + "/userboards/" + id.ToString());
            
            try
            {
                var response = await _httpClient.SendAsync(request);
                var boards = await response.Content.ReadAsAsync<IEnumerable<UserBoard>>();
                return boards;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public async Task<User> GetUserProfile(int id)
        {
            var request = new HttpRequestMessage(new HttpMethod("GET"),
                _remoteServiceBaseUrl + "/users/" + id.ToString());

            try
            {
                var response = await _httpClient.SendAsync(request);
                return await response.Content.ReadAsAsync<User>();
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public async Task<UserBoard> AddUserBoard(int id, UserBoard board)
        {
            var request = new HttpRequestMessage(new HttpMethod("POST"),
                _remoteServiceBaseUrl + "/userboards");
            request.Content = new StringContent(JsonConvert.SerializeObject(board),
                Encoding.UTF8, "application/json");

            try
            {
                var response = await _httpClient.SendAsync(request);
                return await response.Content.ReadAsAsync<UserBoard>();
            }
            catch (Exception e)
            {
                return null;
            }
        }
    }
}
