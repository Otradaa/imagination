using ChannelService.Data.Models;
using GatewayService.Models;
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
        Task<IEnumerable<SubsResponse>> GetUserSubscriptionsList(int userId);
        Task<User> GetUserProfile(int userId);
        Task<UserBoard> AddUserBoard(int userId, UserBoard board);
        Task<Channel> AddUserChannel(int userId, Channel channel);
        Task<Subscription> AddUserSubscription(int userId, Subscription subscription);
        Task<BoardWithImages> GetBoardWithImages(int id);
        Task<ChannelWithImages> GetChannelWithImages(int id);
        Task LoadImageInBoard(string path, string descr, string tags, int bid);
        Task LoadImageInChannel(string path, string descr, string tags, int id);
    }
}
