using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MVCG4.Models;
using Newtonsoft.Json;

namespace MVCG4.Controllers
{
    public class CartController : Controller
    {
        private readonly ProjectPRNContext _db;
        public CartController(ProjectPRNContext db)
        {
            this._db = db;
        }

        public IActionResult ViewListCart()
        {
            int? getAccID = HttpContext.Session.GetInt32("AccountID");
            MultiModels mulModel = new MultiModels();

            // Lấy thông tin account
            Account acc = this._db.Accounts.Find(getAccID);

            // Kiểm tra có acc ko
            if (acc != null)
            {
                // Set multi model
                mulModel.getAccount = acc;
                mulModel.cartList = this._db.Carts.Where(ca => ca.AccId == getAccID).ToList();
                mulModel.productList = this._db.Products.ToList();

                string username = HttpContext.Session.GetString("Username");
                ViewBag.getUsername = username;
                return View(mulModel);
            }

            // Đéo có trả về view
            return View();
        }

        // Thêm vào cart
        [HttpPost]
        public IActionResult AddToCart()
        {
            int getProId = int.Parse(Request.Form["product_ID"]);
            int getAccId = Convert.ToInt32(HttpContext.Session.GetInt32("AccountID"));
            MultiModels mulModel = new MultiModels();

            // Lấy thông tin account
            Account acc = this._db.Accounts.Find(getAccId);
            if (acc != null)
            {
                Product productToCart = this._db.Products.Find(getProId);
                Cart checkCart = this._db.Carts.SingleOrDefault(ca => ca.AccId == getAccId && ca.ProId == getProId);

                // Kiểm tra SP trong giỏ hàng
                if (checkCart == null)
                {
                    decimal currentPrice = productToCart.ProPrice;
                    if (productToCart.Discount != 0)
                    {
                        currentPrice = productToCart.Discount;
                    }
                    
                    // Tạo đối tương cart mới để thêm vào

                    Cart newCart = new Cart();
                    newCart.AccId = getAccId;
                    newCart.ProId = getProId;
                    newCart.ProQuantity = 1;
                    newCart.CartPrice = currentPrice;

                    // Tạo mới cart
                    this._db.Carts.Add(newCart);
                    this._db.SaveChanges();

                    return RedirectToAction("Index", "Home");
                } else {
                    int addQuantity = checkCart.ProQuantity + 1;

                    decimal currentPrice = productToCart.ProPrice;
                    if (productToCart.Discount != 0)
                    {
                        currentPrice = productToCart.Discount;
                    }

                    // Thêm sp đã có vào cart
                    Cart updateCart = this._db.Carts.SingleOrDefault(ca => ca.AccId == getAccId && ca.ProId == getProId);
                    if (updateCart != null)
                    {
                        updateCart.ProQuantity = addQuantity;
                        updateCart.CartPrice = currentPrice * addQuantity;
                        this._db.SaveChanges();

                        return RedirectToAction("Index", "Home");
                    }

                }
            }
            return RedirectToAction("Index", "Home");
        }

        // Tăng cart
        public IActionResult IncreaseCart(int caid)
        {
            int? getAccId = HttpContext.Session.GetInt32("AccountID");
            MultiModels mulModel = new MultiModels();

            // Dò acc
            Account acc = this._db.Accounts.Find(getAccId);
             // Check if acc is found
            if (acc != null)
            {
                // Cập nhật +
                Cart getCart = this._db.Carts.SingleOrDefault(ca => ca.AccId == getAccId && ca.ProId == caid);
                if (getCart != null)
                {
                    getCart.ProQuantity += 1;
                    this._db.SaveChanges();
                }
                return RedirectToAction("ViewListCart","Cart");
            }
            return RedirectToAction("ViewListCart","Cart");
        }
        
        //Giảm cart
        public IActionResult DescreaseCart(int caid)
        {
            int? getAccId = HttpContext.Session.GetInt32("AccountID");
            MultiModels mulModel = new MultiModels();

            // Dò acc
            Account acc = this._db.Accounts.Find(getAccId);
             // Check if acc is found
            if (acc != null)
            {
                // Cập nhật -
                Cart getCart = this._db.Carts.SingleOrDefault(ca => ca.AccId == getAccId && ca.ProId == caid);
                if (getCart != null)
                {
                    getCart.ProQuantity -= 1;
                    this._db.SaveChanges();
                }
                return RedirectToAction("ViewListCart","Cart");
            }
            return RedirectToAction("ViewListCart","Cart");
        }

        // Xóa cart
        public IActionResult DeleteCart(int caid)
        {
            int? getAccId = HttpContext.Session.GetInt32("AccountID");
            MultiModels mulModel = new MultiModels();

            // Dò acc
            Account acc = this._db.Accounts.Find(getAccId);
             // Check if customer is found
            if (acc != null)
            {
                Cart getCart = this._db.Carts.SingleOrDefault(ca => ca.AccId == getAccId && ca.ProId == caid);
                if (getCart != null)
                {
                    this._db.Remove(getCart);
                    this._db.SaveChanges();
                }
                return RedirectToAction("ViewListCart","Cart");
            }
            return RedirectToAction("ViewListCart","Cart");
        }

        // Mua trong cart
        [HttpPost]
        public IActionResult BuyInCart()
        {
            // Lấy chuỗi checkbox
            string[] checkBoxValues = Request.Form["checkBoxID"];
            // Lưu vè dạng chuỗi json
            string getIDCheckBox = JsonConvert.SerializeObject(checkBoxValues);
            HttpContext.Session.SetString("checkBoxValues", getIDCheckBox);
            return RedirectToAction("Checkout","Order");
        }
    }
}
