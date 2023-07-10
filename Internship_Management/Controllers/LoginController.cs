using Internship_Management.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.UI.WebControls;

namespace Internship_Management.Controllers
{
    [AllowAnonymous]
    public class LoginController : Controller
    {

        internshipManagementEntities db = new internshipManagementEntities();
        public static TBL_Ogrenciler aktifOgr;
        public static TBL_Akademisyenler aktifAka;

        public ActionResult Index()
        {
            if(aktifOgr == null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("/Index", "Home");
            }
        }

        

        [HttpPost]
        public ActionResult Index(TBL_Ogrenciler ogr)
        {
            var isExist = db.TBL_Ogrenciler.Where(x => x.ogrenciNo == ogr.ogrenciNo && x.ogrenciSifre == ogr.ogrenciSifre).FirstOrDefault();

            if (isExist != null)
            {
                FormsAuthentication.SetAuthCookie(ogr.ogrenciNo, false);
                Session["ogrNo"] = ogr.ogrenciNo;
                var ogrenci = db.TBL_Ogrenciler.Where(x => x.ogrenciNo == ogr.ogrenciNo).FirstOrDefault();
                aktifOgr = ogrenci;
                return RedirectToAction("/Index", "Home");
            }
            else
            {
                TempData["wrongUser"] = "Kullanıcı adı veya şifre hatalı.";
                return RedirectToAction("/Index", "Login");
            }
        }

        [HttpPost]
        public ActionResult AkaLogin(TBL_Akademisyenler aka)
        {
            var isExist = db.TBL_Akademisyenler.Where(x => x.akaMail == aka.akaMail && x.akaSifre == aka.akaSifre).FirstOrDefault();

            if (isExist != null)
            {
                FormsAuthentication.SetAuthCookie(aka.akaMail, false);
                Session["akaMail"] = aka.akaMail;
                var akademisyen = db.TBL_Akademisyenler.Where(x => x.akaMail == aka.akaMail).FirstOrDefault();
                aktifAka = akademisyen;
                return RedirectToAction("/Index", "Akademisyen");
            }
            else
            {
                TempData["wrongUser"] = "Kullanıcı adı veya şifre hatalı.";
                return RedirectToAction("/Login", "Akademisyen");
            }
        }




        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            LoginController.aktifAka = null;
            LoginController.aktifOgr = null;
            return RedirectToAction("/Index", "Login");
        }

        public ActionResult AkaLogOut()
        {
            FormsAuthentication.SignOut();
            LoginController.aktifAka = null;
            LoginController.aktifOgr = null;
            return RedirectToAction("/Login", "Akademisyen");
        }
    }
}