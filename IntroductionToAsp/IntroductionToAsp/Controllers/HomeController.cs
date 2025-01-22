using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using IntroductionToAsp.Entities;
using IntroductionToAsp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace IntroductionToAsp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private List<Drink> drinks;
        private List<FastFood> fastFood;
        private List<HotMeal> hotMeal;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
            drinks = new List<Drink>
            {
                new Drink(){ Id = 1, Name = "Coca Cola", Volume = 1.5f, Price = 1.87m },
                new Drink(){ Id = 2, Name = "Coca Cola", Volume = 2f, Price = 2.25m },
                new Drink(){ Id = 3, Name = "Fanta", Volume = 1f, Price = 1.37m },
                new Drink(){ Id = 4, Name = "Sprite", Volume = 1.5f, Price = 1.87m },
            };

            fastFood = new List<FastFood>
            {
                new FastFood() { Id = 1, Name = "Hamburger", Calories = 335, PortionSize = "Medium", Price = 2.75m },
                new FastFood() { Id = 2, Name = "Cheeseburger", Calories = 355, PortionSize = "Small", Price = 3.5m },
                new FastFood() { Id = 3, Name = "Big Mac Menu", Calories = 500, PortionSize = "Large", Price = 8.99m },
            };

            hotMeal = new List<HotMeal> {
                new HotMeal() { Id = 1, Name = "Bozbash", PortionSize = "1 person", Price = 7.6m },
                new HotMeal() { Id = 2, Name = "Gurcu Xangali", PortionSize = "1 piece", Price = 0.8m },
                new HotMeal() { Id = 3, Name = "Leaf Xangal", PortionSize = "1 person", Price = 6m },
                new HotMeal() { Id = 4, Name = "Dolma", PortionSize = "1 person", Price = 5.8m },
            };
        }

        public IActionResult Index()
        {
            var vm = new MenuViewModel() { Drinks = drinks, FastFoods = fastFood, HotMeals = hotMeal };
            return View(vm);
        }

        public IActionResult Drinks()
        {
            return View(drinks);
        }

        public IActionResult FastFoods()
        {
            return View(fastFood);
        }

        public IActionResult HotMeals()
        {
            return View(hotMeal);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
