using OnlineStoreModel.Context;
using OnlineStoreModel.CustomModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStoreRepository.Interface
{
    public interface IProductInterface
    {
        void AddProduct(Products newProduct, string aggregatedProductImages);
        void EditProduct(EditProductModel editProductModel);
        Products GetProduct(int prodID);
        List<Products> GetAllProducts(int ShopID);
        bool DeleteProduct(int prodID);
    }
}
