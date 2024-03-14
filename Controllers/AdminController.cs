using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MVCG4.Models;

namespace MVCG4.Controllers
{

    public class AdminController : Controller
    {

        private readonly ProjectPRNContext _db;
        public AdminController(ProjectPRNContext db)
        {
            this._db = db;
        }
        public IActionResult Index()
        {
            return View();
        }


        //HÀM NÀY ĐỂ THỐNG KÊ
        public IActionResult CountAccounts()
        {


            //THỐNG KÊ SỐ ĐƠN HÀNG THEO THÁNG ĐỂ VẼ BIỂU ĐỒ ***********************************************************
            var orderData = this._db.Orders
      .Where(o => o.Status == "Da Giao")
      .GroupBy(o => new { Year = o.ODate.Year, Month = o.ODate.Month })
      .Select(group => new
      {
          MonthYear = group.Key,
          Count = group.Count()
      })
      .OrderBy(o => o.MonthYear.Year)
      .ThenBy(o => o.MonthYear.Month)
      .ToList();

            if (orderData.Any())
            {
                // tạo danh sách các nhãn (tháng/năm)
                var orderLabels = orderData.Select(o => $"{o.MonthYear.Month}/{o.MonthYear.Year}").ToList();

                // tạo danh sách số lượng đơn hàng đã bán
                var orderCounts = orderData.Select(o => o.Count).ToList();

                // truyền dữ liệu vào ViewData
                ViewData["OrderLabels"] = string.Join(",", orderLabels.Select(x => "'" + x + "'"));
                ViewData["OrderData"] = string.Join(",", orderCounts);
            }
            else
            {
                // nếu không có dữ liệu, thiết lập danh sách nhãn và số lượng đơn hàng là rỗng
                ViewData["OrderLabels"] = "''";
                ViewData["OrderData"] = "''";
            }





            //THỐNG KÊ SỐ LƯỢNG NHÂN VIÊN, KHÁCH HÀNG , ĐƠN HÀNG , DOANH THU ***********************************************************

            int accountsCount = this._db.Accounts.Count();
            int CountAdmin = this._db.Accounts.Count(o => o.IsAdmin == 1);
            int CountOrder = this._db.Orders.Count(o => o.Status == "Da Giao");
            decimal CountMoney = this._db.Orders.Where(o => o.Status == "Da Giao")
            .Sum(o => o.TotalPrice);

            ViewData["AccountsCount"] = accountsCount;
            ViewData["CountAdmin"] = CountAdmin;
            ViewData["CountOrder"] = CountOrder;
            ViewData["CountMoney"] = CountMoney;

            return View();




        }

    }
}