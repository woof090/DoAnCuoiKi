using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using DuAnCuoiKy_Nhom08.Models;

namespace DuAnCuoiKy_Nhom08.Controllers
{
    public class ProjectsController : Controller
    {
        private PortfioEntities db = new PortfioEntities();

        [HttpGet]
        public ActionResult Xemproject(string search, int? categoryId, int? tagId)
        {
            ViewBag.Categories = db.Categories.ToList();
            ViewBag.Tags = db.Tags.ToList();

            var q = db.Projects.Include(p => p.Category).Include(p => p.Tags).AsQueryable();

            if (!string.IsNullOrEmpty(search))
                q = q.Where(p => p.Title.Contains(search) || p.Description.Contains(search));

            if (categoryId.HasValue)
                q = q.Where(p => p.CategoryID == categoryId.Value);

            if (tagId.HasValue)
                q = q.Where(p => p.Tags.Any(t => t.TagID == tagId.Value));

            return View(q.OrderBy(p => p.DisplayOrder).ToList());
        }
        public ActionResult Xemchitiet(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var p = db.Projects.Include(x => x.Category).Include(x => x.Tags).FirstOrDefault(x => x.ProjectID == id);
            if (p == null) return HttpNotFound();
            return View(p);
        }
        [HttpGet]
        public ActionResult Taomoi()
        {
            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "CategoryName");
            ViewBag.AllTags = db.Tags.ToList();
            return View(new Project { CreatedDate = DateTime.Now, Status = true });
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Taomoi(Project project, int[] selectedTags)
        {
            if (selectedTags != null)
            {
                foreach (var tid in selectedTags)
                    project.Tags.Add(db.Tags.Find(tid));
            }
            if (ModelState.IsValid)
            {
                db.Projects.Add(project);
                db.SaveChanges();
                return RedirectToAction("Xemproject");
            }
            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "CategoryName", project.CategoryID);
            ViewBag.AllTags = db.Tags.ToList();
            return View(project);
        }

        // GET: Form sửa
        [HttpGet]
        public ActionResult Chinhsua(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var p = db.Projects.Include(x => x.Tags).FirstOrDefault(x => x.ProjectID == id);
            if (p == null) return HttpNotFound();

            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "CategoryName", p.CategoryID);
            ViewBag.AllTags = db.Tags.ToList();
            return View(p);
        }

        // POST: Lưu sửa
        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Chinhsua(Project project, int[] selectedTags)
        {
            var pInDb = db.Projects.Include(x => x.Tags).FirstOrDefault(x => x.ProjectID == project.ProjectID);
            if (pInDb == null) return HttpNotFound();

            if (ModelState.IsValid)
            {
                pInDb.Title = project.Title;
                pInDb.Description = project.Description;
                pInDb.Image = project.Image;
                pInDb.ProjectURL = project.ProjectURL;
                pInDb.GithubURL = project.GithubURL;
                pInDb.CategoryID = project.CategoryID;
                pInDb.Status = project.Status;
                pInDb.DisplayOrder = project.DisplayOrder;

                pInDb.Tags.Clear();
                if (selectedTags != null)
                {
                    foreach (var tid in selectedTags)
                        pInDb.Tags.Add(db.Tags.Find(tid));
                }

                db.SaveChanges();
                return RedirectToAction("Xemproject");
            }
            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "CategoryName", project.CategoryID);
            ViewBag.AllTags = db.Tags.ToList();
            return View(project);
        }


        [HttpGet]
        public ActionResult Xacnhanxoa(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var project = db.Projects
                .Include(p => p.Category)
                .Include(p => p.Tags)
                .FirstOrDefault(p => p.ProjectID == id);

            if (project == null) return HttpNotFound();

            return View(project);
        }
        [HttpPost, ActionName("Xacnhanxoa")]
        [ValidateAntiForgeryToken]
        public ActionResult XacnhanxoaConfirmed(int id)
        {
            var project = db.Projects
                .Include(p => p.Tags)
                .FirstOrDefault(p => p.ProjectID == id);

            if (project != null)
            {
                project.Tags.Clear();
                db.Projects.Remove(project);
                db.SaveChanges();
            }

            return RedirectToAction("Xemproject");
        }
    }
}