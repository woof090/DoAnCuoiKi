using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DuAnCuoiKy_Nhom08.Helpers;
using DuAnCuoiKy_Nhom08.Models;

namespace DuAnCuoiKy_Nhom08.Views
{
    public class PersonalPortfolioController : Controller
    {
        private const int BlogPageSize = 6;
        private PortfioEntities database =new PortfioEntities();

        public object PasswordHelper { get; private set; }

        protected override void Dispose(bool disposing)
        {
            if (disposing) database.Dispose();
            base.Dispose(disposing);
        }
        // GET: PersonalPortfolio
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Login(string returnUrl)
        {
            if (Session["AdminID"] != null) return RedirectToAction("Blogs", "Admin");
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(string username, string password, string returnUrl)
        {
            username = (username ?? "").Trim();
            ViewBag.ReturnUrl = returnUrl;
            ViewBag.Username = username;
            var ip = Request.UserHostAddress;

            if (username.Length == 0 || string.IsNullOrEmpty(password))
            {
                ViewBag.LoginError = "Vui lòng nhập tên đăng nhập và mật khẩu.";
                return View();
            }
            if (LoginThrottle.IsLocked(ip, username))
            {
                ViewBag.LoginError = "Bạn đã đăng nhập sai quá nhiều lần. Vui lòng thử lại sau 5 phút.";
                return View();
            }

            var admin = database.Admins.FirstOrDefault(a => a.Username == username);
            var needsUpgrade = false;
            var ok = admin != null && Helpers.PasswordHelper.Verify(password, admin.PasswordHash, out needsUpgrade);
            if (!ok)
            {
                LoginThrottle.RegisterFailure(ip, username);
                ViewBag.LoginError = "Tên đăng nhập hoặc mật khẩu không đúng.";
                return View();
            }

            if (needsUpgrade)
            {
                // Dữ liệu cũ đang lưu mật khẩu thô -> băm lại
                admin.PasswordHash = Helpers.PasswordHelper.Hash(password);
                admin.ConfirmPassword = null;
                database.SaveChanges();
            }

            LoginThrottle.Reset(ip, username);
            Session.Clear();
            Session["AdminID"] = admin.AdminID;
            Session["AdminName"] = string.IsNullOrWhiteSpace(admin.Hoten) ? admin.Username : admin.Hoten;

            if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl)) return Redirect(returnUrl);
            return RedirectToAction("Blogs", "Admin");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Logout()
        {
            Session.Clear();
            Session.Abandon();
            return RedirectToAction("Index");
        }
        public ActionResult GioiThieu()
        {
            var profile = database.Profiles.OrderBy(p => p.ProfileID).FirstOrDefault();
            return View(profile);
        }
        public ActionResult KyNang()
        {
            return View();
        }
        public ActionResult DuAn()
        {
            return View();
        }
        public ActionResult KinhNghiem()
        {
            return View();
        }
        public ActionResult HocVan()
        {
            return View();
        }
        
        public ActionResult Blog()
        {
            return View();
        }
        public ActionResult LienHe()
        {
            return View();
        }
    }
}