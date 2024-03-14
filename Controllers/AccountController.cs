using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MVCG4.Models;

namespace MVCG4.Controllers
{

    public class AccountController : Controller
    {
        private readonly ProjectPRNContext _db;

        public AccountController(ProjectPRNContext db)
        {
            this._db = db;
        }

        public IActionResult Index()
        {
            IEnumerable<Account> list = this._db.Accounts.ToList();
            return View(list);
        }

        public IActionResult Create()
        {
            // TODO: Your code here
            return View();
        }
        [HttpPost]
        public IActionResult Create(Account obj)
        {
            if (ModelState.IsValid)
            {
                var existingUser = this._db.Accounts.FirstOrDefault(x => x.Username == obj.Username);
                if (existingUser != null)
                {
                    ModelState.AddModelError("Username", "This username already exists.");
                    return View(obj);
                }

                var existingPhone = this._db.Accounts.FirstOrDefault(x => x.PhoneNumber == obj.PhoneNumber);
                if (existingPhone != null)
                {
                    ModelState.AddModelError("PhoneNumber", "This phone number already exists.");
                    return View(obj);
                }

                var existingEmail = this._db.Accounts.FirstOrDefault(x => x.Email == obj.Email);
                if (existingEmail != null)
                {
                    ModelState.AddModelError("Email", "This email already exists.");
                    return View(obj);
                }


                obj.Password = Md5(obj.Password);
                this._db.Accounts.Add(obj);
                this._db.SaveChanges();
                return RedirectToAction("Index", "Account");
            }
            return View();
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
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var obj = this._db.Accounts.Find(id);
            if (obj == null)
                return NotFound();
            return View(obj);
        }

        [HttpPost]
        public IActionResult Delete(Account obj)
        {
            obj.IsDelete = 1;
            this._db.Accounts.Update(obj);
            this._db.SaveChanges();
            return RedirectToAction("Index");
        }




    }
}