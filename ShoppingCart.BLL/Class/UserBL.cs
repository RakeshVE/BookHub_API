using ShoppingCart.DAL.Models;
using ShoppingCart.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCart.BLL.Class
{
    public class UserBL
    {
        private readonly IUserRepository _userRepository;

        public UserBL(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<IEnumerable<User>> GetUsers()
        {
            return await _userRepository.GetUsers();
        }

        public async Task<User> GetUserById(int id)
        {
            return await _userRepository.GetUserById(id);

        }

        public async Task<string> DeactivtaeUser(int userId)
        {
            return await _userRepository.DeactivtaeUser(userId);


        }
    }
}
