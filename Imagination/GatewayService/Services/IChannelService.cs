using ChannelService.Data.Models;
using GatewayService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GatewayService.Services
{
    public interface IChannelService
    {
        Task<IEnumerable<Channel>> GetUserChannelsList(int id);
        Task<IEnumerable<SubsResponse>> GetUserSubscriptionsList(int id);
        Task<Channel> AddUserChannel(int id, Channel channel);
        Task<Subscription> AddUserSubscription(int id, Subscription subscription);
        Task<ChannelWithImages> GetChannelImages(int id);
        Task<int> GetSubsCount(int id);
        Task<ChannelImage> AddChannelImage(int id, int img, string descr);
    }
}
