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

        public ActionResult Create() 
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(string Mem_Name,string Mem_Phone,int Mem_salary) 
        {
            tMember member = new tMember();
            member.Mem_Name = Mem_Name;
            member.Mem_Phone = Mem_Phone;
            member.Mem_Salary = Mem_salary;
            db.tMember.Add(member);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}