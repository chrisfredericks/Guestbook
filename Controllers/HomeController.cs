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
            // keep the box checked after postback
            if (Convert.ToString(TempData["addEntryCheck"]) == "1") {
                guestbookManager.addEntryCheck = true;
            }
            guestbookManager.setupMe();
            return View(guestbookManager);  
        }

        [HttpPost]
        public IActionResult AddSubmit(GuestbookManager guestbookManager, string firstName, string lastName, string entry) {
            guestbookManager.addEntry(firstName, lastName, entry);


            guestbookManager.setupMe();
            TempData["addEntryCheck"] = "1";
            return RedirectToAction("Index");
        }
    }
}
