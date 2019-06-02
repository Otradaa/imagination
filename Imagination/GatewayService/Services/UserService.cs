using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace GatewayService.Services
{
    public class UserService : IUserService
    {
        private readonly HttpClient _httpClient;
        private readonly string _remoteServiceBaseUrl;

        public UserService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = new HttpClient();
            _remoteServiceBaseUrl = $"{configuration["ConcertsUrl"]}";
        }
    }
}
