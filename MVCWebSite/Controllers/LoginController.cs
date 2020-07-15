using MVCWebSite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCWebSite.Controllers
{
    public class LoginController : Controller
    {
        dbMemberEntities db = new dbMemberEntities();
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(string Id, string Pwd)
        {
            var check = db.tManager.Where(m=> m.Account.ToString() == Id.Trim()).Select(m => m.Pwd.ToString() ).ToList();
            if (check.Count() != 0)
            {
                Response.Write("< script > alert(check.Count()) </ script >");
                return RedirectToAction("Index", "Home");
            }
            else 
            {
                return View();
            }

        }
    }
}