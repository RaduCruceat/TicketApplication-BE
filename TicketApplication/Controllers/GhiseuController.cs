using Microsoft.AspNetCore.Mvc;

namespace TicketApplication.Controllers
{
    public class GhiseuController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
