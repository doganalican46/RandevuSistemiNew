using RandevuSistemiNew.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RandevuSistemiNew.Controllers
{
    public class RandevuController : Controller
    {
        RandevuSistemiEntities db = new RandevuSistemiEntities();
        //Randevular sayfasında aktif randevu için hatıratma maili gönder
        //Randevu oluştuğunda mail gönder
        //Randevu update edildiğinde ve silindiğinde de bildirim gönder

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

        //---------------------Randevular Started----------------------//
        //[Authorize]
        public ActionResult Randevular()
        {
            var randevular = db.Randevular.ToList();
            var currentDateTime = DateTime.Now;

            foreach (var randevu in randevular)
            {
                if (randevu.RandevuTarih < currentDateTime && randevu.Status == true)
                {
                    randevu.Status = false;
                    db.Entry(randevu).State = System.Data.Entity.EntityState.Modified;
                }
            }

            db.SaveChanges();
            return View(randevular);
        }


        //[Authorize]
        [HttpGet]
        public ActionResult YeniRandevular()
        {
            ViewBag.Category = GetCategoryList();
            ViewBag.Doktor = GetDoktorList();
            return View();
        }

        //[Authorize]
        [HttpPost]
        public ActionResult YeniRandevular(Randevular y, int selectedCategoryID, int selectedDoktorID)
        {
            ViewBag.Category = GetCategoryList();
            ViewBag.Doktor = GetDoktorList();

            y.BolumID = selectedCategoryID;
            y.DoktorID = selectedDoktorID;

            db.Randevular.Add(y);
            db.SaveChanges();
            return RedirectToAction("Randevular");
        }

        //[Authorize]
        public ActionResult RandevularSil(int id)
        {
            var randevular = db.Randevular.Find(id);
            if (randevular != null)
            {
                randevular.Status = false;
                db.SaveChanges();
            }
            return RedirectToAction("Randevular");
        }

        //[Authorize]
        public ActionResult RandevularGetir(int id)
        {
            var randevu = db.Randevular.Find(id);
            if (randevu == null)
            {
                return HttpNotFound();
            }

            List<SelectListItem> Category = db.Bolumler
                                              .Where(k => k.Status == true)
                                              .Select(i => new SelectListItem
                                              {
                                                  Text = i.BolumAd,
                                                  Value = i.ID.ToString(),
                                                  Selected = (i.ID == randevu.BolumID)
                                              }).ToList();

            List<SelectListItem> Doktor = db.Users
                                            .Where(k => k.Status == true && k.Role == "doktor" && k.BolumID == randevu.BolumID)
                                            .Select(i => new SelectListItem
                                            {
                                                Text = i.Fullname,
                                                Value = i.ID.ToString(),
                                                Selected = (i.ID == randevu.DoktorID)
                                            }).ToList();

            ViewBag.Category = Category;
            ViewBag.Doktor = Doktor;

            return View(randevu);
        }




        //[Authorize
        [HttpPost]
        public ActionResult RandevularGuncelle(Randevular y)
        {
            if (ModelState.IsValid)
            {
                var Randevular = db.Randevular.Find(y.ID);
                if (Randevular != null)
                {
                    Randevular.HastaAd = y.HastaAd;
                    Randevular.HastaMail = y.HastaMail;
                    Randevular.HastaPhone = y.HastaPhone;
                    Randevular.BolumID = y.BolumID;
                    Randevular.DoktorID = y.DoktorID;
                    Randevular.RandevuTarih = y.RandevuTarih;

                    Randevular.Status = y.Status;

                    db.SaveChanges();
                    return RedirectToAction("Randevular");
                }
                return HttpNotFound();
            }

            ViewBag.Category = GetCategoryList();
            ViewBag.Doktor = GetDoktorList();

            return View("RandevularGetir", y);
        }

        public JsonResult GetDoktorsByCategory(int categoryID)
        {
            var doktors = db.Users
                            .Where(k => k.Status == true && k.Role == "doktor" && k.BolumID == categoryID)
                            .Select(i => new SelectListItem
                            {
                                Text = i.Fullname,
                                Value = i.ID.ToString()
                            }).ToList();

            return Json(doktors, JsonRequestBehavior.AllowGet);
        }



        //---------------------Randevular End----------------------//




    }
}