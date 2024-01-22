using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Car_Rental.Data;
using Car_Rental.Models;
using System.Runtime.ConstrainedExecution;

namespace Car_Rental.Controllers
{
    public class AdminUserController : Controller
    {
        private readonly IUser userRep;
        

        public AdminUserController(IUser userRep)
        {
            this.userRep = userRep;
            
        }

        // GET: AdminUser
        public IActionResult Index()
        {
            return View();
        }

        // GET: AdminUser/Details/5
        public IActionResult Details(int id)
        {
            return View(userRep.GetById(id));
        }

        // GET: AdminUser/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: AdminUser/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("UserId,Email,Password,IsAdmin")] User user)
        {
            if (ModelState.IsValid)
            {
                userRep.Add(user);
                return RedirectToAction(nameof(Index));
            }
            return View(user);
        }

        // GET: AdminUser/Edit/5
        public IActionResult Edit(int id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var user = userRep.GetById(id);
           
            return View(user);
        }

        // POST: AdminUser/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("UserId,Email,Password,IsAdmin")] User user)
        {
            if (id != user.UserId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    userRep.Update(user);
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception)
                {
                    return View();
                }
            }
            return View(user);
        }

        // GET: AdminUser/Delete/5
        public IActionResult Delete(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = userRep.GetById(id);

            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // POST: AdminUser/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var user = userRep.GetById(id);

            if (user != null)
            {
                userRep.Delete(user);
                return RedirectToAction(nameof(Index));
            }

            return NotFound();
        }
    }
}
