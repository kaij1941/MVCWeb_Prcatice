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
            var emps = db.tMember.Where(m=>m.Mem_Exist =="0").OrderBy(m => m.Mem_Id).ToList();

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

        public ActionResult Edit(int Mem_Id) 
        {
            var member = db.tMember.Where(m => m.Mem_Id == Mem_Id).FirstOrDefault();
            return View(member);
        }
        [HttpPost]
        public ActionResult Edit(int Mem_Id, string Mem_Name, string Mem_Phone, int Mem_salary)
        {
            var member = db.tMember.Where(m => m.Mem_Id == Mem_Id).FirstOrDefault();
            member.Mem_Name = Mem_Name;
            member.Mem_Phone = Mem_Phone;
            member.Mem_Salary = Mem_salary;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Delete(int Mem_Id)
        {
            //var member = db.tMember.Where(m => m.Mem_Id == Mem_Id).FirstOrDefault();
            //db.tMember.Remove(member);
            var member = db.tMember.Where(m => m.Mem_Id == Mem_Id).FirstOrDefault();
            member.Mem_Exist = "1";
            db.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}