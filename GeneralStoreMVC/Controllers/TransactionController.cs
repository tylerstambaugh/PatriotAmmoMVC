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
            var cs = CreateCustomerService();
            var ps = CreateProductService();
            TransactionService ts = CreateTransactionService();
            var transactions = ts.GetAllTransactions();
            return View(transactions);
        }

        // GET: /Transaction/Cretae
        [HttpGet]
        public ActionResult Create()
        {

            //Get list of customers for dropdown:
            var cs = CreateCustomerService();
            var ps = CreateProductService();
            //var allCustomers = cs.GetAllCustomers();
            //ViewBag.CustomerItems = cs.GetAllCustomers().Select(customer => new SelectListItem
            //{
            //    Text = customer.FirstName + " " + customer.LastName,
            //    Value = customer.CustomerId.ToString()
            //});
           
            //ViewBag.ProductItems = new SelectList(ps.GetAllProducts(), "ProductID", "Name");

            ViewData["Products"] = ps.GetAllProducts().Select(product => new SelectListItem
            {
                Text = product.Name,
                Value = product.ProductId.ToString()
            });

            ViewData["Customers"] = cs.GetAllCustomers().Select(customer => new SelectListItem
            {
                Text = $"{customer.FirstName}",
                Value = customer.CustomerId.ToString()
            });


            return View(new TransactionCreate());
        }

        //POST: /Transaction/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TransactionCreate model)
        {
            var cs = CreateCustomerService();
            var ps = CreateProductService();

            ViewData["Products"] = ps.GetAllProducts().Select(product => new SelectListItem
            {
                Text = product.Name,
                Value = product.ProductId.ToString()
            });

            ViewData["Customers"] = cs.GetAllCustomers().Select(customer => new SelectListItem
            {
                Text = $"{customer.FirstName}",
                Value = customer.CustomerId.ToString()
            });
            //find customer and product id in database before creating

            //ViewData{"Error"] = "invalid product Id"
            TransactionService ts = CreateTransactionService();
            if(ts.CreateTransaction(model))
            return RedirectToAction("Index");

            return View(model);
        }


        //GET /Transaction/Edit/{id}
        [HttpGet]
        public ActionResult Edit(int id)
        {
            //validate the id

            //populate the dropdowns
            var cs = CreateCustomerService();
            var ps = CreateProductService();

            ViewData["Products"] = ps.GetAllProducts().Select(product => new SelectListItem
            {
                Text = product.Name,
                Value = product.ProductId.ToString()
            });

            ViewData["Customers"] = cs.GetAllCustomers().Select(customer => new SelectListItem
            {
                Text = $"{customer.FirstName}",
                Value = customer.CustomerId.ToString()
            });

            var ts = CreateTransactionService();
            TransactionDetail td = ts.GetTransactionById(id);
            return View(td);
        }
        //GET /Transaction/Edit
        [HttpPost]
        public ActionResult Edit(int id, TransactionEdit model)
        {
            //validate the id

            //populate the dropdowns
            var cs = CreateCustomerService();
            var ps = CreateProductService();

            ViewData["Products"] = ps.GetAllProducts().Select(product => new SelectListItem
            {
                Text = product.Name,
                Value = product.ProductId.ToString()
            });

            ViewData["Customers"] = cs.GetAllCustomers().Select(customer => new SelectListItem
            {
                Text = $"{customer.FirstName}",
                Value = customer.CustomerId.ToString()
            });

            TransactionService ts = CreateTransactionService();
            if (ts.EditTransaction(id, model))
                return RedirectToAction("Index");

            return View(model);
        }
    }
}