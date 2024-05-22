using OnlineStoreModel.Context;
using OnlineStoreRepository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStoreRepository.Services
{
    public class ProductService : IProductInterface
    {
        private readonly OnlineStoreEntities db = new OnlineStoreEntities();
        public void AddProduct(Products newProduct)
        {
            db.Products.Add(newProduct);
            db.SaveChanges();
        }

        public void EditProduct(Products newProductInfo)
        {
            Products product = db.Products.Where(p => p.ProductID == newProductInfo.ProductID).FirstOrDefault();
            product.ProductName = newProductInfo.ProductName;
            product.ProductDescription = newProductInfo.ProductDescription;
            product.ProductPrice = newProductInfo.ProductPrice;
            product.Availability = newProductInfo.Availability;
            db.SaveChanges();
        }

        public Products GetProduct(int prodID)
        {
            Products product = db.Products.Where(p => p.ProductID == prodID).FirstOrDefault();
            return product;
        }

        public List<Products> GetAllProducts(int shopID)
        {
            List<Products> productList = db.Products.Where(s => s.ShopID == shopID).ToList();
            return productList;
        }

        public bool DeleteProduct(int prodID)
        {
            try
            {
                Products productToDelete = db.Products.Where(p => p.ProductID == prodID).FirstOrDefault();
                db.Products.Remove(productToDelete);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
