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
			List<Product> objCategoryList = _db.Products.ToList();
			return View(objCategoryList);
		}
		public IActionResult Create()
		{
			Product product = new Product();
			return View(product);
		}
		[HttpPost]
		public IActionResult Create([Bind("Name,UniqueCode,Price")] Product product)
		{
			if (ModelState.IsValid)
			{
				// Id is 0 or not provided, it's a new entity, add it
				_db.Products.Add(product);

				_db.SaveChanges();
				TempData["SuccessMessage"] = "Added successfully";
				return RedirectToAction("Index");
			}
			else
			{
				return View();
			}
		}
		public IActionResult Edit(int? id)
		{
			if (id == null || id == 0)
			{
				return View();
			}
			Product? product = _db.Products.FirstOrDefault(u => u.Id == id);
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
			if (ModelState.IsValid)
			{
				// If Id is greater than 0, it's an existing entity, update it
				_db.Products.Update(product);
				_db.SaveChanges();
				TempData["SuccessMessage"] = "Edited successfully";
				return RedirectToAction("Index");
			}
			else
			{
				return View();
			}
		}

		[HttpPost]
		public IActionResult Delete(Product product)
		{
			Product? pr = _db.Products.Find(product.Id);
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
