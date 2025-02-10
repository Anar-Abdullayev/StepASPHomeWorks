using ECommerce.Entities.Concrete.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ECommerce.UI.Controllers
{
    [Authorize(Roles = $"{nameof(Roles.Admin)}")]
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        // Custom method to handle unauthorized access and add custom data
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);

            // If the user is not authorized, redirect to login with custom data
            if (!User.Identity.IsAuthenticated)
            {
                string returnUrl = context.HttpContext.Request.Path;
                Roles role = Roles.Admin; // The custom data you want to pass

                // Redirect to Login action with ReturnUrl and custom data
                context.Result = RedirectToAction("Login", "Account", new { returnUrl = returnUrl, role = role });
            }
        }
    }
}
