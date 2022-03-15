using GeneralStore.Models.TransactionModels;
using GeneralStore.Services;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GeneralStoreMVC.Controllers
{
    public class TransactionController : Controller
    {
        private TransactionService CreateTransactionService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var ts = new TransactionService(userId);
            return ts;
        }
        private CustomerService CreateCustomerService()
        {
            Guid userId = Guid.Parse(User.Identity.GetUserId());
            CustomerService cs = new CustomerService(userId);
            return cs;
        }
        private ProductService CreateProductService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var productService = new ProductService(userId);
            return productService;
        }


        // GET: Transaction
        public ActionResult Index()
        {
            return View();
        }

        // GET: /Transaction/Cretae
        [HttpGet]
        public ActionResult Create()
        {
            //Get list of customers for dropdown:
            var cs = CreateCustomerService();
            var ps = CreateProductService();
            //var allCustomers = cs.GetAllCustomers();
            ViewBag.CustomerItems = cs.GetAllCustomers().Select(customer => new SelectListItem
            {
                Text = customer.FirstName + " " + customer.LastName,
                Value = customer.CustomerId.ToString()
            });
            ViewBag.ProductItems = new SelectList(ps.GetAllProducts(), "ProductID", "Name");
            return View(new TransactionCreate());
        }

        //POST: /Transaction/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TransactionCreate model)
        {
            return View();
        }
    }
}