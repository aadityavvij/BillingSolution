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
		public IActionResult Details(long? id)
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
		public IActionResult Create()
		{
			return View();
		}
		public IActionResult Customer(long id)
		{
			Customer? customer = _db.Customers.FirstOrDefault(p => p.PhNo == id);
			if (customer == null)
			{
				Customer customer2 = new Customer();
				customer2.PhNo = id;
				return View(customer2);
			}
			return View(customer);
		}
		[HttpPost]
		public IActionResult Customer(Customer customer)
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
	}
}
