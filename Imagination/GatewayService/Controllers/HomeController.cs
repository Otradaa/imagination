using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using GatewayService.Models;

namespace GatewayService.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet("/")]
        public IActionResult Index()
        {
            return View("~/Views/Home/Index.cshtml");
        }

        [HttpGet("about")]
        public IActionResult About()
        {
            return View("~/Views/Home/About.cshtml");
        }

        [HttpGet("contact")]
        public IActionResult Contact()
        {
            return View("~/Views/Home/Contact.cshtml");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
