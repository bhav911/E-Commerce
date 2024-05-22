using OnlineStoreModel.Context;
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
        void AddToCart(OrderModel cartOrder);
        List<CART> GetCart(int userID);
    }
}
