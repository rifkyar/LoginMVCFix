using Login.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BCrypt;

namespace Login.Controllers
{
    public class UserController : Controller
    {
        ApplicationDbContext myContext = new ApplicationDbContext();
        // GET: User
        public ActionResult Index()
        {
            var list = myContext.Users.ToList();
            return View(list);
        }

        // GET: User/Details/5
        public ActionResult Details(int id)
        {
            var detail = myContext.Users.Find(id);
            return View(detail);
        }

        // GET: User/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: User/Create
        [HttpPost]
        public ActionResult Create(User user)
        {
            try
            {
                // TODO: Add insert logic here
                var mySalt = BCrypt.Net.BCrypt.GenerateSalt();
                user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password, mySalt);
                myContext.Users.Add(user);
                myContext.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: User/Edit/5
        public ActionResult Edit(int id)
        {
            var edit = myContext.Users.Find(id);
            return View();
        }

        // POST: User/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, User user)
        {
            try
            {
                // TODO: Add update logic here
                var edit = myContext.Users.Find(id);
                edit.Email = user.Email;
                edit.Password = user.Password;
                myContext.Entry(edit).State = System.Data.Entity.EntityState.Modified;
                myContext.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: User/Delete/5
        public ActionResult Delete(int id)
        {
            var delete = myContext.Users.Find(id);
            return View();
        }

        // POST: User/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
                var delete = myContext.Users.Find(id);
                myContext.Users.Remove(delete);
                myContext.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(User user)
        {
            var log = myContext.Users.FirstOrDefault(e => e.Email.Equals(user.Email));
            bool tes = Hash.ValidatePassword(user.Password, log.Password);
            if (tes == true)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("Login");
            }
        }
        public ActionResult Logout()
        {
            Session.Remove("id");
            Session.Remove("Email");
            return RedirectToAction("Index", "Login");
        }
    }
}
