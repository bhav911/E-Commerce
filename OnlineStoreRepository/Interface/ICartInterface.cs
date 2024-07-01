﻿using OnlineStoreModel.Context;
using OnlineStoreModel.CustomModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStoreRepository.Interface
{
    public interface ICartInterface
    {
        Cart AddToCart(OrderModel cartOrder);
        List<CartItems> GetCart(int userID);
    }
}
