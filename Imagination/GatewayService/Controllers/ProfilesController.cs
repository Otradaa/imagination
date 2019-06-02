using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChannelService.Data.Models;
using GatewayService.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using UserService.Data.Models;

namespace GatewayService.Controllers
{
    [Route("[controller]")]
    public class ProfilesController : Controller
    {
        private readonly IGatewayService _gateway;
        private readonly ILogger _logger;

        public ProfilesController(IGatewayService gateway, ILogger<ProfilesController> logger)
        {
            _gateway = gateway;
            _logger = logger;
        }

        // список досок пользователя
        // GET api/profiles/id/boards
        [HttpGet("{id}/boards")]
        public async Task<IActionResult> GetUserBoardsList(int id)
        {
            var boardsList = await _gateway.GetUserBoardsList(id);
            if (boardsList != null)
                return View(boardsList);
            _logger.LogInformation($"%%% no user {id} boards");
            return NotFound();
        }

        // список каналов пользователя
        // GET api/profiles/id/channels
        [HttpGet("{id}/channels")]
        public async Task<IActionResult> GetUserChannelsList(int id)
        {
            var channelsList = await _gateway.GetUserChannelsList(id);
            if (channelsList != null)
                return View(channelsList);
            _logger.LogInformation($"%%% no user {id} channels");
            return NotFound();
        }

        // список подписок пользователя
        // GET api/profiles/id/subscriptions
        [HttpGet("{id}/subscriptions")]
        public async Task<IActionResult> GetUserSubscriptionsList(int id)
        {
            var subsList = await _gateway.GetUserSubscriptionsList(id);
            if (subsList != null)
                return View(subsList);
            _logger.LogInformation($"%%% no user {id} subs");
            return NotFound();
        }

        // (регистрация?)

        // получение профиля
        // GET api/profiles/id
        [HttpGet("{id}")]
        public async Task<IActionResult> Profile(int id)
        {
            var profile = await _gateway.GetUserProfile(id);
            if (profile != null)
                return View("~/Views/Profiles/Profile.cshtml",profile);
            _logger.LogInformation($"%%% no user {id}");
            return NotFound();
        }

        // доабвление доски
        // POST api/profiles/id/boards
        [HttpPost("{id}/boards")]
        public async Task<IActionResult> AddUserBoard(int id, [FromBody] UserBoard board)
        {
            var createdBoard = await _gateway.AddUserBoard(id, board);
            if (createdBoard != null)
                return View();// Created($"{id}/boards/{createdBoard.Id}", createdBoard);
            _logger.LogInformation($"%%% couldnt add the board");
            return BadRequest();
        }

        // добавление канала
        // POST api/profiles/id/channels
        [HttpPost("{id}/channels")]
        public async Task<IActionResult> AddUserChannel(int id, [FromBody] Channel channel)
        {
            var createdChannel = await _gateway.AddUserChannel(id, channel);
            if (createdChannel != null)
                return View();// Created($"{id}/channels/{createdChannel.Id}", createdChannel);
            _logger.LogInformation($"%%% couldnt add the channel");
            return BadRequest();
        }

        // 
        // POST api/profiles/id/subscriptions
        [HttpPost("{id}/subscriptions")]
        public async Task<IActionResult> AddUserSubscription(int id, [FromBody] Subscription subscription)
        {
            var createdSubscription = await _gateway.AddUserSubscription(id, subscription);
            if (createdSubscription != null)
                return View();// Created($"{id}/subscriptions/{createdSubscription.Id}", createdSubscription);
            _logger.LogInformation($"%%% couldnt add the subscription");
            return BadRequest();
        }

    }
}