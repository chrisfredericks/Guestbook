using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using guestbook.Models;

namespace guestbook.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index() {
            // first visit to app - construct model and pass to view
            GuestbookManager guestbookManager = new GuestbookManager();
            guestbookManager.setupMe();
            return View(guestbookManager);
        }

        public IActionResult AddEntry(GuestbookManager guestbookManager) {
            return View(guestbookManager);
        }

        public IActionResult AddSubmit(GuestbookManager guestbookManager, string fName, string lName, string ent) {
            return View(guestbookManager);
        }
    }
}
