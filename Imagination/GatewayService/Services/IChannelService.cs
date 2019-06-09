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
        Task<IEnumerable<Channel>> GetTopChannels();

        Task<IEnumerable<SubsResponse>> GetUserSubscriptionsList(int id);
        Task<Channel> AddUserChannel(int id, Channel channel);
        Task<Subscription> AddUserSubscription(bool issubed, Subscription subscription);
        Task<ChannelWithImages> GetChannelImages(int id, int userid);
        Task<int> GetSubsCount(int id);
        Task<ChannelImage> AddChannelImage(int id, int img, string descr);
        Task DeleteImage(int id, int imageid);
        Task DeleteChannel(int id);

    }
}
