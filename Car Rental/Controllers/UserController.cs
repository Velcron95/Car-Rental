using System;
using System.Linq;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Car_Rental.Models;
using Car_Rental.Data;

namespace Car_Rental.Controllers
{
    public class UserController : Controller
    {
        private readonly IUser userRep;
        private readonly ApplicationDbContext applicationDbContext;


        public UserController(IUser userRep, ApplicationDbContext applicationDbContext)
        {
            this.userRep = userRep;
            this.applicationDbContext = applicationDbContext;
        }


        // GET: User/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: User/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(User user)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    userRep.Add(user);
                    return RedirectToAction("Login","User");
                }

                foreach (var modelState in ModelState.Values)
                {
                    foreach (var error in modelState.Errors)
                    {
                        Console.WriteLine($"ModelState Error: {error.ErrorMessage}");
                    }
                }

                
                return View(user);
            }
            catch (Exception ex)
            {
                
                Console.WriteLine($"Exception: {ex.Message}");
                return View(user);
            }
        }

       
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (IsValidUser(model.Email, model.Password, out var user))
                {
                    HttpContext.Session.SetInt32("UserId", user.UserId);
                    HttpContext.Session.SetString("Email", model.Email);
                    HttpContext.Session.SetString("IsAdmin", user.IsAdmin.ToString());
                    if(user.IsAdmin == true)
                    {
                        return RedirectToAction("index", "Car");
                    }

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Invalid username or password");
                }
            }

            return View(model);
        }

        private bool IsValidUser(string username, string password, out User user)
        {
            user = applicationDbContext.User.FirstOrDefault(u => u.Email == username && u.Password == password);

            return user != null;
        }
        [HttpPost]
        public ActionResult Logout()
        {
            
            HttpContext.Session.Remove("Email");
            HttpContext.Session.Remove("IsAdmin");
            Response.Cookies.Delete("Email");

            return RedirectToAction("Index", "Home");
        }



    }
}
