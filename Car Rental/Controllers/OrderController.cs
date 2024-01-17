using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Car_Rental.Data;
using Car_Rental.Models;

namespace Car_Rental.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrder orderRep;

        public OrderController(IOrder orderRep)
        {
            this.orderRep = orderRep;
        }

        // GET: Order
        public IActionResult Index()
        {
            return View(orderRep.GetAll());
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
