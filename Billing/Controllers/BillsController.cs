using Microsoft.AspNetCore.Mvc;

namespace Billing.Controllers
{
    public class BillsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Create()
        {
            return View();
        }
    }
}
