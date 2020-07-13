using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LinqKit;
using MVCWebSite.Models;
using MVCWebSite.Service;
namespace MVCWebSite.Controllers
{
    public class HomeController : Controller
    {
        dbMemberEntities db = new dbMemberEntities();

        //[ActionName("路由名稱")] //可用此方式於網址路由中不直接用Action Function的名稱
        //[HttpGet]預設為Get傳送//可省略
        public ActionResult Index()
        {
            var member = db.tMember.Where(m=>m.Mem_Exist =="0").OrderBy(m => m.Mem_Id).ToList();
            return View(member);
        }
        [HttpPost]
        public ActionResult Index(int Mem_Id =0, string Mem_Name="", string Mem_Phone="", int Mem_salaryMax=100000, int Mem_salaryMin=0) 
        {
            var filter = PredicateBuilder.New<tMember>();
            filter.And(a => a.Mem_Exist == "0");
            if (Mem_Id != 0)
            { filter = filter.And(a => a.Mem_Id == Mem_Id); }
            if (Mem_Name.Trim() != "")
            { filter = filter.And(a => a.Mem_Name == Mem_Name); }
            if (Mem_Phone.Trim() != "")
            { filter = filter.And(a => a.Mem_Phone == Mem_Phone); }
            if (Mem_salaryMax != 100000)
            { filter = filter.And(a => a.Mem_Salary <= Mem_salaryMax); }
            if (Mem_salaryMin != 0)
            { filter = filter.And(a => a.Mem_Salary >= Mem_salaryMin); }
            var member = db.tMember.Where(filter) .OrderBy(m => m.Mem_Id).ToList();
            return View(member);
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
            //以直接delete實作刪除功能
            //var member = db.tMember.Where(m => m.Mem_Id == Mem_Id).FirstOrDefault();
            //db.tMember.Remove(member);
            //以update實作刪除功能
            var member = db.tMember.Where(m => m.Mem_Id == Mem_Id).FirstOrDefault();
            member.Mem_Exist = "1";
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Upload() 
        {

            return View();
        }
        [HttpPost]
        public ActionResult Upload(HttpPostedFileBase file) 
        {

            string filename = "";
            if (file != null && file.ContentLength > 0) 
            {
                //filename = Path.GetFileName(file.FileName);
                filename = "Pic"+DateTime.Now.Millisecond.ToString()+ RandomServer.RandomWord(5);
                string path = string.Format("{0}/{1}", Server.MapPath("~/images"), filename);
                file.SaveAs(path);
            }
            return RedirectToAction("ShowPhotos");
        }
        public string ShowPhotos() 
        {
            string show = "";
            DirectoryInfo dir = new DirectoryInfo(Server.MapPath("~/images"));
            FileInfo[] fileInfos = dir.GetFiles();
            foreach (FileInfo item in fileInfos)
            {
                show += string.Format
                    ("<img src= '../images/{0}' width='160' height ='120'> "
                    , item.Name);
            }
            show += "<hr>";
            show += "<a href= 'Upload'>返回</a>";
            return show;
        }

    }
}