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
        public IActionResult Index()
        {
            IEnumerable<Product> list = this._db.Products.ToList();
            return View(list);
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
                this._db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();


        }


        [HttpGet]
        public IActionResult Edit(int id)
        {
            var obj = this._db.Products.Find(id);
            if (obj == null)
                return NotFound();
            return View(obj);
        }


        [HttpPost]
        public IActionResult Edit(Product obj)
        {
            if (ModelState.IsValid)
            {
                this._db.Products.Update(obj);
                this._db.SaveChanges();
            }
            return RedirectToAction("Index");

        }


        [HttpGet]
        public IActionResult Delete(int id)
        {

            var obj = this._db.Products.Find(id);
            if (obj == null)
                return NotFound();
            return View(obj);
        }

        [HttpPost]
        public IActionResult Delete(Product obj)
        {
            obj.IsDelete = 1;
            this._db.Products.Update(obj);
            this._db.SaveChanges();
            return RedirectToAction("Index");
        }


    }

}
