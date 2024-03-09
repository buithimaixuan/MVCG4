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
    }
}