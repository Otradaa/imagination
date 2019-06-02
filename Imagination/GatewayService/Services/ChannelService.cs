﻿using ChannelService.Data.Models;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace GatewayService.Services
{
    public class ChannelService : IChannelService
    {
        private readonly HttpClient _httpClient;
        private readonly string _remoteServiceBaseUrl;

        public ChannelService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = new HttpClient();
            _remoteServiceBaseUrl = $"{configuration["ChannelUrl"]}";
        }

        public async Task<IEnumerable<Channel>> GetUserChannelsList(int id)
        {
            var request = new HttpRequestMessage(new HttpMethod("GET"),
                _remoteServiceBaseUrl + "/channels?userid=" + id.ToString());

            try
            {
                var response = await _httpClient.SendAsync(request);
                return await response.Content.ReadAsAsync<IEnumerable<Channel>>();
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public async Task<IEnumerable<SubsResponse>> GetUserSubscriptionsList(int id)
        {
            var request = new HttpRequestMessage(new HttpMethod("GET"),
                _remoteServiceBaseUrl + "/channels?userid=" + id.ToString());

            try
            {
                var response = await _httpClient.SendAsync(request);
                return await response.Content.ReadAsAsync<IEnumerable<SubsResponse>>();
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public async Task<Channel> AddUserChannel(int id, Channel channel)
        {
            var request = new HttpRequestMessage(new HttpMethod("POST"),
                _remoteServiceBaseUrl + "/channels");
            request.Content = new StringContent(JsonConvert.SerializeObject(channel),
                Encoding.UTF8, "application/json");

            try
            {
                var response = await _httpClient.SendAsync(request);
                return await response.Content.ReadAsAsync<Channel>();
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public async Task<Subscription> AddUserSubscription(int id, Subscription subscription)
        {
            var request = new HttpRequestMessage(new HttpMethod("POST"),
                _remoteServiceBaseUrl + "/subscriptions");
            request.Content = new StringContent(JsonConvert.SerializeObject(subscription),
                Encoding.UTF8, "application/json");

            try
            {
                var response = await _httpClient.SendAsync(request);
                return await response.Content.ReadAsAsync<Subscription>();
            }
            catch (Exception e)
            {
                return null;
            }
        }
    }
}
