using ShoppingCart.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingCart.Interfaces
{
   public interface ICartRepositories
    {
        Task AddToCart(cartReqDto cart);
        Task<IEnumerable<CartDto>> GetItemToCart(int userId);

        Task RemoveToCart(cartReqDto cart);
        Task EmptyCart(cartReqDto cart);
        Task UpdateCart(cartReqDto cart);
    }
}
