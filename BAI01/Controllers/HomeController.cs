using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BAI01.Models;

namespace BAI01.Controllers
{
    public class HomeController : Controller
    {
        Model1 db = new Model1();
        public ActionResult Index()
        {
            // lấy tất cả thể loại trong csdl
            List<LoaiTinTuc> ds = db.LoaiTinTucs.ToList();
            return View(ds);
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
        public ActionResult ThemMoi()
        {
            ViewBag.MaTinTuc = new SelectList(db.TinTucs.ToList(), "MaTinTuc", "TenTinTuc");
            return View(new LoaiTinTuc());
        }

        [HttpPost]
        public ActionResult ThemMoi(LoaiTinTuc tuc)
        {
            if (ModelState.IsValid)
            {
                db.LoaiTinTucs.Add(tuc);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            // PHẢI gán lại ViewBag.MaTinTuc nếu có lỗi để DropDownList có dữ liệu
            ViewBag.MaTinTuc = new SelectList(db.TinTucs.ToList(), "MaTinTuc", "TenTinTuc", tuc.MaTinTuc);
            return View(tuc);
        }


        public ActionResult Edit(int id)
        {
            var EB_tin = db.LoaiTinTucs.FirstOrDefault(m=>m.MaLoaiTinTuc==id);
            return View(EB_tin);
        }
        [HttpPost]
        public ActionResult Edit(LoaiTinTuc model)
        {
            if (ModelState.IsValid)
            {
                db.Entry(model).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(model);
        }

        public ActionResult Details(int id)
        {
            var details_tin = db.LoaiTinTucs.Where(m => m.MaLoaiTinTuc == id).First();
            return View(details_tin);
        }

        public ActionResult Delete(int id) {
            var D_tin = db.LoaiTinTucs.FirstOrDefault(m => m.MaLoaiTinTuc == id);
            return View(D_tin);
                }

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection) 
        {
            var D_tin = db.LoaiTinTucs.FirstOrDefault(m => m.MaLoaiTinTuc == id);

            db.LoaiTinTucs.Remove(D_tin);
    
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}