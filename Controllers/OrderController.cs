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
    public class OrderController : Controller
    {
        private readonly ProjectPRNContext _db;
        public OrderController(ProjectPRNContext db)
        {
            this._db = db;
        }

        // Trang thanh toán
        public IActionResult Checkout()
        {
            int? getAccID = HttpContext.Session.GetInt32("AccountID");
            MultiModels mulModel = new MultiModels();

            // Lấy thông tin account
            Account acc = this._db.Accounts.Find(getAccID);

            Order order = null;

            // Check if customer is found
            if (acc != null)
            {
                // Populate the OrderModel
                mulModel.getAccount = acc;
                // thk này là set null đập nhập giá trị
                mulModel.getOrder = order;
                // in ra list khi select
                mulModel.cartList = this._db.Carts.ToList();
                mulModel.productList = this._db.Products.ToList();
                string getIDCheckBox = HttpContext.Session.GetString("checkBoxValues");
                // trả về kiểu chuỗi
                string[] getCheckBoxValues = JsonConvert.DeserializeObject<string[]>(getIDCheckBox);

                // Set viewbag
                ViewBag.GetCheckArray = getCheckBoxValues;
                ViewBag.GetAccID = getAccID;
                string username = HttpContext.Session.GetString("Username");
                ViewBag.getUsername = username;
                // DateTime currentDate = DateTime.Now.ToString("yyyy-MM-dd");

                return View(mulModel);
            }

            // If customer is not found, return an empty view
            return View();
        }

        // Tạo order
        [HttpPost]
        public IActionResult PayProduct(MultiModels mulModel)
        {
            int getAccID = Convert.ToInt32(HttpContext.Session.GetInt32("AccountID"));

            // Kiểm tra input
            if (ModelState.IsValid)
            {
                // Tạo order object new để adđ
                mulModel.getOrder.OId = 0;
                mulModel.getOrder.CusId = getAccID;
                mulModel.getOrder.Status = "Chờ xác nhận";
                mulModel.getOrder.ODate = DateTime.Now;
                mulModel.getOrder.IsDelete = 0;

                // Tạo đơn hàng
                this._db.Orders.Add(mulModel.getOrder);
                this._db.SaveChanges();

                // Lấy Id từng SP
                string getIDCheckBox = HttpContext.Session.GetString("checkBoxValues");
                string[] getCheckBoxValues = JsonConvert.DeserializeObject<string[]>(getIDCheckBox);

                // Lấy tk mới mua gần nhất
                Order getOrderPay = this._db.Orders.OrderByDescending(o => o.OId).FirstOrDefault(o => o.CusId == getAccID);
                for (int i = 0; i < getCheckBoxValues.Length; i++)
                {
                    int proID = int.Parse(getCheckBoxValues[i]);
                    Cart getCartPay = this._db.Carts.SingleOrDefault(ca => ca.AccId == getAccID && ca.ProId == proID);

                    // OrderDetail orderDetail = new OrderDetail(getOrderPay.OId, proID, getCartPay.ProQuantity);
                    // Tạo đối tượng orderdetail
                    OrderDetail orderDetail = new OrderDetail();
                    orderDetail.OId = getOrderPay.OId;
                    orderDetail.ProId = proID;
                    orderDetail.Quantity = getCartPay.ProQuantity;

                    // Thêm order detail
                    this._db.OrderDetails.Add(orderDetail);
                    this._db.SaveChanges();

                    // Cập nhật sl hàng khi mua
                    Product updatePro = this._db.Products.Find(proID);
                    if (updatePro != null)
                    {
                        updatePro.ProQuantity -= getCartPay.ProQuantity;
                        this._db.SaveChanges();
                    }

                    // Xóa SP mua trong cart
                    Cart deleteCart = this._db.Carts.SingleOrDefault(ca => ca.AccId == getAccID && ca.ProId == proID);
                    if (deleteCart != null)
                    {
                        this._db.Remove(deleteCart);
                        this._db.SaveChanges();
                    }
                }

                return RedirectToAction("PaySuccess", "Order");
            }
            // return View();
            return View();
        }

        // Trang mua success
        public IActionResult PaySuccess()
        {
            int? getAccID = HttpContext.Session.GetInt32("AccountID");
            MultiModels mulModel = new MultiModels();

            // Lấy thông tin account
            Account acc = this._db.Accounts.Find(getAccID);

            if (acc != null)
            {
                Order ord = this._db.Orders.OrderByDescending(o => o.OId).FirstOrDefault(o => o.CusId == getAccID);
                if (ord != null)
                {
                    mulModel.getOrder = ord;
                    mulModel.getAccount = acc;

                    string username = HttpContext.Session.GetString("Username");
                    ViewBag.getUsername = username;
                    return View(mulModel);
                }
            }
            return View();
        }

        // Danh sách order
        public IActionResult ViewListOrder()
        {
            int? getAccID = HttpContext.Session.GetInt32("AccountID");
            MultiModels mulModel = new MultiModels();

            // Lấy thông tin account
            Account acc = this._db.Accounts.Find(getAccID);

            // Check if customer is found
            if (acc != null)
            {
                // Populate the OrderModel
                mulModel.getAccount = acc;
                mulModel.orderList = this._db.Orders.Where(o => o.CusId == getAccID).ToList();

                string username = HttpContext.Session.GetString("Username");
                ViewBag.getUsername = username;
                return View(mulModel);
            }

            // If customer is not found, return an empty view
            return View(mulModel);
        }

        // Lọc trạng thái order
        public IActionResult ViewListOrderFilter(string status)
        {
            int? getAccID = HttpContext.Session.GetInt32("AccountID");
            MultiModels mulModel = new MultiModels();

            // Lấy thông tin account
            Account acc = this._db.Accounts.Find(getAccID);

            // Check if customer is found
            if (acc != null)
            {
                mulModel.getAccount = acc;
                mulModel.orderList = this._db.Orders.Where(o => o.CusId == getAccID && o.Status.Equals(status)).ToList();

                string username = HttpContext.Session.GetString("Username");
                ViewBag.getUsername = username;
                return View(mulModel);
            }
            return View(mulModel);
        }

        // Order detail của khách
        public IActionResult ViewOrderDetailByCusID(int oid)
        {
            int? getAccID = HttpContext.Session.GetInt32("AccountID");
            MultiModels mulModel = new MultiModels();

            // Lấy thông tin account
            Account acc = this._db.Accounts.Find(getAccID);
            //Lấy thông tin order
            Order ord = this._db.Orders.SingleOrDefault(o => o.OId == oid);
            // Check if customer is found
            if (acc != null)
            {
                mulModel.getAccount = acc;
                mulModel.getOrder = ord;
                // IEnumerable<OrderDetail> odtLenghtList = this._db.OrderDetails.Where(od => od.OId == oid).ToList();
                mulModel.orderDtList = this._db.OrderDetails.Where(od => od.OId == oid).ToList();
                mulModel.productList = this._db.Products.ToList();
                mulModel.categoryList = this._db.Categories.ToList();

                string username = HttpContext.Session.GetString("Username");
                ViewBag.getUsername = username;
                return View(mulModel);
            }
            return View(mulModel);
        }

        //Bom hàng(mắc dại thật)
        public IActionResult CancelOrder(int oid)
        {
            int? getAccID = HttpContext.Session.GetInt32("AccountID");
            MultiModels mulModel = new MultiModels();

            // Lấy thông tin account
            Account acc = this._db.Accounts.Find(getAccID);
            // Check if customer is found
            if (acc != null)
            {
                Order ord = this._db.Orders.Find(oid);
                if (ord != null)
                {
                    ord.Status = "Đã hủy";
                    this._db.SaveChanges();
                    return RedirectToAction("ViewListOrder", "Order");
                }
            }
            return View();
        }
    }
}