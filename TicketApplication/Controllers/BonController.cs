using Microsoft.AspNetCore.Mvc;

namespace TicketApplication.Controllers
{
    public class BonController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
