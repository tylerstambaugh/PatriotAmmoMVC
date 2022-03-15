using GeneralStore.Data;
using GeneralStore.Models;
using GeneralStore.Models.ProductModels;
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
    public class ProductController : Controller
    {

        private ProductService CreateProductService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var productService = new ProductService(userId);
            return productService;
        }

        // GET: Product
        [HttpGet]
        public ActionResult Index()
        {
            var ps = CreateProductService();
           var allProducts = ps.GetAllProducts();
            var orderedList = allProducts.OrderBy(p => p.Name).ToList();
            return View(allProducts);
        }

        // Get: Product/Create
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        //POST: Product/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ProductCreate model)
        {
            if(ModelState.IsValid)
            {
                var ps = CreateProductService();
                if(ps.CreateAProduct(model));
                return RedirectToAction("Index");
            }
            ViewData["ModelStateError"] = "The ProductCreate Model is Invalid.";
            return View(model);
        }

        // GET: Delete
        //Product/Delete/{id}
       // [ValidateAntiForgeryToken]
        [HttpGet]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
            var ps = CreateProductService();
            ProductDetail pd = ps.GetProductById(id);
            if (pd == null)
            {
                return HttpNotFound();
            }
            return View(pd);
        }

        //POST: Delete
        //Product/Delete/{id}
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int id)
        {
            var ps = CreateProductService();
            if(ps.DeleteAProduct(id))
                return RedirectToAction("Index");
            return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);

        }

        // GET: Edit
        //Product/Edit/{id}
        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(400);
            }
            var ps = CreateProductService();
            ProductDetail pd = ps.GetProductById(id);

            return View(pd);
        }

        //PTO: Edit
        //Product/Edit/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, ProductEdit pe)
        {
            if (ModelState.IsValid)
            {
                var ps = CreateProductService();
                bool wasSuccess = ps.EditAProduct(id, pe);
                if (wasSuccess)
                    return RedirectToAction("Index");
                
            }
            return View(pe);
        }

    }
}