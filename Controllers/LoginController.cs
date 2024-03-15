using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVCG4.Models;

namespace MVCG4.Controllers
{
    public class LoginController : Controller
    {
        private readonly ProjectPRNContext _db;
        public LoginController(ProjectPRNContext db)
        {
            _db = db;
        }
        public IActionResult Login()
        {
            // TODO: Your code here
            return View();
        }
        // [HttpPost]
        // public IActionResult Login1(Login login)
        // {
        //     //     // IEnumerable<Account> list = this._db.Accounts.ToList();
        //     //     // if(list!=null){
        //     //     //     return RedirectToAction("Index", "Home");
        //     //     // }
        //     string user = login.Username;
        //     // string pass = Md5(login.Password);
        //     //     // Account account = null;
        //     // var account = this._db.Accounts.Any(acc => acc.Username == user && acc.Password == pass && acc.IsDelete == 0);


        //     if (account != null)
        //     {
        //         IEnumerable<Account> list = null;

        //         var query = from p in _db.Accounts
        //                     where p.Username == user && p.IsDelete == 0 && p.Password == pass
        //                     select p;
        //         list = query.ToList();
        //         foreach (Account a in list)
        //         {
        //             if (a.IsAdmin == 0)
        //             {
        //                 HttpContext.Session.SetString("account", login.Username);

        //                 // return RedirectToAction("Index", "Home");

        //                 // Get session value
        //                 var value = HttpContext.Session.GetString("account");
        //                 if (value != null)
        //                 {
        //                     return RedirectToAction("Index", "Home");
        //                 }
        //             }
        //             else
        //             {
        //                 HttpContext.Session.SetString("account", login.Username);

        //                 // return RedirectToAction("Index", "Home");

        //                 // Get session value
        //                 var value = HttpContext.Session.GetString("account");
        //                 if (value != null)
        //                 {
        //                     return RedirectToAction("Index", "Admin");
        //                 }
        //             }
        //         }



        //     }
        //     // IEnumerable<Account> list = this._db.Accounts.Where(s => s.Email.Equals(user) && s.Password.Equals(pass)).ToList();
        //     // if (list != null)
        //     // {
        //     //     return RedirectToAction("Index", "Home");
        //     // }


        //     return View(login);

        // }

        [HttpPost]
        public async Task<IActionResult> Login(Login login)
        {
            if (!ModelState.IsValid) return View();

            var user = await _db.Accounts.SingleOrDefaultAsync(user =>
                                    user.Username == login.Username &&
                                    user.Password == Md5(login.Password) &&
                                    user.IsDelete == 0);

            if (user == null) return View();
            // Thiết lập session
            HttpContext.Session.SetString("Username", login.Username);
            HttpContext.Session.SetInt32("AccountID", user.AccId);
             
            if (user.IsAdmin == 1)
            {
                ViewBag.getUsername = login.Username;
                return RedirectToAction("Index", "Admin");
            }

            ViewBag.getUsername = login.Username;
            return RedirectToAction("Index", "Home");
        }


        public static string Md5(string message)
        {
            using (MD5 md5 = MD5.Create())
            {
                byte[] input = Encoding.ASCII.GetBytes(message);
                byte[] hash = md5.ComputeHash(input);

                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hash.Length; i++)
                {
                    sb.Append(hash[i].ToString("X2"));
                }
                return sb.ToString();
            }
        }
    }
}