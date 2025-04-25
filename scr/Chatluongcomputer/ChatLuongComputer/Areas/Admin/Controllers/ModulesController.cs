using ChatLuongComputer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ChatLuongComputer.Areas.Admin.Controllers
{
    public class ModulesController : Controller
    {
        private ChatLuongComputerDbContext db = new ChatLuongComputerDbContext();
        // GET: Admin/Modules
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Actions()
        {
            ViewBag.CountOrderSuccess = db.Orders.Where(m => m.Status == 3).Count();
            ViewBag.CountOrderCancel = db.Orders.Where(m => m.Status == 1).Count();
            ViewBag.CountContactDoneReply = db.Contacts.Where(m => m.Flag == 0).Count();
            ViewBag.CountUser = db.Users.Where(m => m.Status != 0).Count();
            return View();
        }
        public ActionResult Toart()
        {
            return View();
        }
    }
}