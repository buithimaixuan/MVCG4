using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MVCG4.Controllers
{
    public class LogoutController : Controller
    {
        // GET
        public IActionResult Logout()
        {
            HttpContext.Session.SetString("Username", "");
            string username = HttpContext.Session.GetString("Username");
            ViewBag.getUsername = username;
            return RedirectToAction("Index", "Home");

        }
    }
}