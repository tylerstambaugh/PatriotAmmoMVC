using GeneralStore.Data;
using GeneralStore.Models;
using GeneralStore.Models.ProductModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.ModelBinding;

namespace GeneralStore.Services
{
    public class ProductService
    {
        private readonly Guid _userId;
        public ProductService(Guid userId)
        {
            _userId = userId;
        }

        //Get list of all products
        public IEnumerable<ProductDetail> GetAllProducts()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var plm = ctx.Products.Select(p =>
                new ProductDetail
                {
                    ProductId = p.ProductId,
                    Name = p.Name,
                    InventoryCount = p.InventoryCount,
                    IsExplosive = p.IsExplosive

                });
                
                return plm.ToList();
            }
        }

        //Get Product by ID
        public ProductDetail GetProductById(int? id)
        {
            if (id != null)
            {
                using (var ctx = new ApplicationDbContext())
                {
                    Product productToReturn = ctx.Products.Find(id);
                    if (productToReturn != null)
                        return new ProductDetail
                        {
                            ProductId = productToReturn.ProductId,
                            Name = productToReturn.Name,
                            InventoryCount = productToReturn.InventoryCount,
                            IsExplosive = productToReturn.IsExplosive
                        };
                    else
                        return null;
                }
            }
            else
                return null;
        }

        //Create a product
        public bool CreateAProduct(ProductCreate model)
        {
            if (model != null)
            {
                using (var ctx = new ApplicationDbContext())
                {
                    Product productToAdd = new Product
                    {
                        Name = model.Name,
                        InventoryCount = model.InventoryCount,
                        IsExplosive = model.IsExplosive
                    };
                    ctx.Products.Add(productToAdd);
                    if(ctx.SaveChanges() == 1)
                        return(true);
                }
            }
                return false;
        }

        //Delete a product
        public bool DeleteAProduct(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                Product productToDelete = ctx.Products.Find(id);
                ctx.Products.Remove(productToDelete);
                if (ctx.SaveChanges() == 1)
                    return true;
                return false;
            }
        }

        //Edit a product
        public bool EditAProduct(int id, ProductEdit pe)
        {
            using (var ctx = new ApplicationDbContext())
            {
                Product productToUpdate = ctx.Products.Find(id);
                if (productToUpdate != null)
                {
                    productToUpdate.Name = pe.Name;
                    productToUpdate.InventoryCount = pe.InventoryCount;
                    productToUpdate.IsExplosive = pe.IsExplosive;
                }
                return ctx.SaveChanges() == 1;
            }
        }
    }
}
