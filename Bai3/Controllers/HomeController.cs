using Bai3.Models;
using Bai3.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;

namespace Bai3.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        Model1 db = new Model1();
        public ActionResult DSNhanVien()
        {
            List<Employee> list = db.Employees.ToList();
            return View(list);
        }
        [HttpGet]
        public ActionResult ThemNhanVien()
        {
            ViewBag.IDDep= new SelectList(db.Deparments.ToList().OrderBy(n => n.NameDept), "Deptid", "NameDept");

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ThemNhanVien(Employee emp)
        {
            if (ModelState.IsValid)
            {
                db.Employees.Add(emp);
                db.SaveChanges();
                return RedirectToAction("DSNhanVien");
            }
            // Nếu lỗi thì render lại view
            ViewBag.IDDep = new SelectList(db.Deparments.ToList().OrderBy(n => n.NameDept), "Deptid", "NameDept", emp.Deptid);
            return View(emp);
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var emp = db.Employees.Find(id);
            ViewBag.IDDep = new SelectList(db.Deparments.ToList().OrderBy(n => n.NameDept), "Deptid", "NameDept", emp.Deptid);
            return View(emp);
        }
        [HttpPost]
        public ActionResult Edit(Employee emp)
        {
            if (ModelState.IsValid)
            {
                db.Entry(emp).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("DSNhanVien");
            }
            ViewBag.IDDep = new SelectList(db.Deparments.ToList().OrderBy(n => n.NameDept), "Deptid", "NameDept", emp.Deptid);
            return View(emp);
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            var emp = db.Employees.Find(id);
            return View(emp);
        }
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            var emp = db.Employees.Find(id);
            db.Employees.Remove(emp);
            db.SaveChanges();
            return RedirectToAction("DSNhanVien");
        }

        public ActionResult Details(int id)
        {
            var emp = db.Employees.Find(id);
            return View(emp);
        }
   
       
        public ActionResult DSNVPB()
        {
            var list = from e in db.Employees
                       join d in db.Deparments on e.Deptid equals d.Deptid
                       select new EmployeeViewModel
                       {
                           Id = e.Id,
                           NameEmpl = e.NameEmpl,
                           Gender = e.Gender,
                           City = e.City,
                           DeptName = d.NameDept,
                       };
            ViewBag.TotalEmployees = list.Count();
            return View(list.ToList());
        }

        public ActionResult ListPB(int? deptId)
        {
            var model = new ListPB();
            model.Deparments = db.Deparments.ToList();
            model.SelectedDeptId = deptId ?? model.Deparments.FirstOrDefault()?.Deptid;
            model.Employees = db.Employees
                                .Where(e => e.Deptid == model.SelectedDeptId)
                                .ToList();
            return View(model);
        }
    }
}