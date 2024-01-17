using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Car_Rental.Data;
using Car_Rental.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;

namespace Car_Rental.Controllers

{
    
    public class OrderController : Controller
    {
        private readonly IOrder orderRep;
        private readonly IHttpContextAccessor _httpContextAccessor;


        public OrderController(IOrder orderRep, IHttpContextAccessor httpContextAccessor)
        {
            this.orderRep = orderRep;
            _httpContextAccessor = httpContextAccessor;
        }

        // GET: Order
        public ActionResult Index()
        {
            return View(orderRep.GetAll());
        }
        public ActionResult DisplayOrders()
        {
            var currentUserEmail = HttpContext.Session.GetString("UserId");
            var isLoggedIn = !string.IsNullOrEmpty(currentUserEmail);

            if (isLoggedIn)
            {
                
                var orderViewModels = orderRep.DisplayOrders(); 
                return View(orderViewModels);
            }

            
            return RedirectToAction("Login"); 
        }


        // GET: Order/Details/5
        public ActionResult Details(int id)
        {
            return View(orderRep.GetById(id));
        }

        // GET: Order/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Order/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Order order)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    orderRep.Add(order);
                    return RedirectToAction(nameof(Index));
                }

                foreach (var modelState in ModelState.Values)
                {
                    foreach (var error in modelState.Errors)
                    {
                        Console.WriteLine($"ModelState Error: {error.ErrorMessage}");
                    }
                }

                return View(order);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
                return View(order);
            }
        }

        // GET: Order/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = orderRep.GetById(id.Value);

            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // POST: Order/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Order order)
        {
            if (id != order.OrderId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    orderRep.Update(order);
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception)
                {
                    return View();
                }
            }

            return View(order);
        }

        // GET: Order/Delete/5
        public ActionResult Delete(int id)
        {
            var order = orderRep.GetById(id);

            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // POST: Order/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var order = orderRep.GetById(id);

            if (order != null)
            {
                orderRep.Delete(order);
                return RedirectToAction(nameof(Index));
            }

            return NotFound();
        }
    }
}
