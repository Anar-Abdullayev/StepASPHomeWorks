using Microsoft.AspNetCore.Mvc;

namespace ECommerce.UI.Controllers
{
    public class SessionController : Controller
    {
        public IActionResult Index()
        {
            HttpContext.Session.SetInt32("age", 26);
            HttpContext.Session.SetString("name", "Elvin");

            return Ok();
        }

        public string Get()
        {
            var name=HttpContext.Session.GetString("name");
            var age=HttpContext.Session.GetInt32("age");
            return $"Name : {name}   Age : {age}";
        }
    }
}
