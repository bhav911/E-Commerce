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

        public static Customers ConvertNewUserToUser(NewRegistration userModel)
        {
            Customers user = new Customers()
            {
                username = userModel.Username,
                CityID = userModel.CityID,
                email = userModel.Email,
                password = userModel.Password,
                StateID = userModel.StateID,
                gender = userModel.Gender == "Male" ? "M" : (userModel.Gender == "Female" ? "F" : "O"),        
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
                OwnerID = shopID,
                Availability = productModel.Availability,
                isDeleted = false
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
                OwnerID = (int)product.OwnerID,
                ImageID = product.ProductImages.FirstOrDefault().ImageID
            };
            string paths = product.ProductImages.FirstOrDefault().uniqueImageName;
            if(paths != null && paths.Length > 0)
            {
                productModel.ImagePaths = paths.Split(',');
            }
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
                string imagePaths = product.ProductImages.FirstOrDefault().uniqueImageName;
                if(imagePaths != null)
                {
                    newproduct.ImagePaths = imagePaths.Split(',');
                }
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
                    OwnerID = owner.OwnerID,
                    Shopname = owner.shopname,
                    Email = owner.email,
                    State = owner.States.StateName,
                    City = owner.Cities.CityName,
                    Description = owner.Description
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
                    ProductName = cart.Products.ProductName,
                    ProductPrice = (decimal)cart.Products.ProductPrice,
                    ProductQuantity = cart.Quantity,
                    CartID = cart.CartID
                };

                cartModelList.Add(cartModel);
            }

            return cartModelList;
        }

        public static List<OrderHistoryModel> ConvertOrderListToOrderModelList(List<Orders> orderPlacedList)
        {
            List<OrderHistoryModel> orderModelList = new List<OrderHistoryModel>();
            foreach (Orders order in orderPlacedList)
            {
                OrderHistoryModel current = new OrderHistoryModel()
                {
                    ProductName = order.Products.ProductName,
                    ProductPrice = order.unitPrice,
                    ProductQuantity= (int)order.Quantity
                };
                orderModelList.Add(current);
            }
            return orderModelList;
        }

        public static List<OrdersReceivedModel> ConvertOrdersReceivedToOrdersrecievedModel(List<Orders> ordersRecieved)
        {
            List<OrdersReceivedModel> ordersReceivedModel = new List<OrdersReceivedModel>();

            foreach(Orders order in ordersRecieved)
            {
                OrdersReceivedModel orderRecieved = new OrdersReceivedModel()
                {
                    ProductName = order.Products.ProductName,
                    ProductQuantity = (int)order.Quantity,
                    Email = order.Customers.email
                };
                ordersReceivedModel.Add(orderRecieved);
            }

            return ordersReceivedModel;
        }        
    }
}
