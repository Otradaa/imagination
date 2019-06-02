using ChannelService.Data.Models;
using Microsoft.Extensions.Logging;
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
        //private readonly IStorageService _storageService;
        //private readonly IAuthService authService;
        private readonly ILogger _logger;

        public GatewayService(IChannelService channelService,// IAuthService _authService,
            IUserService userService,// IStorageService storageService,
            ILogger<GatewayService> logger)
        {
            _logger = logger;
            _channelService = channelService;
            _userService = userService;
           // _storageService = storageService;
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

        public async Task<Subscription> AddUserSubscription(int userId, Subscription subscription)
        {
            return await _channelService.AddUserSubscription(userId, subscription);
        }
    }
}
