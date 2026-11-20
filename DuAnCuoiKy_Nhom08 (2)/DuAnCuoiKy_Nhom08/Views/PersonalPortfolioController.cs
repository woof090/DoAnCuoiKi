using DuAnCuoiKy_Nhom08.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DuAnCuoiKy_Nhom08.Views
{
    public class PersonalPortfolioController : Controller
    {
        private PortfioEntities database =new PortfioEntities();   
        // GET: PersonalPortfolio
        public ActionResult Index()
        {
            return View();
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