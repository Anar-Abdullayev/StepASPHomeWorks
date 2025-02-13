using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;

namespace ECommerce.UI.ViewComponents
{
    public class LoginViewComponent() : ViewComponent
    {
        public ViewViewComponentResult Invoke()
        {
            string user = User.Identity.Name;
            return View("Default", user);
        }
    }
}
