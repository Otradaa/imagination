using ChannelService.Data.Models;
using GatewayService.Models;
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

        public async Task<IEnumerable<Channel>> GetTopChannels()
        {
            var request = new HttpRequestMessage(new HttpMethod("GET"),
                _remoteServiceBaseUrl + "/channels/top");

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
                _remoteServiceBaseUrl + "/subscriptions?userid=" + id.ToString());

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


        public async Task<bool> IsSubed(int cid, int uid)
        {
            var request = new HttpRequestMessage(new HttpMethod("GET"),
                _remoteServiceBaseUrl + "/subscriptions/" + cid.ToString() + "/issubed/" + uid.ToString());

            try
            {
                var response = await _httpClient.SendAsync(request);
                var b = await response.Content.ReadAsAsync<bool>();
                return b;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public async Task<Subscription> AddUserSubscription(bool issubed, Subscription subscription)
        {
            HttpRequestMessage request;
            var isReallySubed = await IsSubed(subscription.ChannelId, subscription.UserId);

            if (!isReallySubed)
            {
                request = new HttpRequestMessage(new HttpMethod("POST"),
                    _remoteServiceBaseUrl + "/subscriptions");
                request.Content = new StringContent(JsonConvert.SerializeObject(subscription),
                    Encoding.UTF8, "application/json");
            }
            else
                request = new HttpRequestMessage(new HttpMethod("DELETE"),
                    _remoteServiceBaseUrl + "/subscriptions/" + subscription.ChannelId + "?userid=" + 
                    subscription.UserId.ToString());

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

        public async Task<ChannelWithImages> GetChannelImages(int id, int userid)
        {
            var request = new HttpRequestMessage(new HttpMethod("GET"),
            _remoteServiceBaseUrl + "/channels/" + id.ToString() + "/images?userid=" + userid.ToString());

            try
            {
                var response = await _httpClient.SendAsync(request);
                var channel = await response.Content.ReadAsAsync<ChannelWithImages>();
                return channel;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public async Task<int> GetSubsCount(int id)
        {
            var request = new HttpRequestMessage(new HttpMethod("GET"),
                _remoteServiceBaseUrl + "/channels/" + id.ToString() + "/subs");

            try
            {
                var response = await _httpClient.SendAsync(request);
                return await response.Content.ReadAsAsync<int>();
            }
            catch (Exception e)
            {
                return 0;
            }
        }

        public async Task<ChannelImage> AddChannelImage(int id, int img, string descr)
        {
            var request = new HttpRequestMessage(new HttpMethod("POST"),
                _remoteServiceBaseUrl + "/channels/images");
            request.Content = new StringContent(JsonConvert.SerializeObject(new ChannelImage() { ChannelId = id, Date = DateTime.Now, Description = descr, ImageId = img }),
                Encoding.UTF8, "application/json");

            try
            {
                var response = await _httpClient.SendAsync(request);
                return await response.Content.ReadAsAsync<ChannelImage>();
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public async Task DeleteImage(int id, int imageid)
        {
            var request = new HttpRequestMessage(new HttpMethod("DELETE"),
                _remoteServiceBaseUrl + "/channels/"+id.ToString()+"/images/"+imageid.ToString());

            try
            {
                var response = await _httpClient.SendAsync(request);
                //return await response.Content.ReadAsAsync<ChannelImage>();
            }
            catch (Exception e)
            {
                //return null;
            }
        }

        public async Task DeleteChannel(int id)
        {
            var request = new HttpRequestMessage(new HttpMethod("DELETE"),
                _remoteServiceBaseUrl + "/channels/" + id.ToString());

            try
            {
                var response = await _httpClient.SendAsync(request);
                //return await response.Content.ReadAsAsync<ChannelImage>();
            }
            catch (Exception e)
            {
                //return null;
            }
        }
    }
}
