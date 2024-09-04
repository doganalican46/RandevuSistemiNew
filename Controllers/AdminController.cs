using RandevuSistemiNew.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RandevuSistemiNew.Controllers
{
    public class AdminController : Controller
    {
        RandevuSistemiEntities db = new RandevuSistemiEntities();
        // GET: Admin
        //ana sayfa için istatistikler ekle : aktif randevu sayısı, toplam randevu sayısı, toplam doktor ve hasta sayısı 
        //en çok ziyaret edilen doktor en çok çalışan doktor  en çok randevu olan gün
        //rapor çıkart düğmesine basınca pdf e çevir rapor halinde masaüstüne kaydet
        //  ayarlar sayfası ayarla giriş yapan kullanıcı bilgisi ile ayarlansın

        public ActionResult Index()
        {
            return View();
        }
        private List<SelectListItem> GetCategoryList()
        {
            return db.Bolumler.Where(k => k.Status == true)
                              .Select(i => new SelectListItem
                              {
                                  Text = i.BolumAd,
                                  Value = i.ID.ToString()
                              }).ToList();
        }

        private List<SelectListItem> GetDoktorList()
        {
            return db.Users.Where(k => k.Status == true && k.Role == "doktor")
                           .Select(i => new SelectListItem
                           {
                               Text = i.Fullname,
                               Value = i.ID.ToString()
                           }).ToList();
        }

        private List<SelectListItem> GetHemsireList()
        {
            return db.Users.Where(k => k.Status == true && k.Role == "hemsire")
                           .Select(i => new SelectListItem
                           {
                               Text = i.Fullname,
                               Value = i.ID.ToString()
                           }).ToList();
        }

        public ActionResult Ayarlar()
        {
            return View();
        }









    }
}