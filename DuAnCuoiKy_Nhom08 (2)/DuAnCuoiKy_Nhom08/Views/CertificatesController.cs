using DuAnCuoiKy_Nhom08.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace DuAnCuoiKy_Nhom08.Views
{
    public class CertificatesController : Controller
    {
        PortfioEntities db = new PortfioEntities();
        // GET: ChungChi
        public ActionResult ChungChi()
        {
            var certificates = db.Certificates.ToList();
            return View(certificates);
        }

        [HttpGet]
        public ActionResult ThemChungChi()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ThemChungChi(Certificate certificate, HttpPostedFileBase ImageFile)
        {
            ModelState.Remove("Image");
            if (ModelState.IsValid)
            {
                if (ImageFile != null && ImageFile.ContentLength > 0)
                {
                    string fileName = System.IO.Path.GetFileName(ImageFile.FileName);
                    string folderPath = Server.MapPath("~/Images/");

                    if (!System.IO.Directory.Exists(folderPath))
                    {
                        System.IO.Directory.CreateDirectory(folderPath);
                    }

                    
                    string fullPath = System.IO.Path.Combine(folderPath, fileName);
                    ImageFile.SaveAs(fullPath);

                    certificate.Image = "~/Images/" + fileName;
                }

                db.Certificates.Add(certificate);
                db.SaveChanges();

                return RedirectToAction("ChungChi");
            }

            return View(certificate);
        }
        public ActionResult Xemchitiet(int id)
        {
            var certificate=db.Certificates.Where(c=>c.CertificateID==id).FirstOrDefault();
            return View(certificate);
        }

        [HttpGet]
        public ActionResult Chinhsua(int id)
        {
            var certificate = db.Certificates.Where(c => c.CertificateID == id).FirstOrDefault();
            return View(certificate);
        }

        [HttpPost]
        public ActionResult Chinhsua(int id, Certificate certificate)
        {
            db.Entry(certificate).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("ChungChi");
        }

        [HttpGet]
        public ActionResult XoaChungChi(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var cetificate = db.Certificates.Where(c => c.CertificateID == id).FirstOrDefault();
            if (cetificate == null)
            {
                return HttpNotFound();
            }
            return View(cetificate);
        }

        [HttpPost, ActionName("XoaChungChi")]

        public ActionResult XacnhanXoa(int id)
        {
            try
            {
                var chungchi = db.Certificates.Where(c => c.CertificateID == id).FirstOrDefault();
                db.Certificates.Remove(chungchi);
                db.SaveChanges();
                return RedirectToAction("ChungChi");
            }
            catch
            {
                return Content("Không xóa được do có liên quan đến bảng khác");
            }
        }

    }
}