using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MVCG4.Models;

namespace MVCG4.Controllers
{
    public class HomeController : Controller
    {
        private readonly ProjectPRNContext _db;
        public HomeController(ProjectPRNContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {

            Dictionary<string, IEnumerable<Product>> map = new Dictionary<string, IEnumerable<Product>>();
            IEnumerable<Product> list = null;
            var distinctCategories = _db.Categories.Select(x => x.CatName).Distinct().ToList();
            foreach (string s in distinctCategories)
            {
                // Thực hiện truy vấn sử dụng Entity Framework
                var query = from p in _db.Products
                            join c in _db.Categories on p.CatId equals c.CatId
                            where c.CatName == s && p.IsDelete == 0
                            select p;

                // Lấy ra tối đa 4 sản phẩm
                list = query.Take(4).ToList();

                map.Add(s, list);
                list = null;
            }

            string username = HttpContext.Session.GetString("Username");
            ViewBag.getUsername = username;
            return View(map);
        }
        public IActionResult IndexAll()
        {
            string searchSession = HttpContext.Session.GetString("SearchValue");
            if (searchSession != null && !searchSession.Equals(""))
            {
                IEnumerable<Product> list = this._db.Products.Where(p => p.ProName.Contains(searchSession) && p.IsDelete == 0).ToList();
                HttpContext.Session.SetString("SearchValue", "");
                string username = HttpContext.Session.GetString("Username");
                ViewBag.getUsername = username;
                return View(list);
            }
            else
            {
                var query = from p in _db.Products
                            where p.IsDelete == 0
                            select p;
                IEnumerable<Product> list = query.ToList();
                string username = HttpContext.Session.GetString("Username");
                ViewBag.getUsername = username;
                return View(list);
            }

        }

        public IActionResult SNL()
        {
            Dictionary<string, IEnumerable<Product>> map = getProByTypeCat("Set nguyên liệu");
            string username = HttpContext.Session.GetString("Username");
            ViewBag.getUsername = username;
            return View("Index", map);
        }
        public IActionResult BoKem()
        {
            Dictionary<string, IEnumerable<Product>> map = getProByTypeCat("Kem, Bơ, Sữa, Phô mai");
            string username = HttpContext.Session.GetString("Username");
            ViewBag.getUsername = username;
            return View("Index", map);
        }
        public IActionResult Bot()
        {
            Dictionary<string, IEnumerable<Product>> map = getProByTypeCat("Bột làm bánh");
            string username = HttpContext.Session.GetString("Username");
            ViewBag.getUsername = username;
            return View("Index", map);
        }
        public IActionResult PhuGia()
        {
            Dictionary<string, IEnumerable<Product>> map = getProByTypeCat("Phụ gia");
            string username = HttpContext.Session.GetString("Username");
            ViewBag.getUsername = username;
            return View("Index", map);
        }
        public IActionResult NlSocola()
        {
            Dictionary<string, IEnumerable<Product>> map = getProByTypeCat("Nguyên liệu làm socola");
            string username = HttpContext.Session.GetString("Username");
            ViewBag.getUsername = username;
            return View("Index", map);
        }
        public IActionResult SNLSinhNhat()
        {
            IEnumerable<Product> list = getAllProByTypeCat("Set nguyên liệu bánh sinh nhật");
            string username = HttpContext.Session.GetString("Username");
            ViewBag.getUsername = username;
            return View("IndexAll", list);
        }
        public IActionResult SNLCookie()
        {
            IEnumerable<Product> list = getAllProByTypeCat("Set nguyên liệu bánh cookie");
            string username = HttpContext.Session.GetString("Username");
            ViewBag.getUsername = username;
            return View("IndexAll", list);
        }
        public IActionResult SNLBanhMi()
        {
            IEnumerable<Product> list = getAllProByTypeCat("Set nguyên liệu bánh mì");
            string username = HttpContext.Session.GetString("Username");
            ViewBag.getUsername = username;
            return View("IndexAll", list);
        }
        public IActionResult SNLAnvat()
        {
            IEnumerable<Product> list = getAllProByTypeCat("Set nguyên liệu bánh ăn vặt");
            string username = HttpContext.Session.GetString("Username");
            ViewBag.getUsername = username;
            return View("IndexAll", list);
        }
        public IActionResult SNLSocola()
        {
            IEnumerable<Product> list = getAllProByTypeCat("Set nguyên liệu làm socola");
            string username = HttpContext.Session.GetString("Username");
            ViewBag.getUsername = username;
            return View("IndexAll", list);
        }
        public IActionResult Bo()
        {
            IEnumerable<Product> list = getAllProByTypeCat("Bơ");
            string username = HttpContext.Session.GetString("Username");
            ViewBag.getUsername = username;
            return View("IndexAll", list);
        }
        public IActionResult Kem()
        {
            IEnumerable<Product> list = getAllProByTypeCat("Whipping(Cream)");
            string username = HttpContext.Session.GetString("Username");
            ViewBag.getUsername = username;
            return View("IndexAll", list);
        }
        public IActionResult PhoMai()
        {
            IEnumerable<Product> list = getAllProByTypeCat("Phô mai(cheese)");
            string username = HttpContext.Session.GetString("Username");
            ViewBag.getUsername = username;
            return View("IndexAll", list);
        }
        public IActionResult Sua()
        {
            IEnumerable<Product> list = getAllProByTypeCat("Sữa và sản phẩm làm từ sữa");
            string username = HttpContext.Session.GetString("Username");
            ViewBag.getUsername = username;
            return View("IndexAll", list);
        }
        public IActionResult BotMi()
        {
            IEnumerable<Product> list = getAllProByTypeCat("Bột mì làm bánh");
            string username = HttpContext.Session.GetString("Username");
            ViewBag.getUsername = username;
            return View("IndexAll", list);
        }
        public IActionResult BotMiNC()
        {
            IEnumerable<Product> list = getAllProByTypeCat("Bột mì nguyên cám");
            string username = HttpContext.Session.GetString("Username");
            ViewBag.getUsername = username;
            return View("IndexAll", list);
        }
        public IActionResult BotTron()
        {
            IEnumerable<Product> list = getAllProByTypeCat("Bột trộn sẵn");
            string username = HttpContext.Session.GetString("Username");
            ViewBag.getUsername = username;
            return View("IndexAll", list);
        }
        public IActionResult BotKhac()
        {
            IEnumerable<Product> list = getAllProByTypeCat("Bột làm bánh khác");
            string username = HttpContext.Session.GetString("Username");
            ViewBag.getUsername = username;
            return View("IndexAll", list);
        }
        public IActionResult Men()
        {
            IEnumerable<Product> list = getAllProByTypeCat("Men nở và phụ gia nhỏ");
            string username = HttpContext.Session.GetString("Username");
            ViewBag.getUsername = username;
            return View("IndexAll", list);
        }
        public IActionResult Duong()
        {
            IEnumerable<Product> list = getAllProByTypeCat("Đường các loại");
            string username = HttpContext.Session.GetString("Username");
            ViewBag.getUsername = username;
            return View("IndexAll", list);
        }
        public IActionResult Huong()
        {
            IEnumerable<Product> list = getAllProByTypeCat("Hương liệu và tinh dầu");
            string username = HttpContext.Session.GetString("Username");
            ViewBag.getUsername = username;
            return View("IndexAll", list);
        }
        public IActionResult Mau()
        {
            IEnumerable<Product> list = getAllProByTypeCat("Màu thực phẩm");
            string username = HttpContext.Session.GetString("Username");
            ViewBag.getUsername = username;
            return View("IndexAll", list);
        }
        public IActionResult SocolaHat()
        {
            IEnumerable<Product> list = getAllProByTypeCat("Socola hạt");
            string username = HttpContext.Session.GetString("Username");
            ViewBag.getUsername = username;
            return View("IndexAll", list);
        }
        public IActionResult SocolaThanh()
        {
            IEnumerable<Product> list = getAllProByTypeCat("Socola thanh");
            string username = HttpContext.Session.GetString("Username");
            ViewBag.getUsername = username;
            return View("IndexAll", list);
        }
        public IActionResult SocolaTrangTri()
        {
            IEnumerable<Product> list = getAllProByTypeCat("Nguyên liệu trang trí socola");
            string username = HttpContext.Session.GetString("Username");
            ViewBag.getUsername = username;
            return View("IndexAll", list);
        }


        public Dictionary<string, IEnumerable<Product>> getProByTypeCat(string type)
        {
            Dictionary<string, IEnumerable<Product>> map = new Dictionary<string, IEnumerable<Product>>();
            IEnumerable<Product> list = null;
            var typeCategories = _db.Categories.Where(c => c.CatName == type).Select(c => c.TypeCategories)
    .ToList();
            foreach (string s in typeCategories)
            {
                // Thực hiện truy vấn sử dụng Entity Framework
                var query = from p in _db.Products
                            join c in _db.Categories on p.CatId equals c.CatId
                            where c.TypeCategories == s && p.IsDelete == 0
                            select p;

                // Lấy ra tối đa 4 sản phẩm
                list = query.ToList();

                map.Add(s, list);
                list = null;
            }
            return map;
        }
        public IEnumerable<Product> getAllProByTypeCat(string type)
        {
            var query = from p in _db.Products
                        join c in _db.Categories on p.CatId equals c.CatId
                        where c.TypeCategories == type && p.IsDelete == 0
                        select p;
            IEnumerable<Product> list = query.ToList();
            return list;
        }

        [HttpPost]
        public IActionResult SearchProduct()
        {
            string getSearch = Request.Form["value-search"];
            HttpContext.Session.SetString("SearchValue", getSearch);
            return RedirectToAction("IndexAll", "Home");
        }

    }
}
