using Billing.Data;
using Microsoft.AspNetCore.Mvc;
using Billing.Models;

namespace Billing.Controllers
{
    public class ProductsController(ApplicationDbContext db, IWebHostEnvironment hostEnvironment) : Controller
    {
        private readonly ApplicationDbContext _db = db;
        private readonly IWebHostEnvironment webHostEnvironment = hostEnvironment;

        public IActionResult Index()
        {
			List<Product> objCategoryList = _db.Products.ToList();
			return View(objCategoryList);
		}
    }
}
