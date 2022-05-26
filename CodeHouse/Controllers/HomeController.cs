using CodeHouse.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace CodeHouse.Controllers
{
    public class HomeController : Controller
    {

        public HomeController()
        {
            
        }

        public IActionResult Page()
        {
            List<SelectListItem> DropMenu = new List<SelectListItem>()
            {
                new SelectListItem(){Text = "Oner", Value = "Ontigon"},
                 new SelectListItem(){Text = "Twover", Value = "Woweruskes"},
                  new SelectListItem(){Text = "Three", Value = "Tretogon"}
            };

            ViewData.Add("Drops",DropMenu);
            return View();
        }

        public IActionResult Index()
        {
            return View();
        }

        [Authorize]
        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult One()
        {
            if (User.Identity.IsAuthenticated)
            {
                return Content(User.Identity.Name);
            }
            return Content("не аутентифицирован");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
