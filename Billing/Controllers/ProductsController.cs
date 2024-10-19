using Billing.Data;
using Microsoft.AspNetCore.Mvc;
using Billing.Models;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace Billing.Controllers
{
    public class ProductsController(ApplicationDbContext db, UserManager<IdentityUser> UserManager) : Controller
    {
        private readonly ApplicationDbContext _db = db;
		private readonly UserManager<IdentityUser> _UserManager = UserManager;
		[Authorize]
        public IActionResult Index()
        {
			var objCategoryList = _db.Products.Where(p => p.Sold == false).ToList().GroupBy(p => p.Name);
			return View(objCategoryList);
		}
		[Authorize]
		public IActionResult Create()
		{
			Product product = new Product();
			return View(product);
		}
		[Authorize]
		[HttpPost]
		public IActionResult Create(Product product)
		{
			try
			{
				_db.Products.Add(product);
				_db.SaveChanges();
				TempData["SuccessMessage"] = "Added successfully";
				return RedirectToAction("Index");
			}
			catch (Exception ex)
			{
				TempData["FailureMessage"] = "An error occurred while adding the product";
				return View();
			}
		}
		[Authorize]
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
		[Authorize]
		[HttpPost]
		public IActionResult Edit(Product product)
		{
			try
			{
				_db.Products.Update(product);
				_db.SaveChanges();
				TempData["SuccessMessage"] = "Edited successfully";
				return RedirectToAction("Index");
			}
			catch (Exception ex)
			{
				TempData["FailureMessage"] = "An error occurred while editing the product";
				return View();
			}
		}

		[Authorize]
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
