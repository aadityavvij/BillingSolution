﻿using Microsoft.AspNetCore.Mvc;

namespace Billing.Controllers
{
    public class ProductsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
