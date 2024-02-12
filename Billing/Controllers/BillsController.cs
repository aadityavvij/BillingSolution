using Billing.Data;
using Billing.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Npgsql.EntityFrameworkCore.PostgreSQL.Query.Expressions.Internal;

namespace Billing.Controllers
{
    public class BillsController(ApplicationDbContext db, UserManager<IdentityUser> UserManager) : Controller
    {
		private readonly ApplicationDbContext _db = db;
		private readonly UserManager<IdentityUser> _UserManager = UserManager;
		[Authorize]
		public IActionResult Index()
		{
			List<Invoice> objInvoiceList = _db.Invoices.Where(o => o.StoreId == _UserManager.GetUserId(User)).Include(u => u.Customer).ToList();
			return View(objInvoiceList);
		}
		[Authorize]
		public IActionResult Create()
		{
			return View();
		}
		[Authorize]
		public IActionResult Customer(long id)
		{
			Customer? customer = _db.Customers.Where(o => o.StoreId == _UserManager.GetUserId(User)).FirstOrDefault(p => p.PhNo == id);
			if (customer == null)
			{
				Customer customer2 = new Customer
				{
					PhNo = id
				};
				return View(customer2);
			}
			return View(customer);
		}
		[Authorize]
		[HttpPost]
		public IActionResult Customer(Customer customer)
		{
			try
			{
				customer.StoreId = _UserManager.GetUserId(User);
				_db.Customers.Update(customer);
				_db.SaveChanges();

				Invoice invoice = new Invoice();
				invoice.CustomerId = customer.CustomerId;
				invoice.StoreId = _UserManager.GetUserId(User);
                _db.Invoices.Add(invoice);
				_db.SaveChanges();

				long id = invoice.InvoiceId;
				return RedirectToAction("Index", "BillDetails", new { id = id });
			}
			catch (Exception ex)
			{
				TempData["FailureMessage"] = "An error occurred while processing the customer";
				return View();
			}
		}

	}
}
