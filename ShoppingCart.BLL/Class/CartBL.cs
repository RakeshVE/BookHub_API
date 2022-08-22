using ShoppingCart.DTO.DTOs;
using ShoppingCart.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCart.BLL.Class
{
    public class CartBL
    {
        private readonly ICartRepositories _cartRepositories;

        public CartBL(ICartRepositories cartRepositories)
        {
            _cartRepositories = cartRepositories;
        }

        public async Task AddToCart(cartReqDto cart)
        {
            await _cartRepositories.AddToCart(cart);
        }
        public async Task RemoveToCart(cartReqDto cart)
        {
            await _cartRepositories.RemoveToCart(cart);

        }




        public async Task<IEnumerable<CartDto>> GetItemToCart(int userId)
        {
          return  await _cartRepositories.GetItemToCart(userId);


        }
        public async Task EmptyCart(int userId)
        {
            await _cartRepositories.EmptyCart(userId);

        }
        public async Task UpdateCart(cartReqDto cart)
        {
            await _cartRepositories.UpdateCart(cart);

        }


    }
}
