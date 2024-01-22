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

        public ActionResult Index()
        {
            return View(userRep.GetAll());
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
                    return RedirectToAction(nameof(Index));
                }

                // Log or inspect ModelState errors
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

        //Get
            public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (IsValidUser(model.Email, model.Password,out var user))
                {
                    HttpContext.Session.SetInt32("UserId", user.UserId);
                    HttpContext.Session.SetString("Email", model.Email);
                    HttpContext.Session.SetInt32("IsAdmin", Convert.ToInt32(user.IsAdmin));
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
            user = applicationDbContext.User.FirstOrDefault(u => u.Email == username);

            return user != null && user.Password == password;
        }
        [HttpPost]
        public ActionResult Logout()
        {
            // Remove the session key
            HttpContext.Session.Remove("Email");

            // Optionally, remove the cookie (if you have set a cookie during login)
            Response.Cookies.Delete("Email");

            return RedirectToAction("Index", "Home");
        }



    }
}
