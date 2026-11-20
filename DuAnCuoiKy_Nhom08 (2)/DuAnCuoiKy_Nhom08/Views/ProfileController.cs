using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DuAnCuoiKy_Nhom08.Models;
namespace DuAnCuoiKy_Nhom08.Views
{
    public class ProfileController : Controller
    {
        private PortfioEntities db = new PortfioEntities();
        // GET: Profile
        public ActionResult Xemprofile()
        {
            // 1. Nếu chưa đăng nhập hoặc đã ấn Đăng xuất -> đá thẳng về trang Đăng nhập
            if (Session["TaiKhoan"] == null)
            {
                return RedirectToAction("Dangnhap", "Admin");
            }

            // 2. Lấy đúng tài khoản Admin đang đăng nhập trong Session
            var admin = (DuAnCuoiKy_Nhom08.Models.Admin)Session["TaiKhoan"];

            // 3. Chỉ lọc đúng Profile của Admin đó (giữ nguyên .ToList() để khớp với @model bên View)
            var profile = db.Profiles.Where(p => p.AdminID == admin.AdminID).ToList();

            return View(profile);
        }
        [HttpGet]
        public ActionResult ChinhsuaProfile(int id)
        {
            var profile = db.Profiles.Where(c => c.ProfileID == id).FirstOrDefault();
            return View(profile);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChinhsuaProfile(int id, Profile profile, HttpPostedFileBase ImageFile)
        {
            try
            {
                // Lấy bản ghi hiện tại từ DB để giữ lại Avatar cũ nếu người dùng không chọn ảnh mới
                var existingProfile = db.Profiles.AsNoTracking().FirstOrDefault(p => p.ProfileID == id);

                if (ImageFile != null && ImageFile.ContentLength > 0)
                {
                    string folderPath = Server.MapPath("~/Images/");
                    if (!System.IO.Directory.Exists(folderPath))
                    {
                        System.IO.Directory.CreateDirectory(folderPath);
                    }

                    string fileName = System.IO.Path.GetFileName(ImageFile.FileName);
                    string fullPath = System.IO.Path.Combine(folderPath, fileName);
                    ImageFile.SaveAs(fullPath);

                    profile.Avatar = "/Images/" + fileName;
                }
                else if (existingProfile != null)
                {
                    // Không chọn ảnh mới -> giữ nguyên đường dẫn ảnh cũ
                    profile.Avatar = existingProfile.Avatar;
                }

                db.Entry(profile).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Xemprofile");
            }
            catch
            {
                return View(profile);
            }
        }
    }
}