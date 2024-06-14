﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStoreModel.CustomModels
{
    public class CartModel
    {
        public int CartID { get; set; }
        public string ProductName { get; set; }
        public int ProductQuantity { get; set; }
        public decimal ProductPrice {get; set; }
        public decimal TotalPrice { get; set; }
        public List<CouponModel> CouponList { get; set; }
    }
}
