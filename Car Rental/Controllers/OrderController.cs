using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Car_Rental.Data;
using Car_Rental.Models;
using Microsoft.AspNetCore.Http;

namespace Car_Rental.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrder orderRep;
        private readonly IUser userRep;
        private readonly ICar carRep;
        

        public OrderController(IOrder orderRep,  IUser userRep, ICar carRep)
        {
            this.orderRep = orderRep;
            this.userRep = userRep;
            this.carRep = carRep;
        }

       


        public ActionResult DisplayOrders()
        {
            var currentUserEmail = HttpContext.Session.GetString("Email");
            var isLoggedIn = !string.IsNullOrEmpty(currentUserEmail);

            if (isLoggedIn)
            {
                var orderViewModels = orderRep.DisplayOrders(currentUserEmail);
                return View(orderViewModels);
            }

            return RedirectToAction("Login", "User");
        }

        // GET: Order/Details/5
        public ActionResult Details(int id)
        {
            return View(orderRep.GetById(id));
        }

        // GET: Order/Create
        public ActionResult Create(int carId)
        {
            
            var currentUserId = HttpContext.Session.GetInt32("UserId");
            
            

            if (currentUserId != null)
            {

                var car = carRep.GetById(carId);
                var orderCreateVM = new OrderCreateVM(carId);

                return View();
            }

            return RedirectToAction("Login", "User");
        }

        // POST: Order/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(OrderCreateVM orderCreateVM)
        {
            
            int currentUserId = HttpContext.Session.GetInt32("UserId") ?? throw new Exception("User Id Null");
            

            if (currentUserId != null)
            {
                    if (ModelState.IsValid)
                    {
                        try
                        {
                            var car = carRep.GetById(orderCreateVM.CarId);
                            var user = userRep.GetById(currentUserId); 
                            var order = new Order
                            {
                                Car = car,
                                User = user,
                                StartDate = orderCreateVM.StartDate,
                                EndDate = orderCreateVM.EndDate
                            };

                            orderRep.Add(order);

                            return RedirectToAction("DisplayOrders");
                        }
                        catch (Exception ex)
                        {
                           
                            return RedirectToAction("Error");
                        }
                    }

     
                    
                

                
                return RedirectToAction("Login", "User");
            }

            
            return RedirectToAction("Login", "User");
        }



        //// GET: Order/Edit/5
        //public ActionResult Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var order = orderRep.GetById(id.Value);

        //    if (order == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(order);
        //}

        //// POST: Order/Edit/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit(int id, Order order)
        //{
        //    if (id != order.OrderId)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            orderRep.Update(order);
        //            return RedirectToAction(nameof(Index));
        //        }
        //        catch (Exception)
        //        {
        //            return View();
        //        }
        //    }

        //    return View(order);
        //}

        //// GET: Order/Delete/5
        //public ActionResult Delete(int id)
        //{
        //    var order = orderRep.GetById(id);

        //    if (order == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(order);
        //}

        //// POST: Order/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    var order = orderRep.GetById(id);

        //    if (order != null)
        //    {
        //        orderRep.Delete(order);
        //        return RedirectToAction(nameof(Index));
        //    }

        //    return NotFound();
        //}

       
    }
}
