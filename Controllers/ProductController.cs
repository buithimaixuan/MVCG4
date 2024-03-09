using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MVCG4.Models;

namespace MVCG4.Controllers
{

    public class ProductController : Controller
    {
        private readonly ProjectPRNContext _db;
        public ProductController(ProjectPRNContext db)
        {
            this._db = db;
        }
        public IActionResult Create()
        {
            // TODO: Your code here
            return View();
        }
        [HttpPost]
        public IActionResult Create(Product obj)
        {
            if (ModelState.IsValid)
            {
                this._db.Products.Add(obj);
                return RedirectToAction("Index", "Admin");
            }
            return View();


        }

    }

}
