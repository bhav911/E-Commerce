using OnlineStoreModel.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStoreRepository.Interface
{
    public interface IProductInterface
    {
        void AddProduct(Products newProduct);
        void EditProduct(Products newProductInfo);
        //Products GetProduct(int prodID);
        //Products GetAllProducts();
        void DeleteProduct(int prodID);
    }
}
