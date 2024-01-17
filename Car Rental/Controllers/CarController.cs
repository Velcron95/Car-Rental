using Car_Rental.Data;
using Car_Rental.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Car_Rental.Controllers
{
    public class CarController : Controller
    {
        private readonly ICar carRep;

        public CarController(ICar carRep)
        {
            this.carRep = carRep;
        }

        // GET: CarController
        public IActionResult Index()
        {
            return View(carRep.GetAll());
        }

        // GET: CarController/Details/5
        public ActionResult Details(int id)
        {
            return View(carRep.GetById(id));
        }

        // GET: CarController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CarController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Car car)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    carRep.Add(car);
                    return RedirectToAction(nameof(Index));
                }

                
                foreach (var modelState in ModelState.Values)
                {
                    foreach (var error in modelState.Errors)
                    {
                        Console.WriteLine($"ModelState Error: {error.ErrorMessage}");
                    }
                }

                // Return the view with errors if ModelState is not valid
                return View(car);
            }
            catch (Exception ex)
            {
                // Log or handle the exception
                Console.WriteLine($"Exception: {ex.Message}");
                return View(car);
            }
        }

        // GET: CarController/Edit/5
        public ActionResult Edit(int? id)
        {
            var isAdmin = Convert.ToBoolean(HttpContext.Session.GetString("IsAdmin"));
            if (!isAdmin)
            {
                return View("AccessDenied");
            }

            var car = carRep.GetById(id.Value);

            if (id == null)
            {
                return NotFound();
            }

            return View(car);
        }

        // POST: CarController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Car car)
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

        // GET: CarController/Delete/5
        public ActionResult Delete(int id)
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

        // POST: CarController/Delete/5
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
