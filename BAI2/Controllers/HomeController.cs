using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BAI2.Models;

namespace BAI2.Controllers
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
        public ActionResult DMSach()
        {
            List<SACH> list = db.SACHes.ToList();
            return View(list);
        }

        [HttpGet]
   

        public ActionResult ThemSachMoi()
        {
            // đưa dl vào dropdownlist lấy ds từ table chủ đề ,sắp xếp tăng
            ViewBag.MaCD = new SelectList(db.CHUDEs.ToList().OrderBy(n=>n.TENCHUDE),"MACD","TENCHUDE");

            // đưa dl vào dropdownlist nhà xuất bản
            ViewBag.MaNXB = new SelectList(db.NHAXUATBANs.ToList().OrderBy(n => n.TENNXB), "MANXB", "TENNXB");
            return View();
        }


        [HttpPost]
        [ValidateInput(false)]
        public ActionResult ThemSachMoi(SACH sACH,HttpPostedFileBase fileupLoad)
        {
            // đưa dữ liệu vào dropdownlist
            ViewBag.MaCD = new SelectList(db.CHUDEs.ToList().OrderBy(n => n.TENCHUDE), "MACD", "TENCHUDE");
            ViewBag.MaNXB = new SelectList(db.NHAXUATBANs.ToList().OrderBy(n => n.TENNXB), "MANXB", "TENNXB");

            // kiểm tra đường dẫn file 
            if (fileupLoad != null) {
                ViewBag.Thongbao = "Vui lòng chọn ảnh bìa!";
                return View();
            }

            // thêm vào CSDL
            if (ModelState.IsValid) {
                //Lưu tên file , lưu ý bỏ usinhg thư viện System.IO;
                //
                var filename = Path.GetFileName(fileupLoad.FileName);

                //lưu đường dẫn của file
                var path = Path.Combine(Server.MapPath("~/Content/img/"),filename);

                if (System.IO.File.Exists(path))
                {
                    ViewBag.Thongbao = "Hình ảnh đã tồn tại";

                }
                else
                {
                    fileupLoad.SaveAs(path); 
                }

                sACH.ANHBIA = filename;

                db.SACHes.Add(sACH);
                db.SaveChanges();
            
            }
            return RedirectToAction("SACH");

        }
    }
}