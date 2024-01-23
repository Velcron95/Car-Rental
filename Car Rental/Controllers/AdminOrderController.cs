using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Car_Rental.Data;
using Car_Rental.Models;

namespace Car_Rental.Controllers
{
    public class AdminOrderController : Controller
    {
        private readonly IOrder orderRep;

        public AdminOrderController(IOrder orderRep)
        {
            this.orderRep = orderRep;
        }

        // GET: AdminOrder
        public IActionResult Index()
        {
            var orders = orderRep.GetAll();
            return View(orders);
        }

        // GET: AdminOrder/Details/5
        public IActionResult Details(int id)
        {
            return View(orderRep.GetById(id));
        }

        // GET: AdminOrder/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: AdminOrder/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Order order)
        {
            if (ModelState.IsValid)
            {
                orderRep.Add(order);
                return RedirectToAction(nameof(Index));
            }
            return View(order);
        }

        // GET: AdminOrder/Edit/5
        public IActionResult Edit(int id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var order = orderRep.GetById(id);

            return View(order);
        }

        // POST: AdminOrder/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Order order)
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

        // GET: AdminOrder/Delete/5
        public IActionResult Delete(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = orderRep.GetById(id);

            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // POST: AdminOrder/Delete/5
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
