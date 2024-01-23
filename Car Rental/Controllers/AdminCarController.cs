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
    public class AdminCarController : Controller
    {
        private readonly ICar carRep;

        public AdminCarController(ICar carRep)
        {
            this.carRep = carRep;
        }




        // GET: AdminCar
        public IActionResult Index()
        {
            var cars = carRep.GetAll();
            return View(cars);
        }

        // GET: AdminCar/Details/5
        public IActionResult Details(int id)
        {
            return View(carRep.GetById(id));
        }

        // GET: AdminCar/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: AdminCar/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Car car)
        {
            if (ModelState.IsValid)
            {
                carRep.Add(car);
                return RedirectToAction(nameof(Index));
            }
            return View(car);
        }

        // GET: AdminCar/Edit/5
        public IActionResult Edit(int id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var car = carRep.GetById(id);

            return View(car);
        }

        // POST: AdminCar/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Car car)
        {
            if (id != car.CarId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    carRep.Update(car);
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception)
                {
                    return View();
                }
            }
            return View(car);
        }

        // GET: AdminCar/Delete/5
        public IActionResult Delete(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var car = carRep.GetById(id);

            if (car == null)
            {
                return NotFound();
            }

            return View(car);
        }

        // POST: AdminCar/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var car = carRep.GetById(id);

            if (car != null)
            {
                carRep.Delete(car);
                return RedirectToAction(nameof(Index));
            }

            return NotFound();
        }
    }
}
