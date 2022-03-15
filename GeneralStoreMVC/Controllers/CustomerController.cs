using GeneralStore.Models.CustomerModels;
using GeneralStore.Services;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GeneralStoreMVC.Controllers
{
    [Authorize]
    public class CustomerController : Controller
    {
        private CustomerService CreateCustomerService()
        {
            Guid userId = Guid.Parse(User.Identity.GetUserId());
            CustomerService cs = new CustomerService(userId);
            return cs;
        }

        // GET: Customer
        [HttpGet]
        public ActionResult Index()
        {
            CustomerService cs = CreateCustomerService();
            var listOfCustomers = cs.GetAllCustomers();  
            return View(listOfCustomers);
        }

        //GET: Customer/Create
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        //POST: /Customer/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CustomerCreate model)
        {
            if (ModelState.IsValid)
            {
                var cs = CreateCustomerService();
                if (cs.CreateACustomer(model)) ;
                return RedirectToAction("Index");
            }
            return View(model);
        }

        //GET: /Customer/Detail/{id}
        [HttpGet]
        public ActionResult Detail(int id)
        {
            var cs = CreateCustomerService();
            var cd = cs.GetCustomerById(id);
            if (cd == null)
                return HttpNotFound();
            return View(cd);
        }

        //GET: Customer/Delete/{id}
        [HttpGet]
        public ActionResult Delete(int? id)
        {
            if(id == null)
                {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
                }
            var cs = CreateCustomerService();
            CustomerDetail cd = cs.GetCustomerById(id);
            if(cd == null)
            {
                return HttpNotFound();
            }
            return View(cd);
        }

        //POST: /Customer/Delete/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int id)
        {
            var cs = CreateCustomerService();
            if (cs.DeleteACustomer(id))
                return RedirectToAction("Index");
            return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
        }

        //GET: /Customer/Edit{id}
        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(400);
            }
            var cs = CreateCustomerService();
            CustomerDetail cd = cs.GetCustomerById(id);
            return View();
        }

        //POST: /Customer/Edit{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, CustomerEdit model)
        {
            if(ModelState.IsValid)
            {
                var cs = CreateCustomerService();
                if(cs.UpdateACustomer(id, model));
                return RedirectToAction("Index");
            }
            return View(model);
        }
    }
}