using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MVCG4.Models;

namespace MVCG4.Controllers
{

    public class RegisterController : Controller
    {
        private readonly ProjectPRNContext _db;
        public RegisterController(ProjectPRNContext db)
        {
            this._db = db;
        }

        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Register(Account acc)
        {
            if (ModelState.IsValid)
            {
                var context = new ProjectPRNContext();
                if (context.Accounts.Any(a => a.Username == acc.Username))
                {
                    ModelState.AddModelError("Username", "This Username already exist!");
                    return View(acc);
                }
                else if (context.Accounts.Any(a => a.Email == acc.Email))
                {
                    ModelState.AddModelError("Email", "This Email already exist!");
                    return View(acc);
                }
                else if (context.Accounts.Any(a => a.PhoneNumber == acc.PhoneNumber))
                {
                    ModelState.AddModelError("PhoneNumber", "This PhoneNumber already exist!");
                    return View(acc);
                }
                else
                {
                    string hashPass = Md5(acc.Password);
                    acc.Password = hashPass;
                    context.Accounts.Add(acc);
                    context.SaveChanges();
                    return RedirectToAction("Login", "Login");
                }
            }
            else
            {
                return View();
            }

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