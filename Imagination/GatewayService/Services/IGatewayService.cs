using ChannelService.Data.Models;
using GatewayService.Models;
using StorageService.Data.Models;
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
        Task<IEnumerable<Channel>> GetTopChannels();
        Task<IEnumerable<SubsResponse>> GetUserSubscriptionsList(int userId);
        Task<User> GetUserProfile(int userId);
        Task<UserBoard> AddUserBoard(int userId, UserBoard board);
        Task<Channel> AddUserChannel(int userId, Channel channel);
        Task<Subscription> AddUserSubscription(bool issubed, Subscription subscription);
        Task<BoardWithImages> GetBoardWithImages(int id);
        Task<ChannelWithImages> GetChannelWithImages(int id, int userid);
        Task LoadImageInBoard(string path, string descr, string tags, int bid);
        Task LoadImageInChannel(string path, string descr, string tags, int id);
        Task<int> AddUser(User profile);
        Task DeleteImageFromChannel(int id, int imageid);
        Task DeleteImageFromBoard(int id, int imageid);
        Task DeleteChannel(int id);
        Task DeleteBoard(int id);
        Task<IEnumerable<Image>> GetImagesByTag(string tag);

    }
}
