using Microsoft.EntityFrameworkCore;
using ShoppingCart.DTOs;
using ShoppingCart.Interfaces;
using ShoppingCart.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingCart.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ShoppingCartContext _context;

        public UserRepository(ShoppingCartContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<User>> GetUsers()
        {
            return await _context.Users.ToListAsync();
            
        }

        public async Task<User> GetUserById(int id)
        {
            return await _context.Users.Where(x => x.UserId == id).FirstOrDefaultAsync();
        }
    }
}
