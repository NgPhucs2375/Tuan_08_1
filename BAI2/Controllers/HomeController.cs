using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
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

        Model2 db = new Model2();
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
            if (fileupLoad == null) {
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
            return RedirectToAction("DMSach");

        }

        public ActionResult Chitietsach(int? id)
        {
            if (id == null)
            {
                // Trả về lỗi 400 kèm thông báo rõ ràng
                return new HttpStatusCodeResult(400, "Yêu cầu không hợp lệ: thiếu tham số id");
            }

            var sach = db.SACHes.SingleOrDefault(n => n.MASACH == id);
            if (sach == null)
            {
                // Trả về lỗi 404 nếu không tìm thấy sách
                return HttpNotFound("Không tìm thấy sách");
            }

            return View(sach);
        }

        [HttpGet]
        public ActionResult Xoasach(int id)
        {
            SACH sach = db.SACHes.SingleOrDefault(n => n.MASACH == id);
            ViewBag.IDsach = sach.MASACH;
            if (sach == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(sach);
        }
        [HttpPost, ActionName("Xoasach")]
        public ActionResult Xacnhanxoa(int id)
        {
            //lay doi tuong  sach can xoa theo ma
            SACH sach = db.SACHes.SingleOrDefault(n => n.MASACH == id);
            ViewBag.MaSach = sach.MASACH;
            if (sach == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            db.SACHes.Remove(sach);
            db.SaveChanges();
            return RedirectToAction("DMSach");
        }

        [HttpGet]
        public ActionResult Suasach(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(400, "Thiếu tham số id");

            SACH sach = db.SACHes.SingleOrDefault(n => n.MASACH == id);
            if (sach == null)
                return HttpNotFound("Không tìm thấy sách");

            // Gán ViewBag.CurrentSachId để layout navbar dùng
            ViewBag.CurrentSachId = sach.MASACH;

            // Dropdown cho view
            ViewBag.MaCD = new SelectList(db.CHUDEs.OrderBy(n => n.TENCHUDE), "MACD", "TENCHUDE", sach.MACD);
            ViewBag.MaNXB = new SelectList(db.NHAXUATBANs.OrderBy(n => n.TENNXB), "MANXB", "TENNXB", sach.MANXB);

            return View(sach);
        }


        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Suasach(SACH sach, HttpPostedFileBase fileUpload)
        {
            // Dropdown lại để view render đúng nếu lỗi xảy ra
            ViewBag.MaCD = new SelectList(db.CHUDEs.OrderBy(n => n.TENCHUDE), "MACD", "TENCHUDE", sach.MACD);
            ViewBag.MaNXB = new SelectList(db.NHAXUATBANs.OrderBy(n => n.TENNXB), "MANXB", "TENNXB", sach.MANXB);

            if (ModelState.IsValid)
            {
                var sachDb = db.SACHes.SingleOrDefault(s => s.MASACH == sach.MASACH);
                if (sachDb == null)
                    return HttpNotFound("Không tìm thấy sách");

                // Cập nhật thông tin sách
                sachDb.TENSACH = sach.TENSACH;
                sachDb.GIABAN = sach.GIABAN;
                sachDb.MOTA = sach.MOTA;
                sachDb.MACD = sach.MACD;
                sachDb.MANXB = sach.MANXB;
                sachDb.NGAYCAPNHAT = sach.NGAYCAPNHAT;

                // Xử lý file ảnh
                if (fileUpload != null)
                {
                    var filename = Path.GetFileName(fileUpload.FileName);
                    var path = Path.Combine(Server.MapPath("~/Content/img/"), filename);
                    if (!System.IO.File.Exists(path))
                        fileUpload.SaveAs(path);

                    sachDb.ANHBIA = filename;
                }

                db.SaveChanges();
                return RedirectToAction("DMSach");
            }

            // Nếu lỗi, render lại view
            return View(sach);
        }

    }
}