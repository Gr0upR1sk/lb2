using lb2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace lb2.Controllers
{
    public class AdminController : Controller
    {        
        // GET: Home
        public ActionResult Index()
        {
            ViewBag.users = Users.users;
            return View();
        }
           
        // GET: Home/Create
        public ActionResult Create()
        {
            return View(new User());
        }

        // POST: Home/Create
        [HttpPost]
        public ActionResult Create(User user)
        {
            try
            {
                // TODO: Add insert logic here
                if (ModelState.IsValid)
                {
                    if (Users.ValidData(user)[0] == "0")
                    {
                        if (Users.Add(user))
                            return RedirectToAction("Index");
                        else
                            ModelState.AddModelError("login", "Ligin is exist");
                    }
                    else
                    {
                        ModelState.AddModelError(Users.ValidData(user)[0], Users.ValidData(user)[1]);
                        return View(user);
                    }
                }
                return View(user);
            }
            catch
            {
                return View(user);
            }
        }

        // GET: Home/Edit/5
        public ActionResult Edit(string l)
        {
            //ViewBag.login = l;
            return View(Users.users.Single(c=>c.login == l));
        }

        // POST: Home/Edit/5
        [HttpPost]
        public ActionResult Edit(User user)
        {
            try
            {
                // TODO: Add update logic here
                //user.login = ViewBag.Log;
                if (Users.ValidData(user)[0] == "0")
                {
                    Users.Update(user.login, user.password, user.fullName, user.email);
                    return RedirectToAction("Index");
                }
                else
                    ModelState.AddModelError(Users.ValidData(user)[0], Users.ValidData(user)[1]);
                return View(user);                
            }
            catch
            {
                return View(user);
            }
        }

        // GET: Home/Delete/5
        public ActionResult Delete(string l)
        {
            User user = Users.users.Single(c => c.login == l);
            return View(user);
        }

        // POST: Home/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(User user)
        {
            try
            {
                // TODO: Add delete logic here                                
                if (ModelState.IsValid)
                {
                    Users.Delete(user.login);
                    return RedirectToAction("Index");
                }
                else
                    return View(user);
            }
            catch
            {
                return View(user);
            }
        }
    }
}
