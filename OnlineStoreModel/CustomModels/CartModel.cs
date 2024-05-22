using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStoreModel.CustomModels
{
    public class CartModel
    {
        public int CartID { get; set; }
        public string productName { get; set; }
        public int productQuantity { get; set; }
        public decimal productPrice {get; set; }
        public decimal totalPrice { get; set; }
    }
}
