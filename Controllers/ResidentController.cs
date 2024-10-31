using Microsoft.AspNetCore.Mvc;

namespace SWD392_PublicService.Controllers
{
    public class ResidentController : Controller
    {
        public IActionResult ListApplication()
        {
            return View();
        }


        public IActionResult SubmitApplication()
        {
            return View();
        }
    }
}
