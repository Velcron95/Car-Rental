using System;
using System.Linq;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Car_Rental.Models;
using Car_Rental.Data;

namespace Car_Rental.Controllers
{
    public class CustomerController : Controller
    {
        private readonly ICustomer customerRep;
        private readonly ApplicationDbContext applicationDbContext;


        public CustomerController(ICustomer customerRep, ApplicationDbContext applicationDbContext)
        {
            this.customerRep = customerRep;
            this.applicationDbContext = applicationDbContext;
        }

        public ActionResult Index()
        {
            return View(customerRep.GetAll());
        }
        // GET: CarController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CarController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Customer customer)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    customerRep.Add(customer);
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

                
                return View(customer);
            }
            catch (Exception ex)
            {
                
                Console.WriteLine($"Exception: {ex.Message}");
                return View(customer);
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
                if (IsValidUser(model.Email, model.Password))
                {
                    HttpContext.Session.SetString("UserId", model.Email);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Invalid username or password");
                }
            }
            return View(model);
        }

        private bool IsValidUser(string username, string password)
        {
            var user = applicationDbContext.Customer.FirstOrDefault(u => u.Email == username);

            if (user != null && user.Password == password)
            {
                HttpContext.Session.SetString("IsAdmin", user.IsAdmin.ToString());
                return true;
            }

            return false;
        }
        [HttpPost]
        public ActionResult Logout()
        {
            // Remove the session key
            HttpContext.Session.Remove("UserId");

            // Optionally, remove the cookie (if you have set a cookie during login)
            Response.Cookies.Delete("UserId");

            return RedirectToAction("Index", "Home");
        }



    }
}
