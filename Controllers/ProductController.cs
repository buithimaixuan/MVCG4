using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MVCG4.Models;

namespace MVCG4.Controllers
{

    public class ProductController : Controller
    {
        private readonly ProjectPRNContext _db;
        private readonly IWebHostEnvironment _hostingEnvironment;
        public ProductController(ProjectPRNContext db, IWebHostEnvironment hostingEnvironment)
        {
            _db = db;
            _hostingEnvironment = hostingEnvironment;
        }
        public IActionResult Create()
        {
            var ListCat = _db.Categories.ToList();
            Product pro = null;
            ShowCatPro show = new ShowCatPro();
            show.cat = ListCat;

            return View(show);
        }
        [HttpPost]
        public IActionResult Create(ShowCatPro obj)
        {


            string fileName1 = null;
            try
            {
                var part = Request.Form.Files.GetFile("proPic1");
                var realPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images");

                fileName1 = Path.GetFileName(part.FileName);
                if (string.IsNullOrEmpty(fileName1))
                {
                    fileName1 = "no_image.png";
                }
                if (!Directory.Exists(realPath))
                {
                    // If directory does not exist, create it
                    Directory.CreateDirectory(realPath);
                }
                var filePath = Path.Combine(realPath, fileName1);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    part.CopyToAsync(fileStream);
                }
            }
            catch (Exception e)
            {
            }
            obj.pro.ProImage = "images/" + fileName1;

            _db.Products.Add(obj.pro);
            _db.SaveChanges();
            return RedirectToAction("Index", "Admin");


        }



    }

}
