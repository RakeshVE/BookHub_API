using Microsoft.EntityFrameworkCore;
using ShoppingCart.Interfaces;
using ShoppingCart.DAL.Models;
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

        public async Task<string> DeactivtaeUser(int userId)
        {
            User user = new User();
            string status = "";
            user = await _context.Users.Where(x => x.UserId == userId).FirstOrDefaultAsync();

            if(user != null)
            {
                if (user.IsActive == true)
                {
                    user.IsActive = false;
                    user.ModifiedBy = userId;
                    user.ModifiedOn = DateTime.Now;
                }
                else
                {
                    user.IsActive = true;
                    user.ModifiedBy = userId;
                    user.ModifiedOn = DateTime.Now;
                }
                _context.Users.Update(user);
                await _context.SaveChangesAsync();
                status = "Success";
            }

            return status;

        }
    }
}
