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






        public ActionResult Doktorlar()
        {
            return View();
        }



        

        //---------------------Users End----------------------//


    }
}