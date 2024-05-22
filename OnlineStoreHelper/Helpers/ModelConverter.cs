using OnlineStoreModel.Context;
using OnlineStoreModel.CustomModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStoreHelper.Helpers
{
    public class ModelConverter
    {
        public static Owner ConvertNewOwnerToOwner(NewRegistration ownerModel)
        {
            Owner owner = new Owner()
            {
                shopname = ownerModel.Username,
                CityID = ownerModel.CityID,
                email = ownerModel.Email,
                password = ownerModel.Password,
                StateID = ownerModel.StateID,
                Description = ownerModel.Description
            };

            return owner;
        }

        public static Users ConvertNewUserToUser(NewRegistration userModel)
        {
            Users user = new Users()
            {
                username = userModel.Username,
                CityID = userModel.CityID,
                email = userModel.Email,
                password = userModel.Password,
                StateID = userModel.StateID,
                gender = userModel.Gender == "Male" ? "M" : (userModel.Gender == "Female" ? "F" : "O")                
            };

            return user;
        }

        public static Products ConvertProductModelToProduct(ProductModel productModel, int shopID)
        {
            Products product = new Products()
            {
                ProductName = productModel.ProductName,
                ProductDescription = productModel.ProductDescription,
                ProductPrice = productModel.ProductPrice,
                ShopID = shopID,
                Availability = productModel.Availability
            };
            return product;
        }

        public static ProductModel ConvertProductToProductModel(Products product)
        {
            ProductModel productModel = new ProductModel()
            {
                ProductName = product.ProductName,
                ProductDescription = product.ProductDescription,
                ProductPrice = (decimal)product.ProductPrice,
                Availability = (bool)product.Availability,
                ProductID = product.ProductID,
                ShopID = (int)product.ShopID
            };
            return productModel;
        }

        public static List<ProductModel> ConvertProductListToProductModelList(List<Products> productList)
        {
            List<ProductModel> productModelList = new List<ProductModel>();
            foreach (Products product in productList)
            {
                ProductModel newproduct = new ProductModel()
                {
                    Availability = (bool)product.Availability,
                    ProductDescription  = product.ProductDescription,
                    ProductID = product.ProductID,
                    ProductName = product.ProductName,
                    ProductPrice = (decimal)product.ProductPrice
                };
                productModelList.Add(newproduct);
            }
            return productModelList;
        }

        public static List<OwnerModel> ConvertOwnerToOwnerModel(List<Owner> shopList)
        {
            List<OwnerModel> ownerModelList = new List<OwnerModel>();
 
            foreach(Owner owner in shopList)
            {
                OwnerModel ownerModel = new OwnerModel()
                {
                    ShopID = owner.ShopID,
                    shopname = owner.shopname,
                    email = owner.email,
                    State = owner.States.StateName,
                    City = owner.Cities.CityName,
                    description = owner.Description
                };

                ownerModelList.Add(ownerModel);
            }

            return ownerModelList;
        }

        public static List<CartModel> ConvertCartListToCartListModel(List<CART> cartList)
        {
            List<CartModel> cartModelList = new List<CartModel>();

            foreach(CART cart in cartList)
            {
                CartModel cartModel = new CartModel()
                {
                    productName = cart.Products.ProductName,
                    productPrice = (decimal)cart.Products.ProductPrice,
                    productQuantity = cart.quantity,
                    totalPrice = (decimal)cart.quantity * (decimal)cart.Products.ProductPrice
                };

                cartModelList.Add(cartModel);
            }

            return cartModelList;
        }
    }
}
