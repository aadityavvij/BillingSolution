using Billing.Data;
using Billing.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Npgsql.EntityFrameworkCore.PostgreSQL.Query.Expressions.Internal;

namespace Billing.Controllers
{
	public class BillDetailsController(ApplicationDbContext db) : Controller
	{
		private readonly ApplicationDbContext _db = db;
		public IActionResult Index(long? id)
		{
			if (id == null || id == 0)
			{
				TempData["FailureMessage"] = "Invoice Not Found";
				return View();
			}
			// Retrieve all InvoiceProducts where InvoiceId matches the provided invoiceid
			List<InvoiceProduct>? objInvoiceProductList = _db.InvoiceProducts
				.Where(i => i.InvoiceId == id)
				.Include(p => p.Product)
				.ToList();

			if (objInvoiceProductList == null)
			{
				TempData["FailureMessage"] = "This Invoice has no Products";
				return View();
			}
			return View(objInvoiceProductList);
		}
		public IActionResult Add(long? id)
		{
			var objCategoryList = _db.Products.Where(p => p.Sold == false).ToList().GroupBy(p => p.Name);
			return View(objCategoryList);
		}

		[HttpPost]
		public IActionResult Add(InvoiceProduct iProduct)
		{
			
			var newInvoiceProduct = new InvoiceProduct
			{
				InvoiceId = iProduct.InvoiceId,
				ProductId = iProduct.ProductId
			};
			_db.InvoiceProducts.Add(newInvoiceProduct);
			Product? product = _db.Products.FirstOrDefault(p => p.ProductId == iProduct.ProductId);
			if (product != null)
			{
				product.Sold = true;
				_db.Products.Update(product);
			}

			_db.SaveChanges();
			TempData["SuccessMessage"] = "Added successfully";
			return RedirectToAction("Add");

		}

	}
}
