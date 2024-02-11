using Billing.Data;
using Billing.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Npgsql.EntityFrameworkCore.PostgreSQL.Query.Expressions.Internal;

namespace Billing.Controllers
{
    public class BillsController(ApplicationDbContext db) : Controller
    {
		private readonly ApplicationDbContext _db = db;
		public IActionResult Index()
		{
			List<Invoice> objInvoiceList = _db.Invoices.Include(u => u.Customer).ToList();
			return View(objInvoiceList);
		}
		public IActionResult Create()
		{
			return View();
		}
		public IActionResult Customer(long id)
		{
			Customer? customer = _db.Customers.FirstOrDefault(p => p.PhNo == id);
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
		[HttpPost]
		public IActionResult Customer(Customer customer)
		{
			try
			{
				_db.Customers.Update(customer);
				_db.SaveChanges();

				Invoice invoice = new Invoice();
				invoice.CustomerId = customer.CustomerId;
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
