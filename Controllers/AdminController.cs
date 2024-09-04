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

        public ActionResult Mesajlar()
        {
            return View();
        }





        //---------------------Randevular End----------------------//

        public ActionResult Doktorlar()
        {
            return View();
        }



        //---------------------Users Started----------------------//
        //[Authorize
        public ActionResult Users()
        {
            var Users = db.Users.ToList();
            return View(Users);
        }
        //[Authorize
        [HttpGet]
        public ActionResult YeniUsers()
        {
            ViewBag.Category = GetCategoryList();
            return View();
        }
        //[Authorize
        [HttpPost]
        public ActionResult YeniUsers(Users y, int selectedCategoryID)
        {
            ViewBag.Category = GetCategoryList();
            y.BolumID = selectedCategoryID;
            db.Users.Add(y);
            db.SaveChanges();
            return RedirectToAction("Users");
        }

        //[Authorize
        public ActionResult UsersSil(int id)
        {
            var Users = db.Users.Find(id);
            Users.Status = false;
            db.SaveChanges();
            return RedirectToAction("Users");
        }
        //[Authorize
        public ActionResult UsersGetir(int id)
        {
            var Users = db.Users.Find(id);
            if (Users == null)
            {
                return HttpNotFound();
            }

            List<SelectListItem> Category = (from i in db.Bolumler.Where(k => k.Status == true).ToList()
                                             select new SelectListItem
                                             {
                                                 Text = i.BolumAd,
                                                 Value = i.ID.ToString()
                                             }).ToList();
            ViewBag.Category = Category;

            return View("UsersGetir", Users);
        }

        //[Authorize
        [HttpPost]
        public ActionResult UsersGuncelle(Users y)
        {
            if (ModelState.IsValid)
            {
                var Users = db.Users.Find(y.ID);
                if (Users != null)
                {
                    Users.Fullname = y.Fullname;
                    Users.Mail= y.Mail; 
                    Users.Phone = y.Phone;
                    Users.Role= y.Role;
                    Users.BolumID = y.BolumID; 
                    Users.Status = y.Status;

                    db.SaveChanges();
                    return RedirectToAction("Users");
                }
                return HttpNotFound();
            }

            ViewBag.Category = GetCategoryList();

            return View("UsersGetir", y);
        }

        //---------------------Users End----------------------//


    }
}