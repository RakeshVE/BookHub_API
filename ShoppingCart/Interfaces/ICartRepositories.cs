using ShoppingCart.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingCart.Interfaces
{
   public interface ICartRepositories
    {
        void AddToCart(cartReqDto cart);
        List<CartDto> GetItemToCart(int userId);

        void RemoveToCart(cartReqDto cart);
        void EmptyCart(cartReqDto cart);
    }
}
