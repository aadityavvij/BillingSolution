using Billing.Data;
using Microsoft.AspNetCore.Mvc;
using Billing.Models;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Hosting;

namespace Billing.Controllers
{
    public class ProductsController(ApplicationDbContext db, IWebHostEnvironment hostEnvironment) : Controller
    {
        private readonly ApplicationDbContext _db = db;
        private readonly IWebHostEnvironment webHostEnvironment = hostEnvironment;

        public IActionResult Index()
        {
			var objCategoryList = _db.Products.ToList().GroupBy(p => p.Name);
			return View(objCategoryList);
		}
		public IActionResult Create()
		{
			Product product = new Product();
			return View(product);
		}
		[HttpPost]
		public IActionResult Create(Product product)
		{
			_db.Products.Add(product);
			_db.SaveChanges();
			TempData["SuccessMessage"] = "Added successfully";
			return RedirectToAction("Index");
			
		}
		public IActionResult Edit(int? id)
		{
			if (id == null || id == 0)
			{
				return View();
			}
			Product? product = _db.Products.FirstOrDefault(u => u.ProductId == id);
			if (product == null)
			{
				TempData["FailureMessage"] = "Not Able to Find The Product";
				return View();
			}
			return View(product);
		}
		[HttpPost]
		public IActionResult Edit(Product product)
		{
			_db.Products.Update(product);
			_db.SaveChanges();
			TempData["SuccessMessage"] = "Edited successfully";
			return RedirectToAction("Index");
		}

		[HttpPost]
		public IActionResult Delete(Product product)
		{
			Product? pr = _db.Products.Find(product.ProductId);
			if(pr == null)
			{
				TempData["FailureMessage"] = "Product Not Found";
				return RedirectToAction("Edit");
			}
			_db.Remove(pr);
			_db.SaveChanges();
			TempData["SuccessMessage"] = "Deleted successfully";
			return RedirectToAction("Index");
		}
	}
}
