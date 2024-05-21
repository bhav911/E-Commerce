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
                
        }

        //public Products GetProduct(int prodID)
        //{

        //}

        //public Products GetAllProducts()
        //{

        //}

        public void DeleteProduct(int prodID)
        {
            Products productToDelete = db.Products.Where(p => p.ProductID == prodID).FirstOrDefault();
            db.Products.Remove(productToDelete);
            db.SaveChanges();
        }
    }
}
