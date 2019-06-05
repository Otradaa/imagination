using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ChannelService.Data.Models;
using GatewayService.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using UserService.Data.Models;

namespace GatewayService.Controllers
{
   // [Route("[controller]")]
    public class ImagesController : Controller
    {
        private readonly IGatewayService _gateway;
        private readonly ILogger _logger;

        public ImagesController(IGatewayService gateway, ILogger<ProfilesController> logger)
        {
            _gateway = gateway;
            _logger = logger;
        }

        [HttpGet("{userid}/boards/{id}")]
        public async Task<IActionResult> GetFullBoard(int userid, int id)
        {
            var fullboard = await _gateway.GetBoardWithImages(id);
            if (fullboard != null)
            {
                return View("~/Views/Profiles/Board.cshtml",fullboard);
            }
            _logger.LogInformation($"%%% not found {id} board");
            return NotFound();
        }

        [HttpGet("channels/{id}")]
        public async Task<IActionResult> GetFullChannel(int id)
        {
            var fullchannel = await _gateway.GetChannelWithImages(id);
            if (fullchannel != null)
            {
                return View("~/Views/Profiles/Channel.cshtml", fullchannel);
            }
            _logger.LogInformation($"%%% not found {id} channel");
            return NotFound();
        }

        [HttpPost("{uid}/boards/{bid}/images")]
        public async Task<IActionResult> LoadImageInBoard(int uid, int bid, string tags, string descr, IFormFile uploadedFile )
        {
            if (uploadedFile != null)
            {
                string filename = Path.GetFileNameWithoutExtension(Path.GetRandomFileName()) + uploadedFile.FileName;
                using (var fileStream = new FileStream(Path.GetFullPath("./wwwroot/images/" + filename), FileMode.Create))
                {
                    await uploadedFile.CopyToAsync(fileStream);
                }
                ///////////////////////////////////////////////
                var path = "~/images/" + filename;
                await _gateway.LoadImageInBoard(path, descr, tags, bid);
            }

            return RedirectToAction("GetFullBoard", new { userid = uid, id = bid });
        }

        [HttpPost("channels/{id}/images")]
        public async Task<IActionResult> LoadImageInChannel(int id, string tags, string descr, IFormFile uploadedFile)
        {
            if (uploadedFile != null)
            {
                string filename = Path.GetFileNameWithoutExtension(Path.GetRandomFileName()) + uploadedFile.FileName;
                using (var fileStream = new FileStream(Path.GetFullPath("./wwwroot/images/" + filename), FileMode.Create))
                {
                    await uploadedFile.CopyToAsync(fileStream);
                }
                ///////////////////////////////////////////////
                var path = "~/images/" + filename;
                await _gateway.LoadImageInChannel(path, descr, tags, id);
            }

            return RedirectToAction("GetFullChannel", new { id = id });
        }

        // GET: Images
        public ActionResult Index()
        {
            return View();
        }

        // GET: Images/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Images/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Images/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Images/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Images/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Images/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Images/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}