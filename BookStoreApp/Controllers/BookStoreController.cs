using BookStoreApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BookStoreApp.Controllers
{
    public class BookStoreController : Controller
    {
        // GET: BookStore
        BookStoreDb db = new BookStoreDb();
        private List<SACH> LaySachMoi(int count)
        {
            return db.SACHes.OrderByDescending(a => a.Ngaycapnhat).Take(count).ToList();
        }
        public ActionResult Index()
        {
            var sachmoi = LaySachMoi(5);
            return View(sachmoi);
        }
        public ActionResult Details(int id)
        {
            var sach = from s in db.SACHes
                       where s.Masach == id
                       select s;
            return View(sach.Single());
        }
    }
}