using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Internship_Management.Models.Entity;
using Microsoft.Ajax.Utilities;

namespace Internship_Management.Controllers
{
    public class HomeController : Controller
    {
        internshipManagementEntities db = new internshipManagementEntities();

        public static string uploadMessage = "";


        public ActionResult Index()
        {
            if (LoginController.aktifOgr != null && LoginController.aktifAka == null)
            {
                var ogr = db.TBL_Ogrenciler.Where(x => x.ogrenciNo == LoginController.aktifOgr.ogrenciNo).FirstOrDefault();
                var dosyalar = db.TBL_Dosyalar.Where(x => x.dosyaGonderen == ogr.ogrenciID).OrderByDescending(x => x.dosyaTarihi).ToList();

                return View(dosyalar);
            }
            else if (LoginController.aktifAka != null && LoginController.aktifOgr == null)
            {
                return RedirectToAction("/Index", "Akademisyen");
            }
            else
            {
                return RedirectToAction("/Index", "Login");
            }

        }

        public ActionResult Bildirimler()
        {
            if (LoginController.aktifOgr != null)
            {
                var ogr = db.TBL_Ogrenciler.Where(x => x.ogrenciNo == LoginController.aktifOgr.ogrenciNo).FirstOrDefault();
                var bildirimler = db.TBL_Bildirimler.Where(x => x.bildirimAlan == ogr.ogrenciID).OrderByDescending(x => x.bildirimTarih).ToList();
                foreach (var x in bildirimler)
                {
                    x.bildirimOkunduMu = true;
                }
                db.SaveChanges();
                return View(bildirimler);
            }
            else if (LoginController.aktifAka != null)
            {
                return RedirectToAction("/Index", "Akademisyen");
            }
            else
            {
                return RedirectToAction("/Index", "Login");
            }

        }

        public JsonResult AciklamaGetir(int id)
        {
            var dosya = db.TBL_Dosyalar.Find(id);
            string aciklama = dosya.dosyaAciklama;
            return Json(aciklama, JsonRequestBehavior.AllowGet);
        }

        public ActionResult SSS()
        {
            if (LoginController.aktifOgr != null)
            {
                return View();
            }
            else if (LoginController.aktifAka != null)
            {
                return RedirectToAction("/Index", "Akademisyen");
            }
            else
            {
                return RedirectToAction("/Index", "Login");
            }
        }

        public JsonResult getInternship()
        {
            List<SelectListItem> interns = (from x in db.TBL_StajTurleri.ToList()
                                            select new SelectListItem
                                            {
                                                Text = x.stajAdi,
                                                Value = x.stajID.ToString()
                                            }).ToList();

            return Json(interns, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public PartialViewResult fileUpload()
        {
            List<SelectListItem> stajTurleri = (from x in db.TBL_StajTurleri.ToList()
                                                select new SelectListItem
                                                {
                                                    Text = x.stajAdi,
                                                    Value = x.stajID.ToString()
                                                }).ToList();
            ViewBag.st = stajTurleri;
            return PartialView();
        }

        [HttpPost]
        public ActionResult FormUpload(HttpPostedFileBase uploadedFile, TBL_Dosyalar dosya)
        {

            // File Upload İşlemleri 
            if (uploadedFile != null)
            {

                // Oturum açan öğrenci bilgilerini getirme
                var ogr = db.TBL_Ogrenciler.Where(x => x.ogrenciNo == LoginController.aktifOgr.ogrenciNo).FirstOrDefault();


                DateTime date = new DateTime(2000, 7, 1);
                TimeSpan diff = DateTime.Now - date;
                double seconds = diff.TotalSeconds;

                string fileName = Path.GetFileName(uploadedFile.FileName);

                // Dosya adının çakışmaması için sonuna güncel saat bilgisi ekleyelim
                //fileName = seconds.ToString() + fileName;
                fileName = LoginController.aktifOgr.ogrenciNo.ToString() + "_" + seconds.ToString() + "_" + fileName;
                var path = Path.Combine(Server.MapPath("~/Files"), fileName);

                uploadedFile.SaveAs(path);


                var fileExt = System.IO.Path.GetExtension(uploadedFile.FileName).Substring(1);
                if (fileExt.ToString() == "pdf")
                {
                    dosya.dosyaTarihi = DateTime.Now;
                    dosya.dosyaAdi = fileName;
                    dosya.dosyaGonderen = ogr.ogrenciID;
                    dosya.dosyaDurum = "0";
                    dosya.dosyaYolu = "Files/" + fileName;
                    if(dosya.dosyaAciklama == null)
                    {
                        dosya.dosyaAciklama = "Açıklama yok.";
                    }
                    db.TBL_Dosyalar.Add(dosya);


                    if (db.SaveChanges() > 0)
                    {
                        TempData["uploadMessage"] = "ok";

                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    TempData["fileExt"] = fileExt;
                    return RedirectToAction("Index", "Home");
                }


            }

            TempData["uploadMessage"] = "error";
            return RedirectToAction("Index", "Home");
        }





    }
}