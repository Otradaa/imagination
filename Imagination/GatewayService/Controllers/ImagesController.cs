using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ChannelService.Data.Models;
using GatewayService.Areas.Identity.Data;
using GatewayService.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using UserService.Data.Models;

namespace GatewayService.Controllers
{
    // [Route("[controller]")]
    public class ImagesController : Controller
    {
        private readonly UserManager<Account> _userManager;
        private readonly IGatewayService _gateway;
        private readonly ILogger _logger;

        public ImagesController(IGatewayService gateway, ILogger<ProfilesController> logger,
            UserManager<Account> userManager)
        {
            _userManager = userManager;
            _gateway = gateway;
            _logger = logger;
        }

        [HttpGet("{userid}/boards/{id}")]
        public async Task<IActionResult> GetFullBoard(int userid, int id)
        {
            var fullboard = await _gateway.GetBoardWithImages(id);
            if (fullboard != null)
            {
                return View("~/Views/Profiles/Board.cshtml", fullboard);
            }
            _logger.LogInformation($"%%% not found {id} board");
            return NotFound();
        }

        [HttpGet("channels/{id}")]
        public async Task<IActionResult> GetFullChannel(int id)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var fullchannel = await _gateway.GetChannelWithImages(id, user.ProfileId);
            if (fullchannel != null)
            {
                return View("~/Views/Profiles/Channel.cshtml", fullchannel);
            }
            _logger.LogInformation($"%%% not found {id} channel");
            return NotFound();
        }

        [HttpPost("{uid}/boards/{bid}/images")]
        public async Task<IActionResult> LoadImageInBoard(int uid, int bid, string tags, string descr, IFormFile uploadedFile)
        {
            if (uploadedFile != null)
            {
                string filename = Path.GetFileNameWithoutExtension(Path.GetRandomFileName()) + uploadedFile.FileName;
                using (var fileStream = new FileStream(Path.GetFullPath("./wwwroot/images/" + filename), FileMode.Create))
                {
                    await uploadedFile.CopyToAsync(fileStream);
                }
                var path = "~/images/" + filename;
                await _gateway.LoadImageInBoard(path, descr, tags, bid);
            }

            return RedirectToAction("GetFullBoard", new { userid = uid, id = bid });
        }

        [HttpPost("channels/{id}/images")]
        public async Task<IActionResult> LoadImageInChannel(int id, string tags, string descr, IFormFile uploadedFile)
        {
            ///////////////////////////////////////////////
            if (uploadedFile != null)
            {
                string filename = Path.GetFileNameWithoutExtension(Path.GetRandomFileName()) + uploadedFile.FileName;
                using (var fileStream = new FileStream(Path.GetFullPath("./wwwroot/images/" + filename), FileMode.Create))
                {
                    await uploadedFile.CopyToAsync(fileStream);
                }
                var path = "~/images/" + filename;
                await _gateway.LoadImageInChannel(path, descr, tags, id);
            }

            return RedirectToAction("GetFullChannel", new { id = id });
        }

        [HttpGet("{id}/download")]
        public async Task<IActionResult> DownloadImage(int id, string path)
        {
            path = Path.GetFullPath(path.Replace("~", "wwwroot"));
            var memory = new MemoryStream();
            using (var stream = new FileStream(path, FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }
            memory.Position = 0;
            return File(memory, "image/jpeg", Path.GetFileName(path));
        }

        [HttpGet("channels/{id}/images/{imageid}/delete")]
        public async Task<IActionResult> DeleteImageFromChannel(int id, int imageid)
        {
            /////////////////////////////////////////////////
            await _gateway.DeleteImageFromChannel(id, imageid);
            return RedirectToAction("GetFullChannel", new { id = id });
        }

        [HttpGet("{pid}/boards/{id}/images/{imageid}/delete")]
        public async Task<IActionResult> DeleteImageFromBoard(int pid, int id, int imageid)
        {
            await _gateway.DeleteImageFromBoard(id, imageid);
            return RedirectToAction("GetFullBoard", new { userid = pid, id = id });
        }

        [HttpGet("channels/{id}/delete")]
        public async Task<IActionResult> DeleteChannel(int id, [FromQuery] int pid)
        {
            await _gateway.DeleteChannel(id);
            return RedirectToAction("Profile", "Profiles", new { id = pid });
        }

        [HttpGet("{pid}/boards/{id}/delete")]
        public async Task<IActionResult> DeleteBoard(int id, int pid)
        {
            await _gateway.DeleteBoard(id);
            return RedirectToAction("Profile", "Profiles", new { id = pid });
        }
    }
}