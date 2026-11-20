using DuAnCuoiKy_Nhom08.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DuAnCuoiKy_Nhom08.Views
{
    public class AdminController : Controller
    {
        private PortfioEntities db =new PortfioEntities();
        // GET: Admin
        [HttpGet]
        public ActionResult Dangky()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Dangky(Admin admin)
        {
            if (ModelState.IsValid)
            {
                if (string.IsNullOrEmpty(admin.Hoten))
                    ModelState.AddModelError(string.Empty, "Họ và tên không được để trống");
                if (string.IsNullOrEmpty(admin.Username))
                    ModelState.AddModelError(string.Empty, "Tên đăng nhập không được để trống");
            if (string.IsNullOrEmpty(admin.PasswordHash))
                    ModelState.AddModelError(string.Empty, "Mật khẩu không được để trống");
            if (string.IsNullOrEmpty(admin.Email))
                    ModelState.AddModelError(string.Empty, "Email không được để trống");
                    
//Kiểm tra xem có người nào đã đăng kí với tên đăng nhập này hay chưa
                    var nguoidung = db.Admins.FirstOrDefault(k => k.Username == admin.Username);
                if (nguoidung != null)
                    ModelState.AddModelError(string.Empty, "Đã có người đăng kí tên này");
            if (ModelState.IsValid)
                {
                    db.Admins.Add(admin);
                    db.SaveChanges();

                }
              
                Profile profile = new Profile
                {
                    AdminID = admin.AdminID,
                    FullName = admin.Hoten,
                    Email = admin.Email
                };

                db.Profiles.Add(profile);
                db.SaveChanges();

            }
            return RedirectToAction("Dangnhap","Admin");


        }
        [HttpGet]
        public ActionResult Dangnhap()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Dangnhap(Admin admin)
        {
            if (ModelState.IsValid)
            {
                if (string.IsNullOrEmpty(admin.Username))
                    ModelState.AddModelError(string.Empty, "Tên đăng nhập không được để trống");
                if (string.IsNullOrEmpty(admin.PasswordHash))
                    ModelState.AddModelError(string.Empty, "Mật khẩu không được để trống");
                if (ModelState.IsValid)
                {
                    //Tìm khách hàng có tên đăng nhập và password hợp lệ trong CSDL
                    var nguoidung = db.Admins.FirstOrDefault(k => k.Username == admin.Username && k.PasswordHash == admin.PasswordHash);
                    if (nguoidung != null)
                    {
                        ViewBag.ThongBao = "Chúc mừng đăng nhập thành công";
                        //Lưu vào session
                        Session["TaiKhoan"] = nguoidung;
                    }
                    else
                        ViewBag.ThongBao = "Tên đăng nhập hoặc mật khẩu không đúng";
                }
            }
            return View();
        }
        public ActionResult Dangxuat()
        { 
            Session.Clear();

            // 2. Hủy bỏ phiên làm việc (Session ID) hiện tại
            Session.Abandon();

            return RedirectToAction("Dangnhap", "Admin");
        }
    }
}
