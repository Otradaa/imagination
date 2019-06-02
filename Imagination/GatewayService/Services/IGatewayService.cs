using ChannelService.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserService.Data.Models;

namespace GatewayService.Services
{
    public interface IGatewayService
    {
        Task<IEnumerable<UserBoard>> GetUserBoardsList(int userId);
        Task<IEnumerable<Channel>> GetUserChannelsList(int userId);
        Task<IEnumerable<Subscription>> GetUserSubscriptionsList(int userId);
        Task<User> GetUserProfile(int userId);
        Task<UserBoard> AddUserBoard(int userId, UserBoard board);
        Task<Channel> AddUserChannel(int userId, Channel channel);
        Task<Subscription> AddUserSubscription(int userId, Subscription subscription);
    }
}
