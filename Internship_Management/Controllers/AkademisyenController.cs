using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Internship_Management.Models.Entity;

namespace Internship_Management.Controllers
{
    public class AkademisyenController : Controller
    {
        internshipManagementEntities db = new internshipManagementEntities();

        [AllowAnonymous]
        public ActionResult Index()
        {
            if(LoginController.aktifAka != null && LoginController.aktifOgr == null)
            {
                dynamic myModel = new ExpandoObject();

                var akademisyen = db.TBL_Akademisyenler.Where(x => x.akaMail == LoginController.aktifAka.akaMail).FirstOrDefault();

                var degerlendirilmemisDosyalar = db.TBL_Dosyalar.Where(x => x.stajTuru == akademisyen.akaStajTuru && x.TBL_Ogrenciler.ogrenciBolum == akademisyen.akaBolum && x.dosyaDurum == "0").OrderByDescending(x => x.dosyaTarihi).ToList();

                var degerlendirilmisDosyalar = db.TBL_Dosyalar.Where(x => x.stajTuru == akademisyen.akaStajTuru && x.TBL_Ogrenciler.ogrenciBolum == akademisyen.akaBolum && x.dosyaDurum != "0").OrderByDescending(x => x.dosyaTarihi).ToList();

                myModel.degerlendirilmis = degerlendirilmisDosyalar;
                myModel.degerlendirilmemis = degerlendirilmemisDosyalar;
                return View(myModel);
            }
            else if(LoginController.aktifOgr != null && LoginController.aktifAka == null)
            {
                return RedirectToAction("/Index", "Home");
            }
            else
            {
                return RedirectToAction("/Login", "Akademisyen");
            }
        }

        [AllowAnonymous]
        public ActionResult Login()
        {
            if(LoginController.aktifOgr != null)
            {
                return RedirectToAction("/Index", "Home");
            }
            else if(LoginController.aktifAka != null)
            {
                return RedirectToAction("/Index", "Akademisyen");
            }
            else
            {
                return View();

            }
            
        }

        public ActionResult DosyaOnayla(int id)
        {
            var dosya = db.TBL_Dosyalar.Find(id);
            dosya.dosyaDurum = "1";
            dosya.dosyaDegerlendiren = LoginController.aktifAka.akaID;

            TBL_Bildirimler bildirim = new TBL_Bildirimler();
            bildirim.bildirimAlan = dosya.dosyaGonderen;
            bildirim.bildirimGonderen = dosya.dosyaDegerlendiren;
            bildirim.bildirimTarih = DateTime.Now;
            bildirim.bildirimIcerik = dosya.dosyaAdi.ToString() + " adlı dosya, " + LoginController.aktifAka.akaAdi + " " + LoginController.aktifAka.akaSoyadi + " tarafından onaylandı.";
            bildirim.bildirimOkunduMu = false;
            db.TBL_Bildirimler.Add(bildirim);

            db.SaveChanges();
            TempData["fileState"] = "onay";
            return RedirectToAction("/Index");
        }

        public ActionResult DosyaReddet(int id)
        {
            var dosya = db.TBL_Dosyalar.Find(id);
            dosya.dosyaDurum = "2";
            dosya.dosyaDegerlendiren = LoginController.aktifAka.akaID;

            TBL_Bildirimler bildirim = new TBL_Bildirimler();
            bildirim.bildirimAlan = dosya.dosyaGonderen;
            bildirim.bildirimGonderen = dosya.dosyaDegerlendiren;
            bildirim.bildirimTarih = DateTime.Now;
            bildirim.bildirimIcerik = dosya.dosyaAdi.ToString() + " adlı dosya, " + LoginController.aktifAka.akaAdi + " " + LoginController.aktifAka.akaSoyadi + " tarafından reddedildi.";
            bildirim.bildirimOkunduMu = false;
            db.TBL_Bildirimler.Add(bildirim);

            db.SaveChanges();
            TempData["fileState"] = "red";
            return RedirectToAction("/Index");
        }

        public ActionResult DosyaBeklet(int id)
        {
            var dosya = db.TBL_Dosyalar.Find(id);
            dosya.dosyaDurum = "0";
            dosya.dosyaDegerlendiren = null;

            TBL_Bildirimler bildirim = new TBL_Bildirimler();
            bildirim.bildirimAlan = dosya.dosyaGonderen;
            bildirim.bildirimGonderen = LoginController.aktifAka.akaID;
            bildirim.bildirimTarih = DateTime.Now;
            bildirim.bildirimIcerik = dosya.dosyaAdi.ToString() + " adlı dosya, " + LoginController.aktifAka.akaAdi + " " + LoginController.aktifAka.akaSoyadi + " tarafından beklemeye alındı.";
            bildirim.bildirimOkunduMu = false;
            db.TBL_Bildirimler.Add(bildirim);

            db.SaveChanges();
            TempData["fileState"] = "bekletme";
            return RedirectToAction("/Index");
        }

        public JsonResult AciklamaGetir(int id)
        {
            var dosya = db.TBL_Dosyalar.Find(id);
            string aciklama = dosya.dosyaAciklama;
            return Json(aciklama, JsonRequestBehavior.AllowGet);
        }
    }
}