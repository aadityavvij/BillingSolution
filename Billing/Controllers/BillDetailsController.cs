﻿using Billing.Data;
using Billing.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Npgsql.EntityFrameworkCore.PostgreSQL.Query.Expressions.Internal;

namespace Billing.Controllers
{
	public class BillDetailsController(ApplicationDbContext db, UserManager<IdentityUser> UserManager) : Controller
	{
		private readonly ApplicationDbContext _db = db;
		private readonly UserManager<IdentityUser> _UserManager = UserManager;
        [Authorize(Roles = "Admin,SalesStaff")]
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
        [Authorize(Roles = "Admin,SalesStaff")]
        public IActionResult Add(long? id)
        {
			var objCategoryList = _db.Products.Where(p => p.Sold == false).ToList().GroupBy(p => p.Name);
			return View(objCategoryList);
		}
        [Authorize(Roles = "Admin,SalesStaff")]
        [HttpPost]
		public IActionResult Add(InvoiceProduct iProduct)
		{
			try
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
			catch (Exception ex)
			{
				TempData["FailureMessage"] = "An error occurred while adding the product to the invoice";
				return RedirectToAction("Add");
			}
		}


	}
}
