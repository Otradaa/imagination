using ChannelService.Data.Models;
using GatewayService.Models;
using Microsoft.Extensions.Logging;
using StorageService.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserService.Data.Models;

namespace GatewayService.Services
{
    public class GatewayService : IGatewayService
    {
        private readonly IChannelService _channelService;
        private readonly IUserService _userService;
        private readonly IStorageService _storageService;
       // private readonly IAuthService authService;
        private readonly ILogger _logger;

        public GatewayService(IChannelService channelService,// IAuthService _authService,
            IUserService userService, IStorageService storageService,
            ILogger<GatewayService> logger)
        {
            _logger = logger;
            _channelService = channelService;
            _userService = userService;
            _storageService = storageService;
            //authService = _authService;
        }

        public async Task<IEnumerable<UserBoard>> GetUserBoardsList(int userId)
        {
            return await _userService.GetUserBoardsList(userId);
        }

        public async Task<IEnumerable<Channel>> GetUserChannelsList(int userId)
        {
            return await _channelService.GetUserChannelsList(userId);
        }

        public async Task<IEnumerable<Channel>> GetTopChannels()
        {
            return await _channelService.GetTopChannels();
        }

        public async Task<IEnumerable<SubsResponse>> GetUserSubscriptionsList(int userId)
        {
            return await _channelService.GetUserSubscriptionsList(userId);
        }

        public async Task<User> GetUserProfile(int userId)
        {
            return await _userService.GetUserProfile(userId);
        }

        public async Task<UserBoard> AddUserBoard(int userId, UserBoard board)
        {
            return await _userService.AddUserBoard(userId, board);
        }

        public async Task<Channel> AddUserChannel(int userId, Channel channel)
        {
            return await _channelService.AddUserChannel(userId, channel);
        }

        public async Task<Subscription> AddUserSubscription(bool issubed, Subscription subscription)
        {
            return await _channelService.AddUserSubscription(issubed, subscription);
        }

        public async Task<BoardWithImages> GetBoardWithImages(int id)
        {
            var imagBoard = await _userService.GetBoardWithImages(id);
            // storageservice. get images by ids
            var imageIds = imagBoard.boardimages.Select(s => s.ImageId);
            imagBoard.images = await _storageService.GetImageFiles(imageIds);
            return imagBoard;

        }

        public async Task<ChannelWithImages> GetChannelWithImages(int id, int userid)
        {
            var imagChannel = await _channelService.GetChannelImages(id, userid);
            //var subs = await _channelService.GetSubsCount(id);
            var imageIds = imagChannel.images.Select(s => s.ImageId);
            imagChannel.files = await _storageService.GetImageFiles(imageIds);
            return imagChannel;

        }

        public async Task LoadImageInBoard(string path, string descr, string tags, int bid)
        {
            var image = await _storageService.AddImage(path, tags);
            await _userService.AddBoardImage(bid, image.Id, descr);
        }

        public async Task LoadImageInChannel(string path, string descr, string tags, int id)
        {
            var image = await _storageService.AddImage(path, tags);
            await _channelService.AddChannelImage(id, image.Id, descr);
        }

        public async Task<int> AddUser(User user)
        {
            return await _userService.AddUser(user);
        }

        public async Task DeleteImageFromChannel(int id, int imageid)
        {
            await _channelService.DeleteImage(id, imageid);
        }

        public async Task DeleteImageFromBoard(int id, int imageid)
        {
            await _userService.DeleteImage(id, imageid);
        }

        public async Task DeleteChannel(int id)
        {
            await _channelService.DeleteChannel(id);
        }

        public async Task DeleteBoard(int id)
        {
            await _userService.DeleteBoard(id);
        }

        public async Task<IEnumerable<Image>> GetImagesByTag(string tag)
        {
            return await _storageService.GetImagesByTag(tag);
        }
    }
}
