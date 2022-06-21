using ShoppingCart.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingCart.Interfaces
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetUsers();
    }
}
