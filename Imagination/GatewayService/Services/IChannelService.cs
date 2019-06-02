using ChannelService.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GatewayService.Services
{
    public interface IChannelService
    {
        Task<IEnumerable<Channel>> GetUserChannelsList(int id);
        Task<IEnumerable<Subscription>> GetUserSubscriptionsList(int id);
        Task<Channel> AddUserChannel(int id, Channel channel);
        Task<Subscription> AddUserSubscription(int id, Subscription subscription);

    }
}
