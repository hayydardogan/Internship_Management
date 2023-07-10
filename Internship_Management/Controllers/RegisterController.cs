using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Internship_Management.Models.Entity;

namespace Internship_Management.Controllers
{
    public class RegisterController : Controller
    {
        internshipManagementEntities db = new internshipManagementEntities();
        public ActionResult Index()
        {
            List<SelectListItem> fakulteler = (from x in db.TBL_Fakulteler.ToList()
                                             select new SelectListItem
                                             {
                                                 Text = x.fakulteAdi,
                                                 Value = x.fakulteID.ToString()
                                             }).ToList();
            ViewBag.fklt = fakulteler;

            List<SelectListItem> bolumler = (from x in db.TBL_Bolumler.ToList()
                                             select new SelectListItem
                                             {
                                                 Text = x.bolumAdi,
                                                 Value = x.bolumID.ToString()
                                             }).ToList();
            ViewBag.blm = bolumler;
            return View();
        }

        // Öğrenci kayıt işlemleri
        [HttpPost]
        public JsonResult createAccount(TBL_Ogrenciler ogr)
        {
            db.TBL_Ogrenciler.Add(ogr);
            if (db.SaveChanges() > 0)
            {
                return Json("ok");
            }
            else
            {
                return Json("hata");
            }
        }

        // Fakülteye göre bölümlerin değişmesi işlemi
        public JsonResult changeDepartment(int id)
        {
            var departments = (from x in db.TBL_Bolumler
                               join y in db.TBL_Fakulteler on x.fakulteID equals y.fakulteID
                               where x.fakulteID == id
                               select new
                               {
                                   Text = x.bolumAdi,
                                   Value = x.bolumID.ToString()
                               }).ToList();

            return Json(departments, JsonRequestBehavior.AllowGet);
        }
    }
}