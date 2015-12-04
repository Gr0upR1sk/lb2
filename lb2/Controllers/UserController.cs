using lb2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace lb2.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        public ActionResult Index()
        {
            ViewBag.users = Users.users;
            return View();
        }

        
        // GET: User/Edit/5
        public ActionResult Edit()
        {
            return View(Users.users.Single(c=>c.login == Session["login"].ToString()));
        }

        // POST: User/Edit/5
        [HttpPost]
        public ActionResult Edit(User user)
        {
            try
            {
                // TODO: Add update logic here
                user.login = Session["login"].ToString();
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
    }
}
