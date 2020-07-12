using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using MVCWebSite.Models;
namespace MVCWebSite.Controllers
{
    public class HomeController : Controller
    {
        dbMemberEntities db = new dbMemberEntities();
        // GET: Home
        public ActionResult Index()
        {
            var emps = db.tMember.OrderBy(m => m.Mem_Id).ToList();

            return View(emps);
        }
    }
}