﻿using Microsoft.AspNetCore.Mvc;

namespace Lesson4.Areas.Client.Controllers
{
    [Area("Client")]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
